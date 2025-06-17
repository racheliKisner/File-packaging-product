using System.CommandLine;
namespace CliOptions
{
    public static class Options
    {
        public static Option<FileInfo> BundleOption = new Option<FileInfo>(
            new[] { "--output", "-o" },
            "file path and name");

        public static Option<DirectoryInfo> FolderOption = new Option<DirectoryInfo>(
            new[] { "--input", "-i" },
            () => new DirectoryInfo(Directory.GetCurrentDirectory()),
            "Directory containing code files to bundle");

        public static Option<string[]> LanguageOption = CreateLanguageOption();

        public static Option<string[]> CreateLanguageOption()
        {
            return new Option<string[]>(
                new[] { "--language", "-l" },
                parseArgument: result =>
                {
                    var allowedLanguages = new HashSet<string>
                    {
                        "csharp", "c#", "js", "javascript", "java-script",
                        "python", "java", "cpp", "c++", "c", "html", "css", "ts", "txt",
                        "php", "ruby", "cobol"
                    };

                    var values = result.Tokens.Select(t => t.Value.ToLowerInvariant()).ToArray();
                    if (values.Contains("all"))
                    {
                        return allowedLanguages.ToArray();
                    }

                    var invalid = values.Where(v => !allowedLanguages.Contains(v)).ToList();

                    if (invalid.Any())
                    {
                        result.ErrorMessage = $"Invalid language(s): {string.Join(", ", invalid)}";
                        return Array.Empty<string>();
                    }

                    return values;
                }
            )
            {
                Description = "List of programming languages to include. Use 'all' to include all code files.",
                IsRequired = true,
                AllowMultipleArgumentsPerToken = true
            };
        }

        public static Option<bool> NoteOption = new Option<bool>(
            new[] { "--note", "-n" },
            description: "Include source code note in the bundle",
            getDefaultValue: () => false
        );

        public static Option<string> SortOption = new Option<string>(
            new[] { "--sort", "-s" },
            description: "which sort of the files"
        );

        public static Option<bool> RemoveEmptyLinesOption = new Option<bool>(
            new[] { "--remove-empty-lines", "-r" },
            description: "Remove all empty lines in files",
            getDefaultValue: () => false
        );

        public static Option<string> AuthorOption = new Option<string>(
            new[] { "--author", "-a" },
            description: "Name of the file author"
        );
    }
}
