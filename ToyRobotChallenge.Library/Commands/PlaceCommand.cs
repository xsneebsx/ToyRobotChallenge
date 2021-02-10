using System.Collections.Generic;
using System.Linq;

namespace ToyRobotChallenge.Library
{
    public class PlaceCommand : ICommand
    {
        public bool IsMatch(string token) => token == "PLACE";
        public IEnumerable<string> Execute(IToyRobot toyRobot, IEnumerable<string> args)
        {
            if (args.Count() == 0)
            {
                toyRobot.Error($"Incorrect arguments: '{args}'");
                return args;
            }

            if (!CommandUtility.TryParse(args.First(), out var result, out var error))
            {
                toyRobot.Error($"Incorrect arguments: '{args}'");
                return args.Skip(1);
            }

            toyRobot.Place(result.Item1, result.Item2, result.Item3);
            return args.Skip(1);
        }
    }
}
