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
            Console.WriteLine("!שלום וברכה");
            Console.WriteLine("? מה שמך");
            string author = Console.ReadLine();

            Console.WriteLine("?מאיזה תיקייה תרצי להעתיק את הקבצים");
            string input = Console.ReadLine();

            Console.WriteLine("באיזה תיקייה תרצי לשמור את הקובץ או הקישי אנטר לשמירה בקובץ זה");
            string output = Console.ReadLine();

            Console.WriteLine("all אילו שפות תרצי לכלול בקובץ או בחרי  ");
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

            Console.WriteLine("האם תרצי להוסיף הערת סוג קובץ בחרי true או false");
            string NoteOption = Console.ReadLine();

            Console.WriteLine("האם תרצי למיין לפי שפות true או false לפי שם הקובץ");
            string SortOption = Console.ReadLine();

            Console.WriteLine("האם תרצי להוסיר שורות ריקות בחרי true או false");
            string RemoveEmptyLinesOption = Console.ReadLine();

            var command = $"fib bundle --input {input} --output {output}" +
                $" --language {LanguageOption} --note {NoteOption} --sort {SortOption} " +
                $"--remove-empty-lines {RemoveEmptyLinesOption} --author {author}";

            // שמירת הפקודה בקובץ תגובה
            File.WriteAllText("fileName.rsp", command);

            Console.WriteLine("קובץ התגובה נוצר בהצלחה בשם fileName.rsp.");
            ExecuteFromRsp.CreateBundleCommandFromRsp("fileName.rsp");
        }
    }
}
