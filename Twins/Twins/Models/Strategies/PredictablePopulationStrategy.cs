using System.Collections.Generic;
using System.Linq;
using static Twins.Models.Board;
using static Twins.Utils.CollectionExtensions;

namespace Twins.Models.Strategies
{
    public class PredictablePopulationStrategy : IBoardPopulationStrategy
    {
        /// <summary>
        /// The size of the groups of cards to match.
        /// </summary>
        /// <example>
        /// For example, <c>2</c> for pairs, <c>3</c> for triples...
        /// </example>
        public int GroupSize { get; }

        public Deck Deck { get; }

        public PredictablePopulationStrategy(int groupSize, Deck deck)
        {
            GroupSize = groupSize;
            Deck = deck;
        }

        public Cell[,] Populate(int height, int width)
        {
            Cell[,] cells = new Cell[height, width];

            IList<Card> availableCards = Deck.Cards.Repeat(GroupSize);

            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    var card = availableCards[(width * row + column) % availableCards.Count];
                    cells[row, column] = new Cell(row, column, card);
                }
            }

            return cells;
        }
    }
}
