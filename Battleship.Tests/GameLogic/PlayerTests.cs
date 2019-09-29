using Microsoft.VisualStudio.TestTools.UnitTesting;
using Battleship.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleship.GameAction;

namespace Battleship.GameLogic.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void AddShipTest()
        {
            var player = new Player();
            var response = player.AddShip(2, 2, 2, Direction.Down);
            Assert.IsTrue(response is SuccessResponse);

            response = player.AddShip(10, 5, 5, Direction.Right);
            Assert.IsTrue(response is SuccessResponse);

            Assert.AreEqual(2, player.Ships.Count());
            Assert.IsTrue(player.Board[9, 7].HasShip);
        }

        [TestMethod()]
        public void AddShipErrorTest()
        {
            var player = new Player();
            var response = player.AddShip(1, 1, 2, Direction.Left);
            Assert.IsTrue(response is ErrorResponse);

            response = player.AddShip(5, 5, 5, Direction.Right);
            Assert.IsTrue(response is SuccessResponse);

            response = player.AddShip(5, 7, 1, Direction.Right);
            Assert.IsTrue(response is ErrorResponse);
        }

        [TestMethod()]
        public void AttackHitTest()
        {
            var player = new Player();
            player.AddShip(1, 1, 1, Direction.Left);
            var response = player.Attack(1, 1);
            Assert.IsTrue(response is HitResponse);
        }

        [TestMethod()]
        public void AttackMissTest()
        {
            var player = new Player();
            player.AddShip(1, 1, 1, Direction.Left);
            var response = player.Attack(2, 2);
            Assert.IsTrue(response is MissResponse);
        }

        [TestMethod()]
        public void AttackErrorTest()
        {
            var player = new Player();
            player.AddShip(1, 1, 1, Direction.Left);
            player.Attack(1, 1);
            var response = player.Attack(1, 1);
            Assert.IsTrue(response is ErrorResponse);
        }
    }
}