using Battleship.GameAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.GameLogic
{
    public class Player
    {
        public List<Ship> Ships = new List<Ship>();

        /// <summary>
        /// 2D Array representing the board. Game Square [1,1] corresponds to Matrix position [0,0]
        ///                 Game Square                  
        /// [1,1] [1,2] ................... [1,10]     
        /// [2,1] [2,2] ................... [2,10]     
        /// ...................
        /// [10,1] [10,2] ................. [10,10]     
        /// </summary>
        public Square[,] Board = new Square[10, 10];

        public Player()
        {
            //Initialise game board
            for (int x = 1; x <= 10; x++)
            {
                for (int y = 1; y <= 10; y++)
                {
                    var square = new Square(x, y);
                    Board[x - 1, y - 1] = square;
                }
            }
        }

        /// <summary>
        /// Add ship on board squares and player
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <returns>SuccessResponse if successful</returns>
        public ResponseBase AddShip(int x, int y, int size, Direction direction)
        {
            //Secure game squares to place ship
            var shipSquares = new List<Square>();
            for (int i = 0; i < size; i++)
            {
                try
                {
                    int xOffset = 0;
                    int yOffset = 0;
                    switch (direction)
                    {
                        case Direction.Up:
                            xOffset = xOffset - i;
                            break;
                        case Direction.Down:
                            xOffset = xOffset + i;
                            break;
                        case Direction.Left:
                            yOffset = yOffset - i;
                            break;
                        case Direction.Right:
                            yOffset = yOffset + i;
                            break;
                    }

                    var square = Board[x + xOffset - 1, y + yOffset - 1];
                    shipSquares.Add(square);
                }
                catch (IndexOutOfRangeException)
                {
                    return new ErrorResponse("Unable to place ship outside of game square");
                }
            }

            //Check if other ships are already taking up space
            if (shipSquares.Any(s => s.HasShip))
            {
                var existintShip = shipSquares.First(s => s.HasShip);
                return new ErrorResponse(string.Format("Unable to place ship due to existing ship on [{0},{1}]", existintShip.XPosition, existintShip.YPosition));
            }
            else
            {
                var ship = new Ship(shipSquares);   //Create ship
                foreach (var s in shipSquares)      //Add ship to squares
                {
                    s.Ship = ship;
                }
                Ships.Add(ship);                    //Add ship to player
            }
            return new SuccessResponse();
        }

        /// <summary>
        /// Performs an attack on the player, returns hit/miss response
        /// </summary>
        /// <param name="x">Attack X-Coordinate</param>
        /// <param name="y">Attack Y-Coordinate</param>
        /// <returns>Attack Response</returns>
        public ResponseBase Attack(int x, int y)
        {
            var square = Board[x - 1, y - 1];
            if (square.Attacked)
            {
                return new ErrorResponse(string.Format("[{0},{1}] is already attacked", x.ToString(), y.ToString()));
            }
            else
            {
                square.Attacked = true;
                if (square.HasShip)
                {
                    return new HitResponse();
                }
                else
                {
                    return new MissResponse();
                }
            }

        }

        public bool Lost()
        {
            return Ships.All(s => s.Sunk());
        }
    }
}
