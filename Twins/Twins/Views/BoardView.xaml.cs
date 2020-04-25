using System;
using Twins.Models;
using Twins.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardView : ContentPage
    {
        private double UsableBoardAreaSize { get { return Math.Min(boardArea.Width, boardArea.Height); } }

        public BoardView(Board board)
        {
            InitializeComponent();
            BindingContext = new BoardViewModel(board);

            FillBoard(board.Height, board.Width);

            boardArea.LayoutChanged += EnforceBoardAspectRatio;

            board.ReferenceCardChanged += OnReferenceCardChanged;
            referenceCard.Clicked += () => {};
        }

        private void FillBoard(int height, int width)
        {
            for (int i = 0; i < height; i++)
            {
                board.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < width; i++)
            {
                board.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            var viewModel = (BoardViewModel)BindingContext;
            foreach (var cell in viewModel.Board.Cells)
            {
                board.Children.Add(viewModel.CardComponents[cell], cell.Row, cell.Column);
            }
        }

        private void EnforceBoardAspectRatio(object sender = null, EventArgs e = null)
        {
            if (UsableBoardAreaSize > 0) {
                var usableWidth = boardArea.Width;
                var usableHeight = boardArea.Height;

                var columns = board.ColumnDefinitions.Count;
                var rows = board.RowDefinitions.Count;

                var cellSide = Math.Min(usableHeight / rows, usableWidth / columns);

                board.WidthRequest = cellSide * columns;
                board.HeightRequest = cellSide * rows;
                InvalidateMeasure();
            }
        }

        async void OnReferenceCardChanged(Card card)
        {
            if (card != null)
            {
                referenceCard.Card = card;
                if (!referenceCard.Flipped)
                {
                    await referenceCard.Flip();
                }
            }
            else
            {
                if (referenceCard.Flipped)
                {
                    await referenceCard.Unflip();
                }
            }
        }

        private void OnPause(object sender, EventArgs e)
        {

        }

        private void OnMute(object sender, EventArgs e)
        {

        }
    }
}