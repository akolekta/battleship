namespace Battleship.Console
{
    using Battleship.Console.BL;
    using Battleship.Console.BL.Helpers;
    using Battleship.Console.BL.InitialShipLocations;
    using Battleship.Console.UI;
    using System;

    public class Program
    {
        internal static void Main(string[] args)
        {
            var game = new BattleshipGameEngine(
                new ShipsLocationsGenerator(new RandomNumbersGenerator()).GenerateRandomLocations(),
                new BattleshipDisplayEngine());

            game.Run();

            while (!game.IsGameFinished)
            {
                var userInput = Console.ReadLine();

                if(!UserInputParser.Validate(userInput))
                {
                    Console.WriteLine("Wrong input format. Correct format is <columnName><rowNumber>. Example : B7, D10.");

                    continue;
                }

                var gridField = UserInputParser.Parse(userInput);

                if (!game.IsShotPlaceValid(gridField))
                {
                    Console.WriteLine("This field is already revealed. Doesn't make sense to shot this place again.");

                    continue;
                }


                game.AcceptUserInput(gridField);
            }

            Console.WriteLine("Game completed.");
        }
    }
}