using System.CommandLine;
using CliBundleCommand;
using CliResponseFile;

var rootCommand = new RootCommand("My CLI");

// הוספת פקודת create-rsp
var createRspCommand = new Command("create-rsp", "יוצר קובץ תגובה");
createRspCommand.SetHandler(() => ResponseFile.CreateRsp());
rootCommand.AddCommand(createRspCommand);

// הוספת פקודת bundle
rootCommand.AddCommand(BundleCommand.CreateBundleCommand()); // הוספת פקודת bundle

if (args.Length == 0)
{
    Console.WriteLine("in rsp");
    ResponseFile.CreateRsp();
}
else
{
    rootCommand.InvokeAsync(args).GetAwaiter().GetResult();
}
