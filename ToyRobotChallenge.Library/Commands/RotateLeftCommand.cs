using System.Collections.Generic;

namespace ToyRobotChallenge.Library
{
    public class RotateLeftCommand : ICommand
    {
        public bool IsMatch(string token) => token == "LEFT";
        public IEnumerable<string> Execute(IToyRobot toyRobot, IEnumerable<string> args)
        {
            toyRobot.RotateLeft();
            return args;
        }
    }
}
