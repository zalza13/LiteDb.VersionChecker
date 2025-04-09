# LiteDb.VersionChecker

🔍 Simple and reliable utility to detect if a `.db` file was created with **LiteDB 4** or **LiteDB 5** — without trying to open the file.

> 🧠 Based on the actual internal logic used by LiteDB to determine file structure versions (`FileReaderV7`, `FileReaderV8`, etc.).

## ✨ Features

- ✅ Detects LiteDB 4 files (datafile version 7)
- ✅ Detects LiteDB 5 files (datafile version 8)
- ✅ Works even on encrypted LiteDB 5 files
- ✅ Zero dependencies
- ✅ .NET Standard 2.0+ / .NET 6+ compatible

## 🚀 Installation

If published as a NuGet package:

```bash
dotnet add package LiteDb.VersionChecker
```

Otherwise, just copy the class from `LiteDbVersionDetector.cs` into your project.

## 🧪 Usage

```csharp
string version = LiteDbVersionDetector.DetectLiteDbVersion("mydata.db");
Console.WriteLine($"Detected version: {version}");
```

### Output:

```
LiteDB 4
LiteDB 5
No LiteDB file
```

## 🔍 How it works

LiteDB stores internal metadata at fixed offsets inside the first 1024 bytes of the file.

This library reads:

- A special header string: `** This is a LiteDB file **`
- A version byte:
  - `7` → LiteDB 4
  - `8` → LiteDB 5
- Encrypted LiteDB 5 files are detected by checking `buffer[0] == 1`

## 📂 File structure detection

| Version  | Offset  | Value                           |
|----------|---------|---------------------------------|
| Header   | 25 (v4) / 32 (v5) | `** This is a LiteDB file **` |
| Version  | 52 (v4) / 59 (v5) | `7` or `8`               |
| Encrypted| 0       | `1` if file is encrypted (v5+)  |

## 📜 License

MIT — Free for personal and commercial use.

---

Contributions welcome! If LiteDB won't merge it, let's improve it together here 😉

