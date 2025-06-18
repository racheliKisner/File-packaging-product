# 📦 CLI Code Bundler

A command-line tool for bundling code files by language and preferences, with support for response files.

---

## 🚀 Usage

### Basic Command:

```bash
dotnet run -- bundle -i <input-folder> -o <output-file> -l <languages> [options]
```

### Example:

```bash
dotnet run -- bundle -i src -o result.txt -l csharp,js -n -s name -r -a "Rachel"
```

### Help:

```bash
dotnet run -- --help
```

---

## 🧾 Available Options

| Short | Long Option            | Description                                       |
| ----- | ---------------------- | ------------------------------------------------- |
| `-i`  | `--input`              | Input folder (default: current directory)         |
| `-o`  | `--output`             | Output file path and name                         |
| `-l`  | `--language`           | List of languages (comma-separated) or `all`      |
| `-n`  | `--note`               | Include file source note                          |
| `-s`  | `--sort`               | Sort mode: `name` or `type`                       |
| `-r`  | `--remove-empty-lines` | Remove empty lines from files                     |
| `-a`  | `--author`             | Name of the author to include in the bundled file |

---

## 🧰 Creating a Response File

```bash
dotnet run -- create-rsp
```

You'll be prompted to enter parameters → a `.rsp` file will be created with the full command.

### Run with Response File:

```bash
dotnet run -- @fileName.rsp
```

---

## 📁 Aliases

All options support short aliases to simplify usage:

```bash
-i, -o, -l, -n, -s, -r, -a
```

---

## 🧪 Tests & Validation

* Supports multiple languages: csharp, js, python, java, html, etc.
* Full validation for input and formats
* Friendly error messages for invalid values

---

Good luck and happy coding! 😄
