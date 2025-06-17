
using System.CommandLine;
namespace CliFileProcessor
{
    public static class FileProcessor
    {
        public static FileInfo EnsureOutputFile(FileInfo output)
        {
            if (output == null)
            {
                string defaultPath = Path.Combine(Directory.GetCurrentDirectory(), "bundle.txt");
                output = new FileInfo(defaultPath);
            }

          
            if (!output.Directory.Exists)
            {
                output.Directory.Create(); 
            }

            return output;
        }


        public static void BundleCodeFiles(
            FileInfo outputFile,
            string[] selectedLanguages,
            DirectoryInfo sourceDirectory,
            bool includeNotes,
            bool removeEmptyLines,
            string author,
            string sortMode = "name"
        )
        {
            var allowedExtensions = selectedLanguages
                .SelectMany(lang => LanguageExtensions[lang])
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var files = sourceDirectory.GetFiles("*", SearchOption.AllDirectories)
                .Where(f => allowedExtensions.Contains(f.Extension));

            var sortedFiles = (sortMode == "type")
                ? files.OrderBy(f => f.Extension).ThenBy(f => f.Name)
                : files.OrderBy(f => f.Name);

            using (var writer = outputFile.CreateText())
            {
                if (!string.IsNullOrWhiteSpace(author))
                {
                    writer.WriteLine($"// קובץ של : {author}");
                    writer.WriteLine();
                }

                if (sortMode == "type")
                {
                    var grouped = sortedFiles.GroupBy(f => f.Extension);
                    foreach (var group in grouped)
                    {
                        if (includeNotes)
                            writer.WriteLine($"// --- סוג קובץ: {group.Key} ---");

                        foreach (var file in group)
                            WriteSingleFileContent(writer, file, sourceDirectory, includeNotes, removeEmptyLines);
                    }
                }
                else
                {
                    foreach (var file in sortedFiles)
                        WriteSingleFileContent(writer, file, sourceDirectory, includeNotes, removeEmptyLines);
                }
            }
        }

        //private static void WriteSingleFileContent(
        //    StreamWriter writer,
        //    FileInfo file,
        //    DirectoryInfo baseDir,
        //    bool includeNote,
        //    bool stripEmptyLines)
        //{
        //    if (includeNote)
        //    {
        //        string relativePath = Path.GetRelativePath(baseDir.FullName, file.FullName);
        //        writer.WriteLine($"// קובץ: {relativePath}");
        //    }

        //    if (stripEmptyLines)
        //    {
        //        foreach (var line in File.ReadLines(file.FullName)
        //                                 .Where(line => !string.IsNullOrWhiteSpace(line)))
        //        {
        //            writer.WriteLine(line);
        //        }
        //    }
        //    else
        //    {
        //        string content = File.ReadAllText(file.FullName);
        //        writer.WriteLine(content);
        //    }

        //    writer.WriteLine();
        //}
    private static void WriteSingleFileContent(
    StreamWriter writer,
    FileInfo file,
    DirectoryInfo baseDir,
    bool includeNote,
    bool stripEmptyLines)
        {
            try
            {
                if (includeNote)
                {
                    string relativePath = Path.GetRelativePath(baseDir.FullName, file.FullName);
                    writer.WriteLine($"// קובץ: {relativePath}");
                }

                if (stripEmptyLines)
                {
                    foreach (var line in File.ReadLines(file.FullName)
                                             .Where(line => !string.IsNullOrWhiteSpace(line)))
                    {
                        writer.WriteLine(line);
                    }
                }
                else
                {
                    string content = File.ReadAllText(file.FullName);
                    writer.WriteLine(content);
                }

                writer.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {file.FullName}: {ex.Message}");
            }
        }
        public static readonly Dictionary<string, string[]> LanguageExtensions = new()
        {
            ["csharp"] = new[] { ".cs" },
            ["c#"] = new[] { ".cs" },
            ["js"] = new[] { ".js" },
            ["javascript"] = new[] { ".js" },
            ["java-script"] = new[] { ".js" },
            ["ts"] = new[] { ".ts" },
            ["python"] = new[] { ".py" },
            ["py"] = new[] { ".py" },
            ["java"] = new[] { ".java" },
            ["cpp"] = new[] { ".cpp", ".hpp" },
            ["c++"] = new[] { ".cpp", ".hpp" },
            ["c"] = new[] { ".c", ".h" },
            ["html"] = new[] { ".html", ".htm" },
            ["css"] = new[] { ".css" },
            ["txt"] = new[] { ".txt" },
            ["php"] = new[] { ".php" },
            ["ruby"] = new[] { ".rb" },
            ["cobol"] = new[] { ".cob", ".cbl" }
        };
    }
}