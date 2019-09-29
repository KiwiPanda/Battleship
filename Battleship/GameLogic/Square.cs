using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.GameLogic
{
    public class Square
    {
        public Ship Ship { get; set; }
        public bool Attacked { get; set; }
        public int XPosition { get; private set; }
        public int YPosition { get; private set; }

        public bool IsHit
        {
            get
            {
                return Ship != null && Attacked;
            }
        }

        public bool HasShip
        {
            get
            {
                return Ship != null;
            }
        }

        public Square(int xPosition, int yPosition)
        {
            XPosition = xPosition;
            YPosition = yPosition;
        }

    }
}
