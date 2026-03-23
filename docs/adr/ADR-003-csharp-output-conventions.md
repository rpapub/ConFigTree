# ADR-003: C# Output Conventions

## Status
Accepted

## Decision

### Structure
- One class per sheet, named per ADR-002
- One root aggregator class `AppConfig` with one property per sheet
- `OrchestratorAsset` helper class emitted once alongside `AssetsConfig`

### Namespace
- Default: `Cpmf.Config`
- User-configurable; blank input falls back to `Cpmf.Config`

### Target .NET version
- **v0.1:** .NET 6 hardcoded
- **v0.2+:** user-selectable; affects available types (`DateOnly`, `TimeOnly`) and syntax

### Property defaults

| C# type | Default initializer |
|---|---|
| `string` | `= "";` |
| `int` | _(none — C# default `0`)_ |
| `double` | _(none — C# default `0.0`)_ |
| `bool` | _(none — C# default `false`)_ |
| `DateOnly` | _(none)_ |
| `DateTime` | _(none)_ |
| `TimeOnly` | _(none)_ |
| `OrchestratorAsset` | `= new();` |

### Property access modifier
All properties use `{ get; set; }`.

### XML doc comments
Generated from the Description column from v0.1. If Description is empty, no comment is emitted.

### Example output

```csharp
namespace Cpmf.Config
{
    /// <summary>Root configuration object.</summary>
    public class AppConfig
    {
        public SettingsConfig Settings { get; set; } = new();
        public ConstantsConfig Constants { get; set; } = new();
        public AssetsConfig Assets { get; set; } = new();
    }

    public class AssetsConfig
    {
        /// <summary>M365 access credential.</summary>
        public OrchestratorAsset MyAssetM365Access { get; set; } = new();
    }

    public class OrchestratorAsset
    {
        public string AssetName { get; set; } = "";
        public string Folder { get; set; } = "";
    }

    public class SettingsConfig
    {
        /// <summary>Orchestrator queue Name. Must match the queue name defined in Orchestrator.</summary>
        public string OrchestratorQueueName { get; set; } = "";
    }

    public class ConstantsConfig
    {
        /// <summary>Must be 0 if working with Orchestrator queues.</summary>
        public int MaxRetryNumber { get; set; }
        /// <summary>Must be TRUE or FALSE.</summary>
        public bool ShouldMarkJobAsFaulted { get; set; }
    }
}
```
