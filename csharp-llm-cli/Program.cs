// csharp-llm-cli — a minimal C#/.NET console client for the Anthropic Claude Messages API.
//
// Demonstrates: async/await, HttpClient, System.Text.Json (typed request + response models),
// environment-variable config, command-line args, and error handling.
//
// Run:
//   export ANTHROPIC_API_KEY=sk-ant-...
//   dotnet run -- "Say hello from C# in one sentence."

using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

string apiKey = Environment.GetEnvironmentVariable("ANTHROPIC_API_KEY")
    ?? throw new InvalidOperationException("Set ANTHROPIC_API_KEY first.");

string prompt = args.Length > 0 ? string.Join(' ', args) : "Say hello from C# in one sentence.";

// If this model id errors, swap it for any model your Anthropic account can use.
const string model = "claude-3-5-haiku-latest";

var request = new ChatRequest(
    Model: model,
    MaxTokens: 300,
    Messages: new[] { new Message("user", prompt) });

using var http = new HttpClient();
http.DefaultRequestHeaders.Add("x-api-key", apiKey);
http.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01");

string payload = JsonSerializer.Serialize(request);
using var content = new StringContent(payload, Encoding.UTF8, "application/json");

HttpResponseMessage response = await http.PostAsync("https://api.anthropic.com/v1/messages", content);
string body = await response.Content.ReadAsStringAsync();

if (!response.IsSuccessStatusCode)
{
    Console.Error.WriteLine($"API error {(int)response.StatusCode}: {body}");
    return 1;
}

ChatResponse? parsed = JsonSerializer.Deserialize<ChatResponse>(body);
string text = parsed?.Content?.FirstOrDefault()?.Text ?? "(no text returned)";
Console.WriteLine(text);
return 0;

// --- typed request / response models ---
record ChatRequest(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("max_tokens")] int MaxTokens,
    [property: JsonPropertyName("messages")] Message[] Messages);

record Message(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);

record ChatResponse(
    [property: JsonPropertyName("content")] ContentBlock[]? Content);

record ContentBlock(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("text")] string Text);
