using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading;
using Twins.Components;
using Twins.Models;
using Twins.Models.Game;
using Twins.Models.Strategies;

namespace Twins.Tests
{
    [TestClass]
    public class StandardGameTests
    {
        private static IGame game;

        [TestInitialize]
        public void CreateGame()
        {
            int height = 2, width = 2;
            Deck deck = BuiltInDecks.Animals.Value;
            game = new StandardGame(
                height,
                width,
                deck,
                timeLimit: null,
                turnLimit: null,
                cells: new PredictablePopulationStrategy(2, deck).Populate(height, width),
                level: 0);

            game.Board.CellFlipped += (cell) => { };                                // model enforces events must have subscribers
            game.Board.CellKeepRevealedStatusChanged += (cell, revealed) => { };
        }

        [TestMethod]
        public void Match_WithEqualCards_IsSuccess()
        {
            game.Board.FlipCell(0, 0);
            game.Board.FlipCell(0, 1);
            Assert.AreEqual(2, game.TryMatch().Count());
        }

        [TestMethod]
        public void Match_WithDistinctCards_IsFail()
        {
            game.Board.FlipCell(0, 0);
            game.Board.FlipCell(1, 1);
            Assert.AreEqual(0, game.TryMatch().Count());
        }

        [TestMethod]
        public void FlipCell_WhenCellFacingDown_WillFaceUp()
        {
            game.Board.FlipCell(0, 1);
            Assert.AreEqual(1, game.Board.FlippedCells.Count);

            Board.Cell cell = game.Board.FlippedCells.First();
            Assert.AreEqual(0, cell.Row);
            Assert.AreEqual(1, cell.Column);
        }

        [TestMethod]
        public void FlipCell_WhenKeptRevealed_ThrowsException()
        {
            game.Board.Cells[0].KeepRevealed = true;

            Assert.ThrowsException<InvalidOperationException>(() => game.Board.FlipCell(0, 0));
        }

        [TestMethod]
        public void Match_WhenAllRevealed_Victory()
        {
            AutoResetEvent barrier = new AutoResetEvent(false);
            GameResult result = null;

            game.GameEnded += (res) =>
            {
                result = res;
                barrier.Set();
            };

            game.Board.FlipCell(0, 0);
            game.Board.FlipCell(0, 1);
            game.TryMatch();
            game.Board.UnflipAllCells();

            game.Board.FlipCell(1, 0);
            game.Board.FlipCell(1, 1);
            game.TryMatch();

            Assert.IsTrue(barrier.WaitOne(TimeSpan.FromSeconds(5)),
                "No se ha disparado el evento de final.");
            Assert.IsNotNull(result,
                "No se ha obtenido resultado del final.");
            Assert.IsTrue(result.IsVictory,
                "El resultado no es de victoria.");
        }
    }
}
