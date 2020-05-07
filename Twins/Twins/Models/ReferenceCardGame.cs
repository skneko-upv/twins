using System;
using System.Collections.Generic;
using System.Linq;
using static Twins.Utils.CollectionExtensions;

namespace Twins.Models
{
    public class ReferenceCardGame : Game
    {
        public ReferenceCardGame(int height, int width, Deck deck, TimeSpan timeLimit, TimeSpan turnLimit, Board.Cell[,] cells = null)
            : base(height, width, deck, timeLimit, turnLimit, cells)
        {
            Board.ReferenceCard = RandomHiddenCard();
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
                Board.ReferenceCard = RandomHiddenCard();
            }
        }

        Card RandomHiddenCard()
            => Board.Cells
                    .Where(c => !c.KeepRevealed)
                    .Select(c => c.Card)
                    .ToList()
                    .PickAndRemoveRandom();
    }
}
