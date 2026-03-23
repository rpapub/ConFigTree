# ADR-003: C# Output Conventions

## Status
Accepted

## Decision

### Structure
- One class per sheet, named per ADR-002
- One root aggregator class (`AppConfig`) with one property per sheet

### Property defaults
| C# type | Default initializer |
|---|---|
| `string` | `= "";` |
| `int` | _(none — C# default is `0`)_ |
| `double` | _(none — C# default is `0.0`)_ |
| `bool` | _(none — C# default is `false`)_ |

### Property access modifier
All properties use `{ get; set; }`.

### XML doc comments
Not generated in v1. The Description column is parsed and stored internally but not emitted. Can be added in a later iteration.

### Namespace
Not wrapped in a namespace block in v1. Plain class declarations only.

### Example output

```csharp
public class AppConfig
{
    public SettingsConfig Settings { get; set; } = new();
    public ConstantsConfig Constants { get; set; } = new();
    public AssetsConfig Assets { get; set; } = new();
    public CpmForgeM365Config CpmForgeM365 { get; set; } = new();
}

public class CpmForgeM365Config
{
    public string ApplicationId { get; set; } = "";
    public string TenantId { get; set; } = "";
    public int MailboxGetCount { get; set; }
    public int RetriesNumber { get; set; }
    public bool IncludePaginationMetadata { get; set; }
    public string LogLevel { get; set; } = "";
}
```
