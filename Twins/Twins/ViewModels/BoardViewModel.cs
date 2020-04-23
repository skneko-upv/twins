using System.Collections.ObjectModel;
using Twins.Components;
using Twins.Models;

namespace Twins.ViewModels
{
    public class BoardViewModel
    {
        public class CardComponentMatrix {
            readonly CardComponent[,] cardComponents;

            public CardComponentMatrix(int height, int width)
            {
                cardComponents = new CardComponent[height, width];
            }

            public CardComponent this[Board.Cell cell]
                => cardComponents[cell.Row, cell.Column];
        }

        public ObservableCollection<Board.Cell> FlippedCells { get; }
        
        public CardComponentMatrix CardComponents { get; }

        public Board Board { get; }

        public BoardViewModel(Board board) {
            Board = board;

            foreach (var cell in Board.Cells)
            {
                CardComponents = new CardComponentMatrix(Board.Height, Board.Width);
            }

            Board.CellFlipped += async cell => {
                await CardComponents[cell].Flip();
            };

            Board.CellUnflipped += async cell =>
            {
                await CardComponents[cell].Unflip();
            };

            Board.CellKeepRevealedStatusChanged += async (cell, reveal) =>
            {
                if (reveal)
                {
                    await CardComponents[cell].Flip();
                }
                else
                {
                    await CardComponents[cell].Unflip();
                }
            };

            Board.ReferenceCardChanged += card =>
            {
                // TODO
            };
        }
    }
}
