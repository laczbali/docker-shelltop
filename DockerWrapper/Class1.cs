using System.Diagnostics;

namespace DockerWrapper;
public class Class1
{
    public static string GetVersion() => RunDockerCommand("--version");
    public static string ListContainers() => RunDockerCommand("container list");

    private static string RunDockerCommand(string command)
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
}
