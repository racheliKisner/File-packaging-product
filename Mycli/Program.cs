using CliBundleCommand;
using CliResponseFile;
using System.CommandLine;

var rootCommand = new RootCommand("My CLI");

var createRspCommand = new Command("create-rsp", "יוצר קובץ תגובה");
createRspCommand.SetHandler(() => ResponseFile.CreateRsp());
rootCommand.AddCommand(createRspCommand);
if (args.Length == 0)
{
    Console.WriteLine("in rsp");
    ResponseFile.CreateRsp();  
}
else
{
    rootCommand.InvokeAsync(args).GetAwaiter().GetResult();
}