using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CliExecuteFromRsp;

namespace CliResponseFile
{
    public class ResponseFile
    {
        public static void CreateRsp()
        {
            Console.WriteLine("Hello");
            Console.WriteLine("What is your name?");
            string author = Console.ReadLine();

            Console.WriteLine("From which folder would you like to copy the files?");
            string input = Console.ReadLine();

            Console.WriteLine("In which folder would you like to save the file or press Enter to save to this file");
            string output = Console.ReadLine();

            Console.WriteLine("Which languages ​​would you like to include in the file or select all?");
            string LanguageOption = Console.ReadLine().Trim().ToLower();
            var allowedLanguages = new HashSet<string>
            {
                "csharp", "c#", "js", "javascript", "java-script",
                "python","py", "java", "cpp", "c++", "c", "html", "css", "ts", "txt",
                "php", "ruby", "cobol"
            };

            if (LanguageOption == "all")
            {
                LanguageOption = string.Join(", ", allowedLanguages);
            }
            else
            {
                var values = LanguageOption.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                           .Select(v => v.Trim().ToLower())
                                           .ToArray();
                var invalid = values.Where(v => !allowedLanguages.Contains(v)).ToList();

                //if (invalid.Any())
                //{
                //    Console.WriteLine($"שפות לא חוקיות: {string.Join(", ", invalid)}");
                //    return; 
                //}

                LanguageOption = string.Join(", ", values);
            }

            Console.WriteLine("Would you like to add a file type comment? Select true or false.");
            string NoteOption = Console.ReadLine();

            Console.WriteLine("Would you like to sort by file name? Select true or false.");
            string SortOption = Console.ReadLine();

            Console.WriteLine("Would you like to remove empty rows? Select true or false.");
            string RemoveEmptyLinesOption = Console.ReadLine();

            var command = $"fib bundle --input {input} --output {output}" +
                $" --language {LanguageOption} --note {NoteOption} --sort {SortOption} " +
                $"--remove-empty-lines {RemoveEmptyLinesOption} --author {author}";
           
            File.WriteAllText("fileName.rsp", command);

            Console.WriteLine("The response file was successfully created as fileName.rsp.");
            ExecuteFromRsp.CreateBundleCommandFromRsp("fileName.rsp");
        }
    }
}
