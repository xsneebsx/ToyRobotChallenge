using System.Collections.Generic;
using System.Linq;

namespace ToyRobotChallenge.Library
{
    public class CommentCommand : ICommand
    {
        public bool IsMatch(string token) => token == "#";
        public IEnumerable<string> Execute(IToyRobot toyRobot, IEnumerable<string> args) => Enumerable.Empty<string>();
    }
}
