using Rdo;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine(Directory.GetCurrentDirectory().ToString());

var runner = new ProcessRunner(@"../../RdoTestHelper/bin/Debug/net8.0/RdoTestHelper.exe");

var task = Task.Run(runner.Do);

task.Wait();

foreach(var item in runner.Stdoutput) {
    Console.WriteLine(item);
}

