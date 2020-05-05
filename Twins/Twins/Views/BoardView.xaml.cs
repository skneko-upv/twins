using System;
using Twins.Components;
using Twins.Models;
using Twins.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardView : ContentPage
    {
        public BoardView(Board board)
        {
            InitializeComponent();
            BoardViewModel boardViewModel = new BoardViewModel(board);
            BindingContext = boardViewModel;

            turnLabel.SetBinding(Label.TextProperty, "Turn");
            turnLabel.BindingContext = boardViewModel.Board.Game.Turn;

            globalTimeLabel.SetBinding(Label.TextProperty, "Time");
            globalTimeLabel.BindingContext = boardViewModel.Board.Game.GameClock.TimeLeft;

            turnTimeLabel.SetBinding(Label.TextProperty, "Time");
            turnTimeLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            successLabel.SetBinding(Label.TextProperty, "Match");
            successLabel.BindingContext = boardViewModel.Board.Game.MatchSuccesses;

            board.Game.Score.Changed += (_) =>
            {
                scoreLabel.Text = board.Game.Score.PositiveValue.ToString();
            };
            scoreLabel.Text = board.Game.Score.PositiveValue.ToString();

            FillBoard(board.Height, board.Width);

            board.ReferenceCardChanged += OnReferenceCardChanged;
            referenceCard.Clicked += () => { };

            board.Game.GameEnded += OnGameEnded;

            PauseMenu.BindingContext = boardViewModel;
        }

        private void OnGameEnded(bool victory)
        {
            Game game = ((BoardViewModel)BindingContext).Board.Game;
            EndGameModal.SetStadistics(
                game.Score.PositiveValue,
                game.GameClock.GetTimeSpan(),
                victory);
            EndGameModal.IsVisible = true;
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

            BoardViewModel viewModel = (BoardViewModel)BindingContext;
            foreach (Board.Cell cell in viewModel.Board.Cells)
            {
                CardComponent card = viewModel.CardComponents[cell];
                card.VerticalOptions = LayoutOptions.Center;
                card.HorizontalOptions = LayoutOptions.Center;
                board.Children.Add(card, cell.Row, cell.Column);
            }
        }

        private async void OnReferenceCardChanged(Card card)
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
            ((BoardViewModel)BindingContext).Board.Game.Pause();
            PauseMenu.OnPause();
        }

        private void OnMute(object sender, EventArgs e)
        {
            CommingSoonView.ButtonNotImplemented();
        }
    }
}