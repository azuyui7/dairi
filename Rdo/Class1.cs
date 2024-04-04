using System.Text.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.CodeDom.Compiler;
using Google.Protobuf.WellKnownTypes;

namespace Rdo;

public record Log(DateTime Time, string Message, bool IsErrMsg);

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Serialization)]
[JsonSerializable(typeof(List<Log>))]
internal partial class LogJsonSerializer : JsonSerializerContext {}

public class ProcessRunner(string ExecPath, string Arguments = "") {

    string ExecPath = ExecPath;
    string Arguments = Arguments;

    object _instance_lock = new();
    public Queue<Log> Stdoutput { get; private set; } = new();
    public Queue<Log> Erroutput { get; private set; } = new();

    public async void  Do() {

        var si = new ProcessStartInfo {
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,

            FileName = ExecPath,
            Arguments = Arguments,
        };

        using var pc = new Process();
        pc.StartInfo = si;
        pc.OutputDataReceived += (sender, e) => {
            if (e.Data != null) {
                Stdoutput.Enqueue(new(DateTime.Now, e.Data, false));
            }
        };
        pc.ErrorDataReceived += (sender, e) => {
            if (e.Data != null) {
                Erroutput.Enqueue(new(DateTime.Now, e.Data, true));
            }
        };
        pc.Start();
        pc.BeginOutputReadLine();
        await pc.WaitForExitAsync();
        pc.Close();
        
        HogeMyHoge.Hoge hoge = new();
        
        
        
    }
}
