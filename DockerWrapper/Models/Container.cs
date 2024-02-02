namespace DockerWrapper.Models;
public class Container
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Image { get; set; }
    public DateTime? CreatedAt { get; set; }
    public ContainerState State { get; set; } = ContainerState.Unknown;
    public string? Status { get; set; }
    public List<PortMap> Ports { get; set; } = new();
    public List<string> Mounts { get; set; } = new();
}
