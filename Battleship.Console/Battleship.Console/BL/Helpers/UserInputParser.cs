using System.Text.RegularExpressions;
using Battleship.Console.BL.DTO;

namespace Battleship.Console.BL.Helpers
{
    internal static class UserInputParser
    {
        public const string UserInputFormatRegex = "^[A-J]([1-9]|10)$";

        public static bool Validate(string input)
        {
            return Regex.IsMatch(input, UserInputFormatRegex);
        }

        public static GridField Parse(string input)
        {
            return new GridField
            {
                Row = int.Parse(input.Substring(1)) - 1,
                Column = MapColumnCharacterToGridIndex(input[0])
            };
        }

        private static int MapColumnCharacterToGridIndex(char columnChar)
        {
            return Array.IndexOf(new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' }, columnChar);
        }
    }
}
