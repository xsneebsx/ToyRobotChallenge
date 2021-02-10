using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ToyRobotChallenge.Library;

namespace ToyRobotChallenge.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddToyRobotServices()
                .AddLogging(config => config.AddConsole());

            var sp = services.BuildServiceProvider();

            Parser.Default.ParseArguments<CommandFileOptions>(args)
                .WithParsed<CommandFileOptions>(options => ParseFile(sp, options))
                ;
        }

        private static void ParseFile(ServiceProvider serviceProvider, CommandFileOptions commandFileOptions)
        {
            var trParser = serviceProvider.GetRequiredService<ICommandParser>();
            trParser.Parse(System.IO.File.ReadAllLines(commandFileOptions.FilePath));
        }
    }

    [Verb("file", HelpText = "File with commands to execute")]
    public class CommandFileOptions
    {
        [Option('f', "filepath", Required = true)]
        public string FilePath { get; set; }
    }
}
