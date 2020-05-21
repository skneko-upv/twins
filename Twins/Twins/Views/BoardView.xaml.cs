using System;
using Twins.Components;
using Twins.Models;
using Twins.Utils;
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
            PauseMenu.BindingContext = boardViewModel;

            turnLabel.SetBinding(Label.TextProperty, "Value");
            turnLabel.BindingContext = boardViewModel.Board.Game.Turn;

            globalTimeLabel.SetBinding(Label.TextProperty, "Time");
            globalTimeLabel.BindingContext = boardViewModel.Board.Game.GameClock.TimeLeft;

            turnTimeLabel.SetBinding(Label.TextProperty, "Time");
            turnTimeLabel.SetBinding(Label.TextColorProperty, "Color");
            turnTimeLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            successLabel.SetBinding(Label.TextProperty, "Value");
            successLabel.BindingContext = boardViewModel.Board.Game.MatchSuccesses;

            board.Game.Score.Changed += (_) =>
            {
                scoreLabel.Text = board.Game.Score.PositiveValue.ToString();
            };
            scoreLabel.Text = board.Game.Score.PositiveValue.ToString();

            board.ReferenceCategoryChanged += OnReferenceCategoryChanged;
            OnReferenceCategoryChanged(board.ReferenceCategory);

            turn2PointLabel.SetBinding(Label.TextColorProperty, "Color");
            turn2PointLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            turnTextLabel.SetBinding(Label.TextColorProperty, "Color");
            turnTextLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            board.Game.GameEnded += OnGameEnded;

            referenceCard.Clicked += () => { };

            FillBoard(board.Height, board.Width);
        }

        private void OnReferenceCategoryChanged(Category category)
        {
            if (category != null)
            {
                categoryHint.IsVisible = true;
                categoryHint.Text = category.Name;
            }
            else
            {
                categoryHint.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            var board = ((BoardViewModel)BindingContext).Board;
            board.ReferenceCardChanged += OnReferenceCardChanged;
            OnReferenceCardChanged(board.ReferenceCard);
        }

        private void OnGameEnded(GameResult result)
        {
            EndGameModal.SetStadistics(result);
            EndGameModal.IsVisible = true;
        }

        private void FillBoard(int height, int width)
        {

            for (int i = 0; i < height; i++)
            {
                board.RowDefinitions.Add(new RowDefinition { Height = new GridLength(120) });
            }
            for (int i = 0; i < width; i++)
            {
                board.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
            }

            BoardViewModel viewModel = (BoardViewModel)BindingContext;
            foreach (Board.Cell cell in viewModel.Board.Cells)
            {
                CardComponent card = viewModel.CardComponents[cell];
                card.VerticalOptions = LayoutOptions.Center;
                card.HorizontalOptions = LayoutOptions.Center;
                board.Children.Add(card, cell.Row, cell.Column);
            }
            boardArea.WidthRequest = 122 * height;
            boardArea.HeightRequest = 122 *  width;
            board.WidthRequest = 122 * height;
            board.HeightRequest = 122 *  width;

        }

        private async void OnReferenceCardChanged(Card card)
        {
            if (card != null)
            {
                referenceCardFrame.IsVisible = true;
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
            var defaultparameters = DefaultParameters.Instance;
            var Player = MainPage.Player;
            if (Player.GetVolume() == 0.0)
            {
                Player.ChangeVolume(defaultparameters.Volume);
            }
            else
            {
                Player.ChangeVolume(0.0);
            }
        }

        public ResumeGameView GetResumeGameView() 
        {
            return EndGameModal;
        }
    }
}