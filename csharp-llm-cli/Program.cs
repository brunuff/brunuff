// csharp-llm-cli — thin CLI over Anthropic.cs.
// Spec: spec.md (SDD).  Tests: tests/ (TDD).
//   export ANTHROPIC_API_KEY=sk-ant-...
//   dotnet run -- "Say hello from C# in one sentence."

using System;
using System.Net.Http;
using System.Threading.Tasks;
using CsharpLlmCli;

string apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")
    ?? throw new InvalidOperationException("Set ANTHROPIC_API_KEY first.");
string prompt = args.Length > 0 ? string.Join(' ', args) : "Say hello from C# in one sentence.";

using var http = new HttpClient();
try
{
    string text = await Anthropic.CompleteAsync(http, apiKey, Anthropic.DefaultModel, 300, prompt);
    Console.WriteLine(text);
    return 0;
}
catch (Exception ex)
{
    Console.Error.WriteLine(ex.Message);
    return 1;
}
