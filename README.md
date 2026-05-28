# Bruno Costa Lima — Selected Work

Ph.D. food scientist (veterinary medicine; postdoc, University of Kentucky) fused
hands-on AI builder. I ship LLM-powered agent systems and computer-vision pipelines that
run in production. Three pieces, with verifiable detail.

## iScout — production agent stack (tech lead)

The agent infrastructure behind a production sports-AI platform. My layer:

- **Two MCP servers** — a memory server, and an "ask-advisor" server that lets a Sonnet
  executor consult an Opus advisor on demand (architectural ambiguity, security-touching
  diffs, unclear eval signal).
- **Canonical memory** — stores facts for O(1) lookup and reserves the model for judgment,
  so values are never silently re-derived. Trust-scored provenance over ~2,400 entries; a
  pre-commit gate runs 393+ integrity assertions.
- **Retrieval tuned from 14.7% → ~50% P@K** — trust-weighting, contextual embeddings,
  entity scanning, HyDE query expansion, and a reranker, each measured.
- **Self-correction / drift control** — a re-anchoring protocol and hooks that catch the
  agent drifting mid-session, plus a verify-then-continue API-error recovery path.
- 800+ merged PRs.

## PSD — computer-vision pipeline for particle-size analysis

A production CV system for particle-size distribution of biological samples (cannabis QC)
in a regulated manufacturing lab.

- Calibrated morphometry — 13 features/particle (area, Feret diameters, circularity,
  solidity…) against a physical reference object.
- RF / GB / SVM candidates, Optuna multi-objective tuning over ~9,000 seeds, 5-fold
  stratified cross-validation; production model at **90.95% accuracy**.
- Full pipeline: detection → preprocessing → feature extraction → classification → PDF
  report + Flask dashboard with live progress.

## Claude Aura — making agent behavior legible

[github.com/brunuff/claude-aura](https://github.com/brunuff/claude-aura) — an MCP server +
dashboard that surfaces a Claude Code session's self-reported state (persona, functional
state, risk) as an ambient "Andon board." A small bet that human-AI collaboration improves
when the machine's state is visible. Grounded in the Toyota Andon system and Axelrod's work
on cooperation under visibility.
