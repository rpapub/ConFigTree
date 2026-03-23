# ADR-004: Browser Storage and App Config Object

## Status
Accepted

## Decision

### Config object

A single in-memory config object holds all runtime settings. No behavioral values are hardcoded in generation logic — everything reads from this object.

```javascript
const config = {
  namespace: "Cpmf.Config",
  rootClassName: "AppConfig",
  dotnetVersion: "net6",
  xmlDocComments: true,
}
```

### Storage by version

| Version | Storage | Notes |
|---|---|---|
| v0.1 | In-memory only | Config instantiated with defaults on page load; not persisted |
| v0.2 | In-memory | UI exposes all settings as inputs/toggles |
| v0.3 | localStorage | Config persisted between sessions; hydrated from localStorage on load, falls back to defaults |

### Feature toggle pattern

All behavioral switches read from the config object. Adding a new option means adding a key with a default — no changes to generation logic internals.

### UI placement

Settings panel, separate from the main input/output areas. Collapsed by default in v0.1 (defaults are sensible). Fully visible and interactive from v0.2.
