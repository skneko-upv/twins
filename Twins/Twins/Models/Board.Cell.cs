namespace Twins.Models
{
    public partial class Board
    {
        public class Cell
        {
            public int Row { get; }

            public int Column { get; }

            public Card Card { get; }

            public bool KeepRevealed { get; set; }

            public int FlipCount { get; set; }

            public Cell(int row, int column, Card card, bool keepRevealed = false)
            {
                Row = row;
                Column = column;
                Card = card;
                KeepRevealed = keepRevealed;
            }
        }
    }
}
