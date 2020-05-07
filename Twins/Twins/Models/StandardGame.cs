using System;
using System.Collections.Generic;
using System.Linq;

namespace Twins.Models
{
    public class StandardGame : Game
    {
        public StandardGame(int height, int width, Deck deck, TimeSpan timeLimit, TimeSpan turnLimit, Board.Cell[,] cells = null)
            : base(height, width, deck, timeLimit, turnLimit, cells)
        { }

        public override IEnumerable<Board.Cell> TryMatch()
        {
            Card reference = Board.FlippedCells.First().Card;
            bool isMatch = Board.FlippedCells.All(c => c.Card.Equals(reference));
            return HandleMatchResult(isMatch);
        }

        public override void EndTurn()
        {
            base.EndTurn();
            Board.ReferenceCard = null;
        }
    }
}
