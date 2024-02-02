namespace DockerWrapper.Models;
public class Image
{
    public string? Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Repository { get; set; }
    public string? Tag { get; set; }
    public string? Size { get; set; }
}
