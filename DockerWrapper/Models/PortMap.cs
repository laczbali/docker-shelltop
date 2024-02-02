using System.Text.RegularExpressions;

namespace DockerWrapper.Models;
public class PortMap
{
    public int? HostPort { get; set; }
    public int? ContainerPort { get; set; }
    public string? Protocol { get; set; }

    public static List<PortMap> FromString(string? portMapString)
    {
        if (string.IsNullOrWhiteSpace(portMapString)) return new();
        var maps = portMapString.Split(',', StringSplitOptions.TrimEntries & StringSplitOptions.RemoveEmptyEntries).ToList();
        return maps.Select(map =>
        {
            var hostPortString = Regex.Match(map, ":\\d+")?.Value?.TrimStart(':') ?? string.Empty;
            var containerPortString = Regex.Match(map, "\\d+\\/")?.Value?.TrimEnd('/') ?? string.Empty;
            var protocol = Regex.Match(map, "\\w+$")?.Value;

            return new PortMap
            {
                HostPort = int.TryParse(hostPortString, out var hp) ? hp : null,
                ContainerPort = int.TryParse(containerPortString, out var cp) ? cp : null,
                Protocol = protocol
            };
        }).ToList();
    }
}
