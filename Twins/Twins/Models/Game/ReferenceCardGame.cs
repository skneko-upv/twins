using System;
using System.Collections.Generic;
using System.Linq;

namespace Twins.Models.Game
{
    public class ReferenceCardGame : AbstractGame
    {
        public ReferenceCardGame(int height, int width, Deck deck, TimeSpan? timeLimit, TimeSpan? turnLimit, Board.Cell[,] cells = null, int level = 0)
            : base(height, width, deck, timeLimit, turnLimit, cells, level)
        {
            Board.ReferenceCard = RandomHiddenCard(Board.Cells);
        }

        public override IEnumerable<Board.Cell> TryMatch()
        {
            bool isMatch = Board.FlippedCells.All(c => c.Card.Equals(Board.ReferenceCard));
            return HandleMatchResult(isMatch);
        }

        public override void EndTurn()
        {
            base.EndTurn();

            if (!IsFinished && Board.ReferenceCard == null)
            {
                Board.ReferenceCard = RandomHiddenCard(Board.Cells);
            }
        }
    }
}
