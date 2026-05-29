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
  verify-then-continue API-error recovery; ~20 reusable skills plus session-lifecycle hooks, with
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

## Claude Aura — legibility as a safety primitive
[github.com/brunuff/claude-aura](https://github.com/brunuff/claude-aura) — an MCP server +
dashboard that surfaces a Claude Code session's *self-reported* state (persona, functional
state, risk) as an ambient "Andon board." The bet: human-AI collaboration gets safer when the
agent's state is visible to its operator — oversight and transparency, not a prettier dashboard.
Grounded in the Toyota Andon system and Axelrod's work on cooperation under visibility.
