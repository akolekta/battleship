using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console.UI
{
    internal enum GridFieldState
    {
        None = 0,
        Empty = 1,
        Ship = 2,
        Miss = 3,
        Hit = 4,
        SinkingHit = 5,
    }
}
