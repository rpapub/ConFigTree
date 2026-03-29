# Coded, and Typed

<!--
DRAFT

## The problem with the Config dictionary

Runtime surprises are the norm: misspell a key and it silently returns Nothing.
Every property access demands a cast — `.ToString()`, `CBool()`, `CInt()` — and
DateTime is simply no fun to use at all. The dictionary accepts any string key at
compile time, so typos are invisible until the robot crashes in production.
No IntelliSense, no type safety, no structure. The REFramework config dictionary
has stagnated for nearly a decade. The developer must memorize internal key names
and trust that whoever built Config.xlsx used the exact same spelling.

-->
