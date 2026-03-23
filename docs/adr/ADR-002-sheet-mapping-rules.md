# ADR-002: Sheet Mapping Rules

## Status
Accepted

## Decision

### Sheet selection

- **v0.1:** Three sheets hardcoded — `Settings`, `Constants`, `Assets`
- **v0.3:** Dynamic — all sheet names read from the uploaded workbook; user may deselect via UI

### Row filtering

Rows where column A (Name) is empty or null are skipped. Trailing empty columns beyond the used schema are ignored.

### Column schemas

Two schemas, detected by inspecting the header row:

| Schema | Columns | Applies to |
|---|---|---|
| Standard | A=Name, B=Value, C=Description | Settings, Constants, and module sheets |
| Asset | A=Name, B=Asset, C=OrchestratorAssetFolder, D=Description | Assets |

### Asset schema — generated type

Each row in the Assets sheet generates a property of type `OrchestratorAsset`. This type is emitted once as a shared helper class:

```csharp
public class OrchestratorAsset
{
    public string AssetName { get; set; } = "";
    public string Folder { get; set; } = "";
}
```

Properties are always initialized empty. Sheet values (asset name, folder) are **not** embedded — they are resolved at runtime via `GetAsset` / `GetCredential` activities. If the Assets sheet has no data rows, an empty `AssetsConfig` class is still generated.

### Type inference (Standard schema, column B only)

Uses the SheetJS cell type field (`t`) — not JavaScript value guessing.

| SheetJS `t` | Condition | C# type |
|---|---|---|
| `b` | | `bool` |
| `n` | no fractional part | `int` |
| `n` | fractional part | `double` |
| `s` | | `string` |
| `d` | date part = 1899-12-30 (Excel time-only epoch) | `TimeOnly` |
| `d` | time component all zero (h/m/s = 0) | `DateOnly` |
| `d` | has both date and time | `DateTime` |
| `e` / unknown | | `string` |

`DateOnly`, `DateTime`, and `TimeOnly` require **.NET 6+**. Type detection is complete and robust from v0.1.

Type inference does not apply to the Asset schema — both `AssetName` and `Folder` are always `string`.

### Property name normalization

Column A values are normalized to PascalCase per .NET C# public property conventions.

Examples: `logF_BusinessProcessName` → `LogFBusinessProcessName`, `myAssetM365Access` → `MyAssetM365Access`

### Sheet name → class name normalization

1. Split on `.` and `-`
2. PascalCase each segment independently
3. Rejoin without separator
4. Append `Config`

Examples: `Settings` → `SettingsConfig`, `CPMForge.M365` → `CpmForgeM365Config`

### Root class name

Fixed as `AppConfig` in v0.1. User-overridable in v0.3.
