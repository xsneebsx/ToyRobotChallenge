using System.Collections.Generic;
using System.Linq;

namespace ToyRobotChallenge.Library
{
    public class EchoCommand : ICommand
    {
        public bool IsMatch(string token) => token == "echo";
        public IEnumerable<string> Execute(IToyRobot toyRobot, IEnumerable<string> args)
        {
            toyRobot.Echo(string.Join(",", args));
            return Enumerable.Empty<string>();
        }
    }
}
