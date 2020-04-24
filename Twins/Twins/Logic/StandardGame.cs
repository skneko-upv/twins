using System.Linq;
using Twins.Logic;

namespace Twins.Models
{
    public class StandardGame : Game
    {
        const int GroupSize = 2;

        public Deck Deck { get; }

        public int RemainingMatches { get; private set; }

        public StandardGame(int height, int width, Deck deck)
        {
            var populationStrategy = new CyclicRandomPopulationStrategy(GroupSize, deck);
            Board = new Board(height, width, this, populationStrategy);

            Deck = deck;

            RemainingMatches = height * width / GroupSize;

            Board.CellFlipped += OnCellFlipped;
        }

        public override void Win()
        {
            throw new System.NotImplementedException();
        }

        public override void Resume()
        {
            throw new System.NotImplementedException();
        }

        public override void Pause()
        {
            throw new System.NotImplementedException();
        }

        public override void CheckMatches()
        {
            if (Board.FlippedCells.Count > GroupSize - 1)
            {
                TryMatch();
            }
        }

        private void OnCellFlipped(Board.Cell cell)
        {
            if (Board.ReferenceCard == null)
            {
                Board.ReferenceCard = cell.Card;
            }
        }

        public override bool TryMatch()
        {
            var reference = Board.FlippedCells.First().Card;
            bool isMatch = Board.FlippedCells.All(c => c.Card == reference);

            if (isMatch)
            {
                MatchSuccesses++;

                foreach (var cell in Board.FlippedCells) {
                    Board.SetCellKeepRevealed(cell.Row, cell.Column, true);
                }

                RemainingMatches--;
                if (RemainingMatches <= 0)
                {
                    Win();
                }
            }
            else
            {
                MatchFailures++;
            }

            Board.UnflipAllCells();
            Board.ReferenceCard = null;
            return isMatch;
        }
    }
}
