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


            GlobalMinLabel.SetBinding(Label.TextProperty, "Minutes", BindingMode.OneWay,null,"{0:D2}");
            GlobalMinLabel.BindingContext = boardViewModel.Board.Game.GlobalTime;

            GlobalSecLabel.SetBinding(Label.TextProperty, "Seconds", BindingMode.OneWay, null, "{0:D2}");
            GlobalSecLabel.BindingContext = boardViewModel.Board.Game.GlobalTime;

            TurnMinLabel.SetBinding(Label.TextProperty, "Minutes", BindingMode.OneWay, null, "{0:D2}");
            TurnMinLabel.BindingContext = boardViewModel.Board.Game.TurnTime;

            TurnSecLabel.SetBinding(Label.TextProperty, "Seconds", BindingMode.OneWay, null, "{0:D2}");
            TurnSecLabel.BindingContext = boardViewModel.Board.Game.TurnTime;

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