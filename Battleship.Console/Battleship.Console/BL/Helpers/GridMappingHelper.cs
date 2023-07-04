using Battleship.Console.Common;
using Battleship.Console.UI;

namespace Battleship.Console.BL.Helpers
{
    internal static class GridMappingHelper
    {
        public static GridFieldUIState[,] MapGridToUIGrid(GridFieldState[,] grid)
        {
            var uiGrid = new GridFieldUIState[Constants.GridSize, Constants.GridSize];

            for (int i = 0; i < Constants.GridSize; i++)
            {
                for (int j = 0; j < Constants.GridSize; j++)
                {
                    uiGrid[i, j] = MapStateToUIState(grid[i, j]);
                }
            }

            return uiGrid;
        }

        private static GridFieldUIState MapStateToUIState(GridFieldState logicalState)
        {
            return logicalState switch
            {
                GridFieldState.None => GridFieldUIState.None,
                GridFieldState.Empty => GridFieldUIState.Hidden,
                GridFieldState.Ship => GridFieldUIState.Hidden,
                GridFieldState.Miss => GridFieldUIState.Miss,
                GridFieldState.Hit => GridFieldUIState.Hit,
                GridFieldState.SinkingHit => GridFieldUIState.SinkingHit,
                _ => throw new Exception("Not supported GridFieldLogicalState"),
            };
        }
    }
}
