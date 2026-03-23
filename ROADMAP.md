# Roadmap

## v0.1 — Hardcoded happy path ✓

Goal: prove the pipeline works end-to-end with zero configuration.

- [x] Single HTML page with file upload and output panel
- [x] SheetJS parses the workbook client-side
- [x] Three hardcoded sheet names: `Settings`, `Constants`, `Assets`
- [x] Full type inference via SheetJS cell type: `string`, `int`, `double`, `bool`, `DateOnly`, `DateTime`, `TimeOnly`
- [x] Class generator — one C# class per sheet + root aggregator `AppConfig` + `OrchestratorAsset` helper
- [x] XML doc comments from Description column
- [x] Syntax-highlighted output (highlight.js, Solarized Light)
- [x] Copy-to-clipboard on output
- [x] In-memory config object with defaults (namespace, root class, .NET version, doc comments)
- [x] Test fixtures for all supported types

---

## v0.2 — Multi-format input, configurable output, UiPath integration ✓

Goal: works with any config file format; output is directly usable in UiPath REFramework.

- [x] Settings panel — namespace, root class, filename, .NET version, toggles for XML docs / ToString / ToJson / IsPristine / Loader
- [x] Download as `.cs` file
- [x] ToString override on root class
- [x] ToJson helper using `System.Text.Json`
- [x] IsPristine / DriftReport — schema manifest + `CheckPristine()` for runtime drift detection (#22)
- [x] Loader — static factory per source format; xlsx uses DataTable, JSON/TOML/YAML use file-based deserializers (#27)
- [x] JSON input parser — nested sections → nested C# classes (#25)
- [x] TOML input parser — native type inference via smol-toml (#26)
- [x] YAML input parser — reuses JSON inference via js-yaml (#26)
- [x] Format dispatch — extension → parser adapter; `sourceFormat` threaded through to code generator (#29)
- [x] SchemaNode IR — unified intermediate representation decouples parsers from generator (#24)
- [x] CodeWriter — indentation-aware code generation helper (#24)
- [x] UiPath clipboard snippet — Ctrl+V paste into UiPath Studio (#28)
- [x] Sheet selection UI with per-sheet checkboxes and asset badges
- [x] Error handling — unsupported file type, parse failure, missing header, empty sheet (#21)
- [x] Test fixtures — JSON, TOML, YAML, INI mirrors of `Config_Basic.xlsx`

---

## v0.3 — Quality and reach

- [ ] PII annotation support — flag sensitive properties in generated class (#17)
- [ ] README update — reflect v0.2 feature set with usage screenshots
- [ ] GitHub Release tags for v0.1 and v0.2
