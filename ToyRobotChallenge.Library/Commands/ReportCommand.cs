using System.Collections.Generic;

namespace ToyRobotChallenge.Library
{
    public class ReportCommand : ICommand
    {
        public bool IsMatch(string token) => token == "REPORT";
        public IEnumerable<string> Execute(IToyRobot toyRobot, IEnumerable<string> args)
        {
            toyRobot.Report();
            return args;
        }
    }
}
