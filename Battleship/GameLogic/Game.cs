using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.GameLogic
{
    public class Game
    {
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Game()
        {
            Player1 = new Player();
            Player2 = new Player();
        }
    }
}
