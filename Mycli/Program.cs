using CliBundleCommand;
using CliResponseFile;
using System.CommandLine;


//if (args.Length > 0 && args[0] == "create-rsp")
//{
//    Console.WriteLine("in rsp");
//    ResponseFile.CreateRsp(); // קריאה לפונקציה CreateRsp
//    return 0;
//}

var rootCommand = new RootCommand("My CLI");
//rootCommand.AddCommand(BundleCommand.CreateBundleCommand());

// הוספת פקודת create-rsp ל-rootCommand
var createRspCommand = new Command("create-rsp", "יוצר קובץ תגובה");
createRspCommand.SetHandler(() => ResponseFile.CreateRsp());
rootCommand.AddCommand(createRspCommand);

return rootCommand.InvokeAsync(args).GetAwaiter().GetResult();
