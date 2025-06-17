using CliFileProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliExecuteFromRsp
{
    public class ExecuteFromRsp
    {

        public static void CreateBundleCommandFromRsp(string rspFilePath)
        {
            string commandText = File.ReadAllText(rspFilePath);
            var arguments = commandText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string input = GetArgumentValue(arguments, "--input");
            string output = GetArgumentValue(arguments, "--output");
            string[] languages = GetArgumentValue(arguments, "--language").Split(',');
            string noteValue = GetArgumentValue(arguments, "--note");
            bool note;
            if (!bool.TryParse(noteValue, out note))
            {               
                note = false; 
            }
            string sort = GetArgumentValue(arguments, "--sort");
            string removeEmptyLinesValue = GetArgumentValue(arguments, "--remove-empty-lines");
            bool removeEmptyLines;
            if (!bool.TryParse(removeEmptyLinesValue, out removeEmptyLines))
            {             
                removeEmptyLines = false; 
            }

            string author = GetArgumentValue(arguments, "--author");

            try
            {
                FileInfo outputFile = new FileInfo(output);
                outputFile = FileProcessor.EnsureOutputFile(outputFile); // ודא שהקובץ קיים

                DirectoryInfo inputDirectory = new DirectoryInfo(input);


                FileProcessor.BundleCodeFiles(outputFile, languages, inputDirectory, note, removeEmptyLines, author, sort);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error executing command: {e.Message}");
            }
        }

        private static string GetArgumentValue(string[] arguments, string argumentName)
        {
            int index = Array.IndexOf(arguments, argumentName);
            if (index >= 0 && index + 1 < arguments.Length)
            {
                return arguments[index + 1];
            }
            return string.Empty;
        }

    }
}
