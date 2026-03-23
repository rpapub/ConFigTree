# ADR-002: Sheet Mapping Rules

## Status
Accepted

## Decision

### Sheet selection
All sheets in the workbook are included by default. The user may deselect sheets before generating output.

### Row filtering
Rows where column A (Name) is empty or null are skipped. Trailing empty columns beyond the used schema are ignored.

### Column schema detection
Two schemas are supported, detected by inspecting the header row:

| Schema | Columns used | Example sheet |
|---|---|---|
| Standard | A=Name, B=Value, C=Description | Settings, Constants, CPMForge.M365 |
| Asset | A=Name, B=Asset, C=OrchestratorAssetFolder, D=Description | Assets |

In both cases, column B is the value used for type inference.

### Type inference (from column B)
| Cell value | C# type |
|---|---|
| JavaScript `boolean` / Excel `TRUE`/`FALSE` | `bool` |
| JavaScript `number` with no fractional part | `int` |
| JavaScript `number` with fractional part | `double` |
| Anything else (string, null, empty) | `string` |

### Sheet name → class name normalization
1. Split on `.` and `-`
2. PascalCase each segment independently
3. Rejoin without separator
4. Append `Config`

Examples: `CPMForge.M365` → `CpmForgeM365Config`, `Settings` → `SettingsConfig`

### Root class name
Fixed as `AppConfig` unless the user overrides it.
