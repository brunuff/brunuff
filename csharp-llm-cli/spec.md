# Spec — csharp-llm-cli (Spec-Driven Development)

Written **spec-first (SDD)**, the same discipline I apply across my agent work: the contract and
acceptance criteria below were defined before the implementation, and each acceptance criterion
maps 1:1 to a test in [`tests/AnthropicTests.cs`](tests/AnthropicTests.cs) (**TDD, RED → GREEN**).

## Purpose
A minimal C#/.NET console client that sends a prompt to the Anthropic Claude Messages API and
prints the model's reply.

## Contract
- **Input:** prompt from CLI args; `ANTHROPIC_API_KEY` from the environment; model and max-tokens
  as constants.
- **Request:** `POST https://api.anthropic.com/v1/messages`, headers `x-api-key` and
  `anthropic-version`, body `{ "model", "max_tokens", "messages": [ { "role": "user",
  "content": <prompt> } ] }`.
- **Response:** parse JSON; return the text of the first `content` block whose `type` is `text`.
- **Errors (no silent failure):** missing API key → throw; non-2xx HTTP → throw with status +
  body; a response with no text block → throw.

## Acceptance criteria → tests
| AC | Criterion | Test |
|----|-----------|------|
| AC1 | the request body carries the model, max_tokens, and one user message holding the prompt | `BuildRequestJson_carries_model_maxtokens_and_user_message` |
| AC2 | a normal response yields the first text block's text | `ExtractText_returns_first_text_block` |
| AC3 | a response with an empty content array is an error, not an empty string | `ExtractText_throws_when_no_text_block` |
| AC4 | a (mocked) 200 response is parsed end-to-end with no network | `CompleteAsync_parses_a_mocked_200_response` |
| AC5 | a non-2xx status surfaces as a thrown error | `CompleteAsync_throws_on_error_status` |

## Out of scope
Streaming, multi-turn conversations, tool use, retries/backoff — kept deliberately minimal. The
goal is a clean, *tested* C# artifact, not a full SDK.
