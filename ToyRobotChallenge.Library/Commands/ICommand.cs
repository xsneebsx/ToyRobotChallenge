using System.Collections.Generic;

namespace ToyRobotChallenge.Library
{
    public interface ICommand
    {
        bool IsMatch(string token);
        IEnumerable<string> Execute(IToyRobot toyRobot, IEnumerable<string> args);
    }
}
