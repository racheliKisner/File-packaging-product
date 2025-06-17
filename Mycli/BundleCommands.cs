
using System.CommandLine;
using CliOptions;
using CliFileProcessor;
namespace CliBundleCommand
{
    public static class BundleCommand
    {
        public static Command CreateBundleCommand()
        {
            var command = new Command("bundle", "Bundles code files based on specified options");

            
            command.AddOption(Options.BundleOption);
            command.AddOption(Options.FolderOption);
            command.AddOption(Options.LanguageOption);
            command.AddOption(Options.NoteOption);
            command.AddOption(Options.SortOption);
            command.AddOption(Options.RemoveEmptyLinesOption);
            command.AddOption(Options.AuthorOption);

            command.SetHandler(
            (FileInfo output, DirectoryInfo input, string[] languages, bool note, string sort, bool removeEmptyLines, string author) =>
             {

                 Console.WriteLine($"Output File: {output}");
                 Console.WriteLine($"Input Directory: {input.FullName}");
                 Console.WriteLine($"Languages: {string.Join(", ", languages)}");
                 Console.WriteLine($"Include Note: {note}");
                 Console.WriteLine($"Sort Files: {sort}");
                 Console.WriteLine($"Remove Empty Lines: {removeEmptyLines}");
                 Console.WriteLine($"Author: {author}");
                 try {
                     output=FileProcessor.EnsureOutputFile(output);
                     FileProcessor.BundleCodeFiles(output, languages,input ,note, removeEmptyLines,author,sort);
                 }
                 catch(Exception e) 
                 { 
                     Console.WriteLine(e); 
                 }            
               },
                Options.BundleOption,
                Options.FolderOption,
                Options.LanguageOption,
                Options.NoteOption,
                Options.SortOption,
                Options.RemoveEmptyLinesOption,
                Options.AuthorOption
            );
                return command;
            }
    }
}