using System.Collections.Generic;

namespace ToyRobotChallenge.Library
{
    public class MoveCommand : ICommand
    {
        public bool IsMatch(string token) => token == "MOVE";
        public IEnumerable<string> Execute(IToyRobot toyRobot, IEnumerable<string> args)
        {
            toyRobot.Move();
            return args;
        }
    }
}
