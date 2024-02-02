// See https://aka.ms/new-console-template for more information
using DockerWrapper;

Console.WriteLine("Ver:" + Executor.GetVersion().Build);
Console.WriteLine(Executor.GetContainers());
