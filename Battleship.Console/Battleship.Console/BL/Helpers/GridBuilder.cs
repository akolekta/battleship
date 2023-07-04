using Battleship.Console.BL.DTO;
using Battleship.Console.Common;
using Battleship.Console.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console.BL.Helpers
{
    internal static class GridBuilder
    {
        public static GridFieldState[,] BuildInitialGrid(ShipsLocations shipsLocations)
        {
            GridFieldState[,] grid = new GridFieldState[Constants.GridSize, Constants.GridSize];

            for (int i = 0; i < Constants.GridSize; i++)
            {
                for (int j = 0; j < Constants.GridSize; j++)
                {
                    grid[i, j] = GridFieldState.Empty;
                }
            }

            MarkShipLocationOnGrid(grid, shipsLocations.Destroyer_1);
            MarkShipLocationOnGrid(grid, shipsLocations.Destroyer_2);
            MarkShipLocationOnGrid(grid, shipsLocations.Battleship);

            return grid;
        }


        private static void MarkShipLocationOnGrid(GridFieldState[,] grid, List<GridField> ship)
        {
            foreach (var field in ship)
            {
                grid[field.Row, field.Column] = GridFieldState.Ship;
            }
        }
    }
}
