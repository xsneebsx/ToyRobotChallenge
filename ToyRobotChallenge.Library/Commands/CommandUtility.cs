using System;

namespace ToyRobotChallenge.Library
{
    internal static class CommandUtility
    {
        public static bool TryParse(string str, out (uint, uint, Direction) result, out string error)
        {
            error = null;
            result = default((uint, uint, Direction));

            var split = str.Split(',');
            if (split.Length != 3)
            {
                return false;
            }

            var count = 0;
            foreach (var token in split)
            {
                switch (count)
                {
                    case 0:
                        if (!uint.TryParse(token, out result.Item1))
                        {
                            error = $"Could not parse command parameters, '{token}' is not a number";
                            return false;
                        }
                        break;
                    case 1:
                        if (!uint.TryParse(token, out result.Item2))
                        {
                            error = $"Could not parse command parameters, '{token}' is not a number";
                            return false;
                        }
                        break;
                    case 2:
                        if (!Enum.TryParse<Direction>(token, out result.Item3))
                        {
                            error = $"Could not parse command parameters, '{token}' is not a direction";
                            return false;
                        }
                        break;
                }
                count++;
            }

            error = null;
            return true;
        }
    }
}
