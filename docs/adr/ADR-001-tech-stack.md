# ADR-001: Tech Stack

## Status
Accepted

## Decision

- **HTML + CSS + vanilla JavaScript** — no framework, no build step
- **SheetJS (xlsx)** — client-side Excel parsing via CDN, version pinned
- **Web Awesome** — UI component library (web components, no framework dependency)
- **Solarized** — color scheme
- **GitHub Pages** — static hosting via GitHub Actions deployment
- **No backend, no storage, no authentication**
- **No localStorage in v0.1** — revisit in v0.3 for persisting user preferences (root class name, sheet selection)

## Rationale

The processing pipeline (parse → normalize → generate) has no need for a server. Keeping the stack minimal means no build tooling and no dependencies to maintain beyond SheetJS and Web Awesome. The app can run offline once loaded. GitHub Actions gives a clean, repeatable deployment without manual branch management.

## Consequences

- SheetJS CDN version must be pinned to avoid silent breakage
- Large `.xlsx` files may block the main thread; a Web Worker can be added later if needed
