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

## Feedback checkpoint

Share v0.1 and collect feedback before expanding scope.

---

## v0.2 — Configurable and richer output

Goal: settings are interactive, output is more useful.

- [ ] Settings panel — editable namespace, root class name, .NET version, XML doc comments toggle (#12)
- [ ] Download as `.cs` file
- [ ] `.ToString()` override on generated classes
- [ ] `.ToJson()` / JSON serialization helper
- [ ] PII annotation support (flag properties containing sensitive data)
- [ ] Syntax-highlighted output polish (#10 follow-up)

---

## v0.3 — Dynamic sheet handling

Goal: works with any workbook, not just the REFramework template.

- [ ] Read all actual sheet names from the uploaded file
- [ ] Sheet selection UI — checkboxes to include/exclude
- [ ] localStorage — persist user preferences between sessions
- [ ] Basic error handling — wrong file type, missing header, empty sheet
