# csharp-llm-cli

A compact **C#/.NET** console client for the Anthropic Claude Messages API — `async`/`await`,
`HttpClient`, `System.Text.Json` typed request/response models, env-var config, and error
handling. Python is my daily driver; this is the same LLM-orchestration work expressed in
idiomatic C#.

## Run

```bash
# .NET SDK (macOS, once):  brew install --cask dotnet-sdk
export ANTHROPIC_API_KEY=sk-ant-...
dotnet run -- "Say hello from C# in one sentence."
```

Expected: a one-sentence reply from Claude. If the model id errors, edit the `model` constant in
`Program.cs` to any model your Anthropic account can use.
