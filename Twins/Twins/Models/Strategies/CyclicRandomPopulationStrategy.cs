using System.Collections.Generic;
using System.Linq;
using static Twins.Models.Board;

namespace Twins.Models.Strategies
{
    public class CyclicRandomPopulationStrategy : IBoardPopulationStrategy
    {
        /// <summary>
        /// The size of the groups of cards to match.
        /// </summary>
        /// <example>
        /// For example, <c>2</c> for pairs, <c>3</c> for triples...
        /// </example>
        public int GroupSize { get; }

        public Deck Deck { get; }

        public CyclicRandomPopulationStrategy(int groupSize, Deck deck)
        {
            GroupSize = groupSize;
            Deck = deck;
        }

        public Cell[,] Populate(int height, int width)
        {
            Cell[,] cells = new Cell[height, width];
            List<(int, int)> emptyPositions = new List<(int, int)>(height * width);
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    emptyPositions.Add((r, c));
                }
            }

            IList<Card> availableCards = Deck.Cards.Clone();

            while (emptyPositions.Count >= 2)
            {
                if (!availableCards.Any())
                {
                    // If there are no more cards available to make pairs, we
                    // reuse the same deck to introduce duplicate pairs.
                    availableCards = Deck.Cards.Clone();
                }
                Card card = availableCards.PickAndRemoveRandom();

                for (int i = 0; i < GroupSize; i++)
                {
                    (int row, int column) = emptyPositions.PickAndRemoveRandom();
                    cells[row, column] = new Cell(row, column, card);
                }
            }

            return cells;
        }
    }
}
