using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Console.UI
{
    internal enum GridFieldUIState
    {
        None = 0,
        Hidden = 1,
        Miss = 2,
        Hit = 3,
        SinkingHit = 4,
    }
}
