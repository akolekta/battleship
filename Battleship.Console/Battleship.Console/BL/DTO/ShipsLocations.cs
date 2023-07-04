using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console.BL.DTO
{
    internal class ShipsLocations
    {
        public List<GridField> Destroyer_1 { get; set; }
        public List<GridField> Destroyer_2 { get; set; }
        public List<GridField> Battleship { get; set; }
    }
}
