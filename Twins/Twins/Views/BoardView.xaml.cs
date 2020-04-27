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
            
            TurnLabel.SetBinding(Label.TextProperty, "Turn");
            TurnLabel.BindingContext = boardViewModel.Board.Game.Turn;

            GlobalTimeLabel.SetBinding(Label.TextProperty, "Time");
            GlobalTimeLabel.BindingContext = boardViewModel.Board.Game.GameClock.TimeLeft;

            TurnTimeLabel.SetBinding(Label.TextProperty, "Time");
            TurnTimeLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            FillBoard(board.Height, board.Width);

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
                CardComponent card = viewModel.CardComponents[cell];
                card.VerticalOptions = LayoutOptions.Center;
                card.HorizontalOptions = LayoutOptions.Center;
                board.Children.Add(card, cell.Row, cell.Column);
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
            PauseMenu.OnPause();
        }

        private void OnMute(object sender, EventArgs e)
        {
            CommingSoonView.ButtonNotImplemented();
        }
    }
}