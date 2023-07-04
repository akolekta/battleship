using Battleship.Console.BL.DTO;
using Battleship.Console.BL.Helpers;
using Battleship.Console.Common;
using Battleship.Console.UI;

namespace Battleship.Console.BL
{
    internal class BattleshipGameEngine
    {
        private readonly ShipsLocations _shipsLocations;
        private readonly IBattleshipDisplayEngine _battleshipDisplayEngine;

        private GridFieldState[,] _grid;
        private int _sinkingHitCounter;

        public BattleshipGameEngine(ShipsLocations shipsLocations, IBattleshipDisplayEngine battleshipDisplayEngine)
        {
            _shipsLocations = shipsLocations;

            _battleshipDisplayEngine = battleshipDisplayEngine;

            _sinkingHitCounter = 0;
        }

        public bool IsGameFinished
        {
            get => _sinkingHitCounter == 3;
        }

        public void Run()
        {
            _grid = GridBuilder.BuildInitialGrid(_shipsLocations);

            RefreshUI();
        }

        public void AcceptUserInput(GridField usersShot)
        {
            ValidateUserInput(usersShot);

            if (_grid[usersShot.Row, usersShot.Column] == GridFieldState.Empty)
            {
                _grid[usersShot.Row, usersShot.Column] = GridFieldState.Miss;
            }

            if (_grid[usersShot.Row, usersShot.Column] == GridFieldState.Ship)
            {
                if (IsSinkingHit(usersShot.Row, usersShot.Column))
                {
                    _grid[usersShot.Row, usersShot.Column] = GridFieldState.SinkingHit;

                    _sinkingHitCounter++;
                }
                else
                {
                    _grid[usersShot.Row, usersShot.Column] = GridFieldState.Hit;
                }
            }

            RefreshUI();
        }

        public bool IsShotPlaceValid(GridField usersShot)
        {
            var state = _grid[usersShot.Row, usersShot.Column];

            return state is GridFieldState.Empty or GridFieldState.Ship;
        }

        private void ValidateUserInput(GridField usersShot)
        {
            if (usersShot.Row < 0 || usersShot.Row > Constants.GridSize || usersShot.Column < 0 || usersShot.Column > Constants.GridSize)
            {
                throw new Exception("Users shot out of grid.");
            }

            if (!IsShotPlaceValid(usersShot))
            {
                throw new Exception("User shots in already revealed field.");
            }
        }

        private bool IsSinkingHit(int row, int column)
        {
            return IsSinkingHit(_shipsLocations.Destroyer_1, row, column) ||
                   IsSinkingHit(_shipsLocations.Destroyer_2, row, column) ||
                   IsSinkingHit(_shipsLocations.Battleship, row, column);
        }

        private bool IsSinkingHit(List<GridField> shipFields, int row, int column)
        {
            if (!shipFields.Any(x => x.Row == row && x.Column == column))
            {
                return false;
            }

            var restOfShipFields = shipFields.Where(x => !(x.Row == row && x.Column == column));

            return restOfShipFields.All(x => _grid[x.Row, x.Column] == GridFieldState.Hit);
        }

        private void RefreshUI()
        {
            _battleshipDisplayEngine.DisplaySquareGrid(UiGrid);
        }

        private GridFieldUIState[,] UiGrid
        {
            get
            {
                return GridMappingHelper.MapGridToUIGrid(_grid);
            }
        }
    }
}
