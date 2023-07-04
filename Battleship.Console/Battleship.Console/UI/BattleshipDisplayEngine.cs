namespace Battleship.Console.UI
{
    using Battleship.Console.Common;
    using System;
    using System.Text;

    internal interface IBattleshipDisplayEngine
    {
        void DisplaySquareGrid(GridFieldUIState[,] grid);
    }

    internal class BattleshipDisplayEngine : IBattleshipDisplayEngine
    {
        private const string ColumnNamesRow = "   A B C D E F G H I J";

        public void DisplaySquareGrid(GridFieldUIState[,] grid)
        {
            ValidateGrid(grid);

            Console.Clear();

            Console.WriteLine(ColumnNamesRow);

            for (int i = 0; i < Constants.GridSize; i++)
            {
                var row = new StringBuilder();

                row.Append(GetRowIndex(i));

                for(int j = 0; j < Constants.GridSize; j++)
                {
                    row.Append(MapStateToDisplayCharacter(grid[i, j]) + " ");
                }

                Console.WriteLine(row.ToString());
            }

            DisplayDescription();
        }

        private void ValidateGrid(GridFieldUIState[,] grid)
        {
            if (grid.GetLength(0) != Constants.GridSize)
            {
                throw new ArgumentException("First dimension size of input grid has to be 10.");
            }

            if (grid.GetLength(1) != Constants.GridSize)
            {
                throw new ArgumentException("Second dimension size of input grid has to be 10.");
            }
        }

        private string GetRowIndex(int gridIndex)
        {
            return (gridIndex + 1).ToString().PadLeft(2) + " ";
        }

        private string MapStateToDisplayCharacter(GridFieldUIState state)
        {
            return state switch
            {
                GridFieldUIState.Hidden => " ",
                GridFieldUIState.Hit => "H",
                GridFieldUIState.Miss => "O",
                GridFieldUIState.SinkingHit => "X",
                _ => throw new Exception("Not supported GridFieldUIState.")
            };
        }

        private void DisplayDescription()
        {
            Console.WriteLine();
            Console.WriteLine("Description:");
            Console.WriteLine("O - Missed shot.");
            Console.WriteLine("H - Ship hit.");
            Console.WriteLine("X - Ship sinking shot.");
        }
    }
}
