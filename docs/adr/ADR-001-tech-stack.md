# ADR-001: Tech Stack

## Status
Accepted

## Decision

- **HTML + CSS + vanilla JavaScript** — no framework, no build step
- **SheetJS (xlsx)** — client-side Excel parsing via CDN
- **GitHub Pages** — static hosting, zero infrastructure
- **No backend, no storage, no authentication**

## Rationale

The processing pipeline (parse → normalize → generate) has no need for a server. Keeping the stack minimal means no build tooling, no deployment pipeline, and no dependencies to maintain beyond SheetJS. The app can run offline once loaded.

## Consequences

- No TypeScript, no bundler — acceptable for this scope
- SheetJS CDN version must be pinned to avoid silent breakage
- Large `.xlsx` files may block the main thread; a Web Worker can be added later if needed
