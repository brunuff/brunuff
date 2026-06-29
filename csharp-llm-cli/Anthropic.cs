// Anthropic.cs — the testable core of csharp-llm-cli.
//
// Built spec-first (see spec.md). Each public method below is covered by a test in
// tests/AnthropicTests.cs (TDD, RED → GREEN). The HTTP call is isolated behind an injected
// HttpClient so it can be exercised through a stub handler with no network.

using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CsharpLlmCli;

public static class Anthropic
{
    public const string DefaultModel = "claude-3-5-haiku-latest";   // swap if your account differs
    public const string Endpoint = "https://api.anthropic.com/v1/messages";
    public const string ApiVersion = "2023-06-01";

    /// <summary>Serialize a single-user-message request body (AC1).</summary>
    public static string BuildRequestJson(string model, int maxTokens, string prompt)
    {
        var request = new ChatRequest(model, maxTokens, new[] { new Message("user", prompt) });
        return JsonSerializer.Serialize(request);
    }

    /// <summary>Return the first text block's text, or throw if there is none (AC2/AC3).</summary>
    public static string ExtractText(string responseBody)
    {
        ChatResponse? parsed = JsonSerializer.Deserialize<ChatResponse>(responseBody);
        ContentBlock? block = parsed?.Content?.FirstOrDefault(b => b.Type == "text");
        return block?.Text
            ?? throw new InvalidOperationException("Anthropic response contained no text block.");
    }

    /// <summary>Post the prompt and return the reply text. Throws on a non-2xx status (AC4/AC5).</summary>
    public static async Task<string> CompleteAsync(
        HttpClient http, string apiKey, string model, int maxTokens, string prompt)
    {
        using var content = new StringContent(
            BuildRequestJson(model, maxTokens, prompt), Encoding.UTF8, "application/json");

        if (!http.DefaultRequestHeaders.Contains("x-api-key"))
        {
            http.DefaultRequestHeaders.Add("x-api-key", apiKey);
            http.DefaultRequestHeaders.Add("anthropic-version", ApiVersion);
        }

        HttpResponseMessage response = await http.PostAsync(Endpoint, content);
        string body = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Anthropic API error {(int)response.StatusCode}: {body}");
        }
        return ExtractText(body);
    }
}

// --- typed request / response models ---
public record ChatRequest(
    [property: JsonPropertyName("model")] string Model,
    [property: JsonPropertyName("max_tokens")] int MaxTokens,
    [property: JsonPropertyName("messages")] Message[] Messages);

public record Message(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);

public record ChatResponse(
    [property: JsonPropertyName("content")] ContentBlock[]? Content);

public record ContentBlock(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("text")] string Text);
