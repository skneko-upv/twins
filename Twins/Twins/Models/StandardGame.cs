using System.Linq;
using Twins.Logic;

namespace Twins.Models
{
    class StandardGame : Game
    {
        const int GroupSize = 2;

        int remainingMatches;

        public StandardGame(int height, int width, Deck deck)
        {
            var populationStrategy = new CyclicRandomPopulationStrategy(GroupSize);
            Board = new Board(height, width, this, deck, populationStrategy);

            remainingMatches = height * width / GroupSize;

            Board.CellFlipped += OnCellFlipped;
        }

        public void OnCellFlipped(Board.Cell cell)
        {
            if (Board.FlippedCells.Count >= GroupSize - 1)
            {
                TryMatch();
            }
            else
            {
                Board.ReferenceCard = cell.Card;
            }
        }

        public void TryMatch()
        {
            var reference = Board.FlippedCells.First().Card;
            bool isMatch = Board.FlippedCells.All(c => c.Card == reference);

            if (isMatch)
            {
                foreach (var cell in Board.FlippedCells) {
                    Board.SetCellKeepRevealed(cell.Row, cell.Column, true);
                }

                remainingMatches--;
                if (remainingMatches <= 0)
                {
                    Win();
                }
                // TODO
            }
            else
            {
                // TODO
            }

            Board.UnflipAllCells();
            Board.ReferenceCard = null;
        }

        public void Win()
        {
            // TODO
        }
    }
}
