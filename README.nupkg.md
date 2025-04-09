# LiteDb.VersionChecker

**LiteDb.VersionChecker** is a lightweight .NET utility that determines whether a `.db` file was created with **LiteDB 4** or **LiteDB 5**, by analyzing the binary file header — no database open required.

## Features

- 🧠 Detects LiteDB 4 files (datafile version 7)
- ✅ Detects LiteDB 5 files (datafile version 8)
- 🔒 Supports encrypted LiteDB 5 files
- ⚡ Fast and dependency-free
- 🔧 Compatible with .NET Standard 2.0+ / .NET 6+

## Usage

```csharp
string version = LiteDbVersionDetector.DetectLiteDbVersion("path/to/file.db");
Console.WriteLine(version); // "LiteDB 4", "LiteDB 5" or "No LiteDB file"
```

## File Format Logic

LiteDb.VersionChecker reads the first 1024 bytes of the file and inspects known headers:

| Version     | Header Offset | Version Offset | Byte Value |
|-------------|----------------|----------------|------------|
| LiteDB 4    | 25             | 52             | `7`        |
| LiteDB 5    | 32             | 59             | `8`        |
| Encrypted   | 0              | -              | `1`        |

## License

MIT — Free for commercial and personal use.

> Project maintained by [@zalza13](https://github.com/zalza13)
