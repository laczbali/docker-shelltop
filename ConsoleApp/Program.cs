// See https://aka.ms/new-console-template for more information
using DockerWrapper;

Console.WriteLine("Ver:" + Class1.GetVersion());
Console.WriteLine(Class1.ListContainers());

while(true)
{
    Thread.Sleep(1000);
    Console.WriteLine("Hello World!");
}