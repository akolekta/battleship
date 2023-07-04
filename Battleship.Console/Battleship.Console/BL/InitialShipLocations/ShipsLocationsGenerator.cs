using Battleship.Console.BL.DTO;
using Battleship.Console.Common;

namespace Battleship.Console.BL.InitialShipLocations
{
    internal interface IShipsLocationsGenerator
    {
        ShipsLocations GenerateRandomLocations();
    }

    internal class ShipsLocationsGenerator
    {
        private const int MaxNumberOfShipGenerationAttempts = 1000;

        private readonly IRandomNumbersGenerator _random;

        public ShipsLocationsGenerator(IRandomNumbersGenerator random)
        {
            _random = random;
        }


        public ShipsLocations GenerateRandomLocations()
        {
            var result = new ShipsLocations();

            result.Destroyer_1 = BuildShip(ShipSize.DestroyerSize);

            result.Destroyer_2 = BuildShipWithoutConflicts(ShipSize.DestroyerSize, result.Destroyer_1);

            result.Battleship = BuildShipWithoutConflicts(ShipSize.BattleshipSize, result.Destroyer_1.Union(result.Destroyer_2).ToList());

            return result;
        }

        private List<GridField> BuildShipWithoutConflicts(ShipSize shipSize, List<GridField> fieldsWithOtherShip)
        {
            for (int i = 0; i < MaxNumberOfShipGenerationAttempts; i++)
            {
                var shipPositionProposal = BuildShip(shipSize);

                if (!DoesShipConflictsWithOther(shipPositionProposal, fieldsWithOtherShip))
                {
                    return shipPositionProposal;
                }
            }

            throw new Exception("Cannot place ship on grid. We are unbelievably unlucky.");
        }

        private bool DoesShipConflictsWithOther(List<GridField> ship, List<GridField> fieldsWithOtherShip)
        {
            return ship.Any(shipField => fieldsWithOtherShip.Any(otherShipField => otherShipField.Row == shipField.Row && otherShipField.Column == shipField.Column));
        }

        private List<GridField> BuildShip(ShipSize shipSize)
        {
            var isVertical = _random.GetRandomIntBetween(0, 2) == 0;

            var beginningOfTheShip = GenerateBeginningOfTheShip(isVertical ? ShipAlignment.Vertically : ShipAlignment.Horizontally, shipSize);

            var shipFields = new List<GridField> { beginningOfTheShip };

            if (isVertical)
            {
                for (var i = 1; i < (int)shipSize; i++)
                {
                    shipFields.Add(new GridField { Row = beginningOfTheShip.Row + i, Column = beginningOfTheShip.Column });
                }
            }
            else
            {
                for (var i = 1; i < (int)shipSize; i++)
                {
                    shipFields.Add(new GridField { Row = beginningOfTheShip.Row, Column = beginningOfTheShip.Column + i });
                }
            }

            return shipFields;
        }


        /// <summary>
        /// Returns beginning of ship which is first field on the left when ship placed horizontally and first field on the top, when placed vertically.
        /// </summary>
        /// <returns></returns>
        private GridField GenerateBeginningOfTheShip(ShipAlignment shipAlignment, ShipSize shipSize)
        {
            var zeroToGridSizeRandom = _random.GetRandomIntBetween(0, Constants.GridSize);
            var zeroToGridSizeRandomLimitedByShipSize = _random.GetRandomIntBetween(0, Constants.GridSize - (int)shipSize + 1);

            return shipAlignment switch
            {
                ShipAlignment.Horizontally => new GridField { Row = zeroToGridSizeRandom, Column = zeroToGridSizeRandomLimitedByShipSize },
                ShipAlignment.Vertically => new GridField { Row = zeroToGridSizeRandomLimitedByShipSize, Column = zeroToGridSizeRandom },
                _ => throw new Exception("Not supported ShipAlignment"),
            };
        }

        private enum ShipAlignment
        {
            None = 0,
            Vertically = 1,
            Horizontally = 2
        }

        private enum ShipSize
        {
            None = 0,
            DestroyerSize = 4,
            BattleshipSize = 5,
        }
    }
}
