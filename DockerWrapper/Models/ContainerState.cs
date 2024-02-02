namespace DockerWrapper.Models;
public enum ContainerState
{
    Created,
    Running,
    Paused,
    Restarting,
    Exited,
    Removing,
    Dead,
    Unknown
}
