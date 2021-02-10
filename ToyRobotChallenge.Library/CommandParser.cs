using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ToyRobotChallenge.Library
{
    public interface ICommandParser
    {
        /// <summary>
        /// Parse and execute a blob of text and execute the commands
        /// </summary>
        void Parse(string commandString);

        /// <summary>
        /// Parse and execute many lines of text that might be read from a file
        /// </summary>
        void Parse(string[] commandLines);
    }

    internal class CommandParser : ICommandParser
    {
        private readonly IToyRobot _toyRobot;

        public CommandParser(IToyRobot toyRobot) => _toyRobot = toyRobot;

        public void Parse(string commandString) => Parse(commandString.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray());

        public void Parse(string[] commandLines)
        {
            foreach (var commandLine in commandLines)
            {
                var parts = commandLine.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts == null || parts.Length == 0)
                {
                    continue;
                }
                ParseRecursive(parts);
            }
        }

        private IEnumerable<string> ParseRecursive(IEnumerable<string> commandLines)
        {
            if (commandLines == null || !commandLines.Any())
            {
                return Enumerable.Empty<string>();
            }

            var matching = _commands.Where(x => x.IsMatch(commandLines.First())).ToList();

            switch (matching.Count)
            {
                case 1:
                    return ParseRecursive(matching.First().Execute(_toyRobot, commandLines.Skip(1)));
                case 0:
                    _toyRobot.Error($"Found zero matching commands: '{commandLines.First()}'");
                    return ParseRecursive(commandLines.Skip(1));
                default:
                    _toyRobot.Error($"Found more than 1 matching command: {string.Join(",", matching.Select(x => x.GetType().Name))}");
                    return ParseRecursive(commandLines.Skip(1));
            }
        }

        private static readonly IEnumerable<ICommand> _commands = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterface(nameof(ICommand)) != null)
            .Select(x => Activator.CreateInstance(x))
            .Cast<ICommand>()
            .ToArray();
    }
}
