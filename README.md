# Bruno Costa Lima — Selected Work

Ph.D. food scientist (veterinary medicine; postdoc, University of Kentucky) and hands-on AI
builder — I keep both. I build LLM agent systems and computer-vision pipelines, and I bring the
regulated-science discipline (validation, auditability, the work of earning trust) that decides
whether they actually get used in production.

## iScout — production agent stack (tech lead)
The agent infrastructure behind a production sports-AI platform. The through-line: an agent is
only trustworthy if a human can verify what it did, so most of my layer is evaluation and
oversight.
- **Two MCP servers** — a canonical-memory server, and an "ask-advisor" server that lets a Sonnet
  executor consult an Opus advisor on demand.
- **Canonical memory** — facts as O(1) lookup, model reserved for judgment; trust-scored
  provenance over 3,400+ entries, with quarantine thresholds and content-hash decay.
- **A pre-commit integrity gate, 393+ assertions** — sprawl checks, cross-document agreement,
  stale-value detection, registration discipline; memory can't silently drift past it.
- **Retrieval tuned 14.7% → ~50% P@K**, every step measured: 20.6% (trust-weighting) → 38.2%
  (contextual embeddings) → 45.1% (entity scanning) → ~50% (HyDE + reranker). Rerank latency
  18s → 6.5s; ~$0.13 to fully reindex, ~$0.0002 incremental, $0 per search.
- **Quality-gated model routing** — the executor/advisor split is promoted only through an eval
  gate of 15 golden issues scored on 5 dimensions, behind a 3-consecutive-pass barrier.
- **Self-correction / drift control** — a re-anchoring protocol, mid-session drift hooks, and
  verify-then-continue API-error recovery; ~20 reusable skills (including `groom-backlog`,
  `orphan-discover`, `triage-reviews`, the weekly `doc-defrag` dream-chain, and an
  `ncsc-compliance` quadrimestral cyber-threat audit) plus session-lifecycle hooks, with
  on-commit, heartbeat, and weekly integrity checks.
- 800+ merged PRs. *Company IP — happy to walk through the architecture or share a sanitized
  writeup.*

## PSD — a model a regulated lab actually trusted
[github.com/brunuff/PSD](https://github.com/brunuff/PSD) — a model isn't done when it's
accurate; it's done when a regulated lab will rely on it. PSD replaced a manual image-analysis
process in a regulated QC lab:
- Calibrated morphometry — 13 features/particle (area, Feret diameters, circularity, solidity…)
  against a physical reference object.
- RF / GB / SVM candidates, Optuna over ~9,000 seeds, 5-fold stratified CV; **production model
  at 90.95% accuracy** — validated against the manual method and shipped with the audit trail
  analysts needed to stop double-checking it.
- detection → preprocessing → feature extraction → classification → PDF report + live dashboard.

The hard part wasn't the model; it was closing the prototype-to-trust gap. That's the work I
care about most.

## Modern Workplace — an M365 Stage-Gate for a regulated client
An NPD Stage-Gate automation shipping for a regulated food manufacturer's product team —
standard M365 / Power Platform connectors only, tenant-native, no external infrastructure.
- **Eleven Power Automate flows** on SharePoint Online + Teams: scheduled reminder engine,
  Monday-morning risk digest, 3-business-day escalation router; event-triggered gate-approval
  routing with Adaptive Cards, an A3 report generator, and a document-upload handler that
  auto-tags metadata and closes checklist items.
- **Tenant-native**: client owns the SharePoint Lists, the flows, and the audit trail — no
  premium connectors, no off-tenant data movement.
- **Shipping into production**: the deployed-flow bug-fix arc — ProjectID uniqueness from
  SharePoint ID, Lookup column setup, idempotent file copy, Submitted → Approved gated by the
  PD Manager — is the part that proves *deployed* and not *demoed*. *Client IP — happy to walk
  through the architecture.*

The discipline is the same as PSD: validate against the paper process, ship the audit trail
the analysts will actually trust. Different stack, different client.

## Claude Aura — legibility as a safety primitive
[github.com/brunuff/claude-aura](https://github.com/brunuff/claude-aura) — an MCP server +
dashboard that surfaces a Claude Code session's *self-reported* state (persona, functional
state, risk) as an ambient "Andon board." The bet: human-AI collaboration gets safer when the
agent's state is visible to its operator — oversight and transparency, not a prettier dashboard.
Grounded in the Toyota Andon system and Axelrod's work on cooperation under visibility.

## csharp-llm-cli — C#/.NET LLM client
[github.com/brunuff/brunuff/tree/main/csharp-llm-cli](https://github.com/brunuff/brunuff/tree/main/csharp-llm-cli)
— a compact C#/.NET console client for the Anthropic Claude API: `async`/`await`, `HttpClient`,
`System.Text.Json` typed request/response models, env-var config, and error handling. Python is
my daily driver — this is the same LLM-orchestration work expressed in idiomatic C#, because the
cleanest way to show a language is to ship a small real thing in it.
