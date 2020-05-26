namespace Twins.Models.Strategies
{
    public interface IBoardPopulationStrategy
    {
        Board.Cell[,] Populate(int height, int width);
    }
}
