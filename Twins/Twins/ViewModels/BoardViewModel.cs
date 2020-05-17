using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Twins.Components;
using Twins.Models;

namespace Twins.ViewModels
{
    public class BoardViewModel : ViewModelBase
    {
        public class CardComponentMatrix
        {
            private readonly CardComponent[,] cardComponents;

            public CardComponentMatrix(int height, int width)
            {
                cardComponents = new CardComponent[height, width];
            }

            public CardComponent this[Board.Cell cell] {
                get => cardComponents[cell.Row, cell.Column];
                set => cardComponents[cell.Row, cell.Column] = value;
            }

        }

        public CardComponentMatrix CardComponents { get; }

        public Board Board { get; }

        public bool InteractionAllowed { get; private set; }

        private int lastScoreChange;

        public BoardViewModel(Board board)
        {
            Board = board;

            CardComponents = new CardComponentMatrix(Board.Height, Board.Width);
            foreach (Board.Cell cell in Board.Cells)
            {
                CardComponent component = new CardComponent(cell.Card);
                component.Clicked += () => OnCellClicked(cell);
                CardComponents[cell] = component;
            }

            Board.CellFlipped += OnCellFlipped;
            Board.CellUnflipped += OnCellUnflipped;
            Board.CellKeepRevealedStatusChanged += OnCellKeepRevealedStatusChanged;
            Board.Game.TurnTimedOut += OnTurnTimedOut;

            Board.Game.Resume();
            InteractionAllowed = true;
            lastScoreChange = Board.Game.Score.Value;
        }

        private void OnTurnTimedOut()
        {
            Board.Game.Pause();
            Board.Game.EndTurn();
            Board.Game.Resume();
        }

        private void OnCellsMatched(IEnumerable<Board.Cell> cells)
        {
            foreach (Board.Cell cell in cells)
            {
                _ = CardComponents[cell].Matched();
            }
        }

        private async void OnCellFlipped(Board.Cell cell)
        {
            await CardComponents[cell].Flip();
        }

        private async void OnCellUnflipped(Board.Cell cell)
        {
            await CardComponents[cell].Unflip();
        }

        private async void OnCellKeepRevealedStatusChanged(Board.Cell cell, bool reveal)
        {
            if (reveal)
            {
                await CardComponents[cell].Flip();
            }
            else
            {
                await CardComponents[cell].Unflip();
            }
        }
        private async void OnCellClicked(Board.Cell cell)
        {
            if (!InteractionAllowed)
            {
                return;
            }

            try
            {
                Board.FlipCell(cell.Row, cell.Column);
            }
            catch (InvalidOperationException) { }

            if (Board.Game.ShouldTryMatch())
            {
                InteractionAllowed = false;
                Board.Game.Pause();

                IEnumerable<Board.Cell> matched = Board.Game.TryMatch();

                var score = Board.Game.Score.Value;
                if (matched.Any())
                {
                    OnCellsMatched(matched);
                    
                    Board.ReferenceCard = null;
                    await CardComponents[cell].ShowGreenPoints(score - lastScoreChange);
                    await Task.Delay(250);
                }
                else
                {
                    await CardComponents[cell].ShowRedPoints(score - lastScoreChange);
                    await Task.Delay(1000);

                }
                lastScoreChange = score;
                if (Board.Game.ShouldEndTurn())
                {
                    Board.Game.EndTurn();
                }

                if (!Board.Game.IsFinished)
                {
                    InteractionAllowed = true;
                    Board.Game.Resume();
                }
            }
        }

    }
}
