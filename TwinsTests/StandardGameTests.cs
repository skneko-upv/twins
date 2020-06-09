using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Twins.Components;
using Twins.Models.Game;
using Twins.Models.Strategies;

namespace Twins.Tests
{
    [TestClass]
    public class StandardGameTests
    {
        static IGame game;

        [TestInitialize]
        public void CreateGame()
        {
            int height = 2, width = 2;
            var deck = BuiltInDecks.Animals.Value;
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

            var cell = game.Board.FlippedCells.First();
            Assert.AreEqual(0, cell.Row);
            Assert.AreEqual(1, cell.Column);
        }

        [TestMethod]
        public void FlipCell_WhenKeptRevealed_ThrowsException()
        {
            game.Board.Cells[0].KeepRevealed = true;

            Assert.ThrowsException<InvalidOperationException>(() => game.Board.FlipCell(0, 0));
        }
    }
}
