namespace Twins.Models
{
    partial class Board
    {
        public class Position
        {
            public int Row { get; }
            public int Column { get; }

            public Position(int row, int column)
            {
                Row = row;
                Column = column;
            }
        }
    }
}
