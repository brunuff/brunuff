# Bruno Costa Lima — Selected Work

Ph.D. food scientist (veterinary medicine; postdoc, University of Kentucky) and hands-on AI
builder — I keep both. I build LLM agent systems and computer-vision pipelines, and I bring the
regulated-science discipline (validation, auditability, the work of earning trust) that decides
whether they actually get used in production.

## iScout — production agent stack (tech lead)
The agent infrastructure behind a production sports-AI platform. My layer:
- **Two MCP servers** — a memory server, and an "ask-advisor" server that lets a Sonnet
  executor consult an Opus advisor on demand.
- **Canonical memory** — facts as O(1) lookup, model reserved for judgment; trust-scored
  provenance over ~2,400 entries; a pre-commit gate runs 393+ integrity assertions.
- **Retrieval tuned 14.7% → ~50% P@K** — trust-weighting, contextual embeddings, entity
  scanning, HyDE, and a reranker, each measured.
- **Self-correction / drift control** — a re-anchoring protocol, mid-session drift hooks, and
  verify-then-continue API-error recovery.
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
