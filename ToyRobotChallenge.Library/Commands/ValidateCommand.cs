using System.Collections.Generic;
using System.Linq;

namespace ToyRobotChallenge.Library
{
    public class ValidateCommand : ICommand
    {
        public bool IsMatch(string token) => token == "VALIDATE";
        public IEnumerable<string> Execute(IToyRobot toyRobot, IEnumerable<string> args)
        {
            if (args.Count() == 0)
            {
                toyRobot.Error($"Incorrect arguments: '{args}'");
                return args;
            }

            if (!CommandUtility.TryParse(args.First(), out var result, out var error))
            {
                toyRobot.Error(error);
                return args.Skip(1);
            }

            toyRobot.Validate(result.Item1, result.Item2, result.Item3);
            return args.Skip(1);
        }
    }
}
