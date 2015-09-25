using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void StartGameCommunicatioTest()
        {
           /* Game game = new Game("Luca");
            game.SetOpponent("Francesco");

            Assert.AreEqual(game.GetShamanName(), "Luca");
            Assert.AreEqual(game.GetOppenentName(), "Francesco");*/
        }

        [TestMethod()]
        public void RoundDefinitionTest()
        {
        /*    Game game = new Game("Luca");
            game.SetOpponent("Francesco");

            game.ThrowDice(6);

            var opponentThrow = 5;
            game.OnOpponentDiceResult(opponentThrow);

            Assert.IsTrue(game.isMyRound());*/
        }

        [TestMethod()]
        public void RoundDefinitionWhenResultsAreEqualTest()
        {
        /*    Game game = new Game("Luca");
            game.SetOpponent("Francesco");

            game.ThrowDice(5);

            var opponentThrow = 5;
            game.OnOpponentDiceResult(opponentThrow);

            Assert.IsTrue(game.isMyRound());*/
        }

        [TestMethod()]
        public void ThrowDiceTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LoadBibliothecaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GameStartedTest()
        {
            Assert.Fail();
        }
    }
}