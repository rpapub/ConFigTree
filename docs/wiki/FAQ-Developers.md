# FAQ — Developers

<!--
DRAFT

Q: What does "opinionated" mean in ConFigTree?

Q: What are coded workflows / coded source files in UiPath?

Q: What is the Loader — what does it generate and when do I need it?

Q: What is IsPristine?

Q: How do I handle drift — the spreadsheet changed, what now?

Q: Why use .xlsx as the primary input format?

Q: Known issues with UiPath Studio when integrating ConFigTree

Q: Namespace and namespaces in UiPath Studio — what to set and why it matters

Q: Quirks in UiPath Studio that affect ConFigTree integration

--- Settings FAQ (one entry per setting) ---

Q: What does Namespace do?

Q: What does Root class do?

Q: What does Filename do?

Q: What is the difference between Target .NET 6 and .NET 8 output?

Q: What does XML docs do?

Q: What does Loader do?

Q: What does ToString do?

Q: What does ToJson do?

Q: What does IsPristine do?

Q: What does Readonly do?

Q: What does UiPath variable name do?

--- Added ---

Q: What formats are supported besides .xlsx?

Q: Can I use ConFigTree without REFramework?

Q: How do I update the generated class when the spreadsheet changes?

Q: Why are sheets whose name starts with a dot excluded?

Q: What is an asset sheet and how is it different from a config sheet?

Q: Can I nest config sections — sheets within sheets?

Q: Does ConFigTree handle Orchestrator Assets?

Q: What is the XAML clipboard format and why does it use that mechanism?

A: When you copy activities inside UiPath Studio, Studio puts them on the clipboard as XAML — the same XML format that `.xaml` workflow files use internally. Studio reads that format back on paste, reconstructing the activities exactly. ConFigTree generates a snippet in that same format, so pasting it into Studio is indistinguishable from pasting something you copied from another workflow. No Studio extension, no file import, no plugin — just Ctrl+C in the browser and Ctrl+V in Studio. The mechanism is not a hack; it is the documented way to programmatically author Studio workflows without the Studio API.

Q: What happens if a sheet has headers but no data rows?

Q: Can I use the generated C# class outside of UiPath?

Q: How do I contribute — add a format parser, fix a bug, open a PR?

Q: What is the ValueType column in an asset sheet?

Q: Why is there no support for Windows-Legacy projects?

A: Coded source files — `.cs` files compiled inside a UiPath project — only work in the **Windows** and **Cross-platform** targets. The **Windows-Legacy** target runs on .NET Framework 4.6.1, which predates the coded workflows feature entirely. UiPath never backported coded workflow support to Legacy, and ConFigTree depends on it. If your project is on Windows-Legacy, the path forward is to migrate the project to the Windows target first, then add ConFigTree.

Q: Why does the Namespace setting matter — can I use any name?

A: C# identifiers must start with a letter or underscore, not a digit or special character. A namespace like `1stProject.Config` or `My-Project.Config` is a compile error. UiPath project names often contain characters that are illegal in C# — spaces, hyphens, leading digits. The Namespace setting lets you enter a clean C# name regardless of what the project folder is called. The default `Cpmf.Config` is safe. If you change it, use only letters, digits (not first), dots, and underscores. Studio will show a compile error immediately if the name is invalid, so the mistake is caught early.

Q: How do I override or extend the generated code?

A: The generated `.cs` file is a plain C# source file compiled inside the Studio project — nothing prevents you from editing it. The safe approach is to add a separate partial class file alongside it. For example, if the generated class is `CodedConfig`, create a `CodedConfig.Extensions.cs` in the same `Lib/` folder with `partial class CodedConfig { ... }` and add your custom methods or properties there. When you regenerate from an updated Config.xlsx, your extensions file is untouched. Avoid editing the generated file directly — it will be overwritten the next time you regenerate.

Q: Is it safe to re-generate when changes happen during maintenance?

A: Yes, with one condition: keep your customisations in a separate file (see above). The generated file is overwrite-safe — drop the new `.cs` into `Lib/` and Studio recompiles. Property names and types track the Config.xlsx exactly, so any drift between the spreadsheet and the code is caught immediately by Verify Project rather than surfacing at runtime. If a setting is renamed or removed in the xlsx, the generated class reflects that and Studio shows a compile error wherever the old name was referenced — which is the point: the error is at design time, not in production.

-->
