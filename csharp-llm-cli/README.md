# csharp-llm-cli

A compact **C#/.NET** console client for the Anthropic Claude Messages API, built **spec-first
(SDD)** and **test-driven (TDD)**:

- **SDD** — the contract and acceptance criteria were written *before* the code, in
  [`spec.md`](spec.md).
- **TDD** — every acceptance criterion maps to a test in
  [`tests/AnthropicTests.cs`](tests/AnthropicTests.cs) (RED → GREEN). The suite runs **offline** —
  the HTTP call is exercised through a stub `HttpMessageHandler`, so no API key or network is
  needed to test.

Demonstrates `async`/`await`, `HttpClient`, `System.Text.Json` typed models, env-var config, and
no-silent-failure error handling. Python is my daily driver; this is the same LLM-orchestration
work expressed in idiomatic, tested C#.

## Layout
- `Anthropic.cs` — the testable core (request building, response parsing, the API call).
- `Program.cs` — a thin CLI entry point over it.
- `spec.md` — the SDD contract + acceptance-criteria → test map.
- `tests/` — the xUnit suite (TDD).

## Run
```bash
# .NET SDK (macOS, once):  brew install --cask dotnet-sdk
export ANTHROPIC_API_KEY=sk-ant-...
dotnet run -- "Say hello from C# in one sentence."
```
If the model id errors, edit the `DefaultModel` constant in `Anthropic.cs`.

## Test (no network / no API key required)
```bash
dotnet test tests/csharp-llm-cli.Tests.csproj
```
