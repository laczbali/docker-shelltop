using DockerWrapper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DockerWrapper;

public class DockerExecutor
{
    public static (string Ver, string Build) GetVersion()
    {
        var cmdRes = RunCommand("--version");
        return (
            Regex.Match(cmdRes, "\\d+\\.\\d+\\.\\d+").Value,
            Regex.Match(cmdRes, "[\\w\\d]+$").Value);
    }

    public static List<Image> GetImages()
    {
        var cmdRes = RunCommand("image list --format json");
        var jsonStrings = cmdRes.Split("\n", StringSplitOptions.TrimEntries & StringSplitOptions.RemoveEmptyEntries);

        var images = new List<Image>();
        foreach (var jsonStr in jsonStrings)
        {
            var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonStr);
            if (jsonObject is null) continue;

            images.Add(new Image
            {
                Id = jsonObject["ID"]?.Value<string>(),
                CreatedAt = ExtractDateTime(jsonObject["CreatedAt"]?.Value<string>()),
                Repository = jsonObject["Repository"]?.Value<string>(),
                Tag = jsonObject["Tag"]?.Value<string>(),
                Size = jsonObject["Size"]?.Value<string>()
            });
        }

        return images;
    }

    public static List<Container> GetContainers()
    {
        var cmdRes = RunCommand("container list --no-trunc --all --format json");
        var jsonStrings = cmdRes.Split("\n", StringSplitOptions.TrimEntries & StringSplitOptions.RemoveEmptyEntries);

        var containers = new List<Container>();
        foreach (var jsonStr in jsonStrings)
        {
            var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonStr);
            if (jsonObject is null) continue;

            containers.Add(new Container
            {
                Id = jsonObject["ID"]?.Value<string>(),
                CreatedAt = ExtractDateTime(jsonObject["CreatedAt"]?.Value<string>()),
                Image = jsonObject["Image"]?.Value<string>(),
                Name = jsonObject["Names"]?.Value<string>(),
                State = Enum.TryParse<ContainerState>(jsonObject["State"]?.Value<string>(), true, out var state) ? state : ContainerState.Unknown,
                Status = jsonObject["Status"]?.Value<string>(),
                Mounts = jsonObject["Mounts"]?.Value<string>()?.Split(',').ToList() ?? new(),
                Ports = PortMap.FromString(jsonObject["Ports"]?.Value<string>())
            });
        }

        return containers;
    }

    public static void ComposeUp(string composeFile)
        => RunCommand($"compose --file {composeFile} up --detach");

    public static void ComposeDown(string composeFile, bool removeImages = true)
        => RunCommand($"compose --file {composeFile} down" + (removeImages ? " --rmi all" : string.Empty));

    public static void StartContainer(string containerName)
        => RunCommand($"container start {containerName}");

    public static void StopContainer(string containerName)
        => RunCommand($"container stop {containerName}");

    public static string GetLogs(string containerName)
        => RunCommand($"container logs {containerName}");

    public static string ExecInContainer(string containerName, string command)
        => RunCommand($"container exec {containerName} {command}");

    private static string RunCommand(string command)
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = $"bash",
                Arguments = $"-c \"docker {command}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        process.Start();
        var output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();
        return output;
    }

    private static DateTime? ExtractDateTime(string? dockerDateTimeStr)
    {
        var dateParts = dockerDateTimeStr?.Split(" ").Take(3).ToArray() ?? Array.Empty<string>();
        var dateOk = DateTime.TryParse($"{dateParts[0]}T{dateParts[1]}{dateParts[2]}", out var date);
        return dateOk ? date : null;
    }
}
