using System;
using System.Linq;
using Twins.Components;
using Twins.Models;
using Twins.Models.Game;
using Twins.Models.Singletons;
using Twins.Utils;
using Twins.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BoardView : ContentPage
    {
        AudioPlayer tickPlayer = new AudioPlayer();

        public BoardView(Board board)
        {
            InitializeComponent();
            if (board.Game.LevelNumber == 0) EndGameModal.DisbleNextButton();
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

            board.ReferenceCategoryChanged += OnReferenceCategoryChanged;
            OnReferenceCategoryChanged(board.ReferenceCategory);

            turn2PointLabel.SetBinding(Label.TextColorProperty, "Color");
            turn2PointLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            turnTextLabel.SetBinding(Label.TextColorProperty, "Color");
            turnTextLabel.BindingContext = boardViewModel.Board.Game.TurnClock.TimeLeft;

            if (board.Game.IsMultiplayer) {
                var game = (IMultiplayerGame)board.Game;
                game.PlayerChanged += OnPlayerChanged;
                OnPlayerChanged(game.CurrentPlayer);

                game.Players[0].Score.Changed += (old, @new) => OnScoreChanged(1, @new);
                OnScoreChanged(1, board.Game.Score.Value);
                game.Players[1].Score.Changed += (old, @new) => OnScoreChanged(2, @new);
                OnScoreChanged(2, board.Game.Score.Value);

                scoreLabelVs.IsVisible = true;
                scoreLabel2.IsVisible = true;

                tickPlayer.LoadEffect("PlayerChange.wav");
            }
            else
            {
                board.Game.Score.Changed += (old, @new) => OnScoreChanged(1, @new);
                OnScoreChanged(1, board.Game.Score.Value);
            }

            board.Game.GameEnded += OnGameEnded;

            referenceCard.Clicked += () => { };

            FillBoard(board.Height, board.Width);
        }

        private void OnPlayerChanged(Player currentPlayer)
        {
            multiplayerFrame.IsVisible = true;
            playerLabel.Text = currentPlayer.Name;

            tickPlayer.Play();
        }

        private void OnScoreChanged(int counterId, int score)
        {
            Label scoreLabel;

            if (counterId == 1)
            {
                scoreLabel = scoreLabel1;
            }
            else
            {
                scoreLabel = scoreLabel2;
            }

            if (score < 0)
            {
                scoreLabel.TextColor = Color.Red;
            }
            else
            {
                scoreLabel.TextColor = Color.White;
            }
            scoreLabel.Text = score.ToString();
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
            var game = ((BoardViewModel)BindingContext).Board.Game;
            if (game.IsMultiplayer)
            {
                EndGameModal.SetMultiplayerStatistics(result,
                    ((IMultiplayerGame)game).DetermineWinner(out bool conclusive),
                    conclusive);
                EndGameModal.IsVisible = true;
            }
            else
            {
                EndGameModal.SetStadistics(result);
                EndGameModal.IsVisible = true;
            }
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
            MainPage.Player.Mute();
            MuteButton.ImageSource = MainPage.Player.GetVolume() == 0.0 ? "Assets/Icons/muteW.png" : "Assets/Icons/volumeW.png";
        }

        public ResumeGameView GetResumeGameView() 
        {
            return EndGameModal;
        }

    }
}