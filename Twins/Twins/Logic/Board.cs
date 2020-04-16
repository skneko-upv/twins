namespace Twins.Models
{
    partial class Board
    {
        void Populate()
        {
            Cells = populationStrategy.Populate(Height, Width, Deck);
        }
    }
}
