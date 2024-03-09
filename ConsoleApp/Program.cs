// See https://aka.ms/new-console-template for more information
using DockerWrapper;

Console.WriteLine("Ver:" + DockerExecutor.GetVersion().Build);
Console.WriteLine(DockerExecutor.GetContainers());
