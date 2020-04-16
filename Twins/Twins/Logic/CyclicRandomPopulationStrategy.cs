using System.Collections.Generic;
using System.Linq;
using Twins.Models;
using static Twins.Models.Board;

namespace Twins.Logic
{
    class CyclicRandomPopulationStrategy : IBoardPopulationStrategy
    {
        /// <summary>
        /// The size of the groups of cards to match.
        /// </summary>
        /// <example>
        /// For example, <c>2</c> for pairs, <c>3</c> for triples...
        /// </example>
        public int GroupSize { get; private set; }

        public CyclicRandomPopulationStrategy(int groupSize)
        {
            GroupSize = groupSize;
        }

        public Cell[,] Populate(int height, int width, Deck deck)
        {
            Cell[,] cells = new Cell[height, width];
            List<(int, int)> emptyPositions = Enumerable.Range(0, height - 1)
                                            .Zip(Enumerable.Range(0, width - 1),
                                                 (r, c) => (r, c))
                                            .ToList();
            List<Card> availableCards = deck.Cards;

            while (emptyPositions.Count >= 2)
            {
                if (!availableCards.Any())
                {
                    // If there are no more cards available to make pairs, we
                    // reuse the same deck to introduce duplicate pairs.
                    availableCards = deck.Cards;
                }
                var card = availableCards.PickAndRemoveRandom();

                for (int i = 0; i < GroupSize; i++)
                {
                    var (row, column) = emptyPositions.PickAndRemoveRandom();
                    cells[row, column] = new Cell(row, column, card);
                }
            }

            return cells;
        }
    }
}
