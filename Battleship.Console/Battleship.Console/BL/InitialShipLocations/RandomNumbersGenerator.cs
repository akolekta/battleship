using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console.BL.InitialShipLocations
{
    internal interface IRandomNumbersGenerator
    {
        int GetRandomIntBetween(int start, int end);
    }

    internal class RandomNumbersGenerator : IRandomNumbersGenerator
    {
        private readonly Random _random;

        public RandomNumbersGenerator()
        {
            _random = new Random();
        }

        public int GetRandomIntBetween(int start, int end)
        {
            return _random.Next(start, end);
        }
    }
}
