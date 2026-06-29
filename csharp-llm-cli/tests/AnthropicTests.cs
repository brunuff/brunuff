// AnthropicTests.cs — xUnit suite (TDD). One test per acceptance criterion in spec.md.
// Network-free: the HTTP path is exercised through a stub HttpMessageHandler.

using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CsharpLlmCli;
using Xunit;

public class AnthropicTests
{
    // AC1
    [Fact]
    public void BuildRequestJson_carries_model_maxtokens_and_user_message()
    {
        string json = Anthropic.BuildRequestJson("claude-x", 123, "hi there");

        using JsonDocument doc = JsonDocument.Parse(json);
        JsonElement root = doc.RootElement;
        Assert.Equal("claude-x", root.GetProperty("model").GetString());
        Assert.Equal(123, root.GetProperty("max_tokens").GetInt32());

        JsonElement message = root.GetProperty("messages")[0];
        Assert.Equal("user", message.GetProperty("role").GetString());
        Assert.Equal("hi there", message.GetProperty("content").GetString());
    }

    // AC2
    [Fact]
    public void ExtractText_returns_first_text_block()
    {
        string body = """{"content":[{"type":"text","text":"hello world"}]}""";
        Assert.Equal("hello world", Anthropic.ExtractText(body));
    }

    // AC3
    [Fact]
    public void ExtractText_throws_when_no_text_block()
    {
        string body = """{"content":[]}""";
        Assert.Throws<InvalidOperationException>(() => Anthropic.ExtractText(body));
    }

    // AC4
    [Fact]
    public async Task CompleteAsync_parses_a_mocked_200_response()
    {
        var handler = new StubHandler(
            HttpStatusCode.OK, """{"content":[{"type":"text","text":"mocked reply"}]}""");
        using var http = new HttpClient(handler);

        string text = await Anthropic.CompleteAsync(http, "key", "model", 10, "prompt");

        Assert.Equal("mocked reply", text);
    }

    // AC5
    [Fact]
    public async Task CompleteAsync_throws_on_error_status()
    {
        var handler = new StubHandler(HttpStatusCode.Unauthorized, """{"error":"bad key"}""");
        using var http = new HttpClient(handler);

        await Assert.ThrowsAsync<HttpRequestException>(
            () => Anthropic.CompleteAsync(http, "key", "model", 10, "prompt"));
    }

    /// <summary>Returns a canned response without touching the network.</summary>
    private sealed class StubHandler : HttpMessageHandler
    {
        private readonly HttpStatusCode _status;
        private readonly string _body;

        public StubHandler(HttpStatusCode status, string body)
        {
            _status = status;
            _body = body;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
            => Task.FromResult(new HttpResponseMessage(_status) { Content = new StringContent(_body) });
    }
}
