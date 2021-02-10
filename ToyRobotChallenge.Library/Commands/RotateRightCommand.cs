using System.Collections.Generic;

namespace ToyRobotChallenge.Library
{
    public class RotateRightCommand : ICommand
    {
        public bool IsMatch(string token) => token == "RIGHT";
        public IEnumerable<string> Execute(IToyRobot toyRobot, IEnumerable<string> args)
        {
            toyRobot.RotateRight();
            return args;
        }
    }
}
