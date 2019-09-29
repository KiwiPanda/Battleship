using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.GameLogic
{
    public class Ship
    {
        public List<Square> Squares;
        public Ship(List<Square> squares)
        {
            Squares = squares;
        }

        public bool Sunk()
        {
            return Squares.All(s => s.IsHit);
        }
    }
}
