# Changelog

## v0.2 — 2026-03-23

Major expansion: multi-format input, configurable output, UiPath integration, and schema drift detection.

### Added

- **Multi-format input** — JSON, TOML, YAML accepted alongside `.xlsx`; file picker dispatches to format-specific parsers (#25, #26, #29)
- **Nested config support** — JSON/TOML/YAML sections nest into child `SchemaNode`s; emitted as nested C# classes (Option A — scoped inside parent) (#24)
- **SchemaNode IR** — unified intermediate representation decouples all parsers from the code generator (#24)
- **CodeWriter** — indentation-aware code generation helper replaces hardcoded space strings (#24)
- **Settings panel** — interactive controls for namespace, root class, filename, .NET version, XML docs, ToString, ToJson, IsPristine, Loader toggles
- **Download as `.cs` file** — filename configurable via settings
- **ToString override** — opt-in toggle generates `public override string ToString()` on root class
- **ToJson helper** — opt-in toggle generates `public string ToJson()` using `System.Text.Json`
- **IsPristine / DriftReport** — opt-in toggle emits `Schema` manifest + `CheckPristine()` on root class + sibling `DriftReport` class (#22)
- **Loader** — opt-in toggle emits format-appropriate static factory:
  - `.xlsx` → `Load(Dictionary<string, DataTable>)` + `FromDataTable()` per section class
  - `.json` → `LoadJson(string filePath)` via `System.Text.Json`
  - `.toml` → `LoadToml(string filePath)` via Tomlyn (NuGet)
  - `.yaml` → `LoadYaml(string filePath)` via YamlDotNet (NuGet)
  (#27)
- **UiPath XAML clipboard snippet** — "Copy Assign" button generates a `ClipboardData` XML envelope pasteable into UiPath Studio with Ctrl+V; full `ExcelApplicationScope` + `ReadRange` sequence for xlsx, simple Assign for other formats (#28)
- **smol-toml** — TOML parser loaded as ES module via esm.sh; replaces `@ltd/j-toml` which had no UMD build (#26)
- **Test fixtures** — `Config_Basic.json`, `.yaml`, `.toml`, `.ini` mirror the xlsx fixture; README documents schema conventions across all formats
- **Output emit order** — section classes emitted before helper classes for top-down developer readability (#30)

### Fixed

- TOML parsing failed silently — `@ltd/j-toml` has no UMD build; switched to `smol-toml` via ESM module setting `window.TOML`
- `using System.Data` emitted for all formats — now only emitted when source is xlsx
- `FromDataTable()` emitted for all formats — now gated on xlsx source format

---

## v0.1.1 — 2026-03-23

### Fixed

- Double file picker on click — `<label>` already triggers the input natively; removed redundant `fileInput.click()` in JS handler
- Syntax highlighting broken on re-upload — `data-highlighted` flag not cleared before re-highlighting caused double-encoded HTML entities (`&lt;summary&gt;` visible as text)
- `TimeOnly` cells incorrectly detected as `DateTime` — SheetJS epoch is 1899-12-31 (not 1899-12-30); fixed to `year < 1900` guard
- Stray semicolon on properties without default initializer (`public int X { get; set; };` → `public int X { get; set; }`)

---

## v0.1 — 2026-03-23

Initial release. Hardcoded happy path: upload a UiPath REFramework `Config.xlsx` and get valid C# output in the browser.

### Added

- Static web app — HTML, CSS, vanilla JavaScript, no build step
- File upload via drag-and-drop or file picker
- SheetJS 0.20.3 client-side Excel parsing
- Three hardcoded sheets: `Settings`, `Constants`, `Assets`
- Full C# type inference from SheetJS cell types:
  `string`, `int`, `double`, `bool`, `DateOnly`, `DateTime`, `TimeOnly`
- C# class generator:
  - One class per sheet, PascalCase property names
  - Root aggregator class `AppConfig`
  - `OrchestratorAsset` helper type for Assets sheet
  - `using System;` emitted when needed
  - XML doc comments from Description column
  - Namespace configurable via in-memory config object (default: `Cpmf.Config`)
- Syntax-highlighted output via highlight.js 11.9.0 (Solarized Light theme)
- Copy-to-clipboard button
- In-memory config object with defaults (ADR-004)
- Web Awesome UI components, Solarized Light color scheme, orange header bar
- GitHub Actions deployment to GitHub Pages
- Product brief, ADRs (001–004), and ROADMAP in `docs/`
- Test fixtures in `test/fixtures/` covering all supported types
