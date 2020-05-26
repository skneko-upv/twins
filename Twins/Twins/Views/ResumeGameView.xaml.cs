using System;
using System.Threading.Tasks;
using Twins.Models;
using Twins.Models.Singletons;
using Twins.Persistence;
using Twins.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResumeGameView : AbsoluteLayout
    {
        public GameResult GameResult { get; private set; }

        private bool AlreadyPlayed { get; set; }

        public int Score {
            set => PointsLabel.Text = value.ToString();
        }

        internal void DisbleNextButton()
        {
            NextButton.IsVisible = false;
        }

        public TimeSpan Time {
            set => TimeLabel.Text = string.Format("{0:D2}:{1:D2}", value.Minutes, value.Seconds);
        }

        public ResumeGameView()
        {
            InitializeComponent();
            AlreadyPlayed = false;
        }

        public void OnRetry(object sender, EventArgs e)
        {

        }

        public async void SetStadistics(GameResult result)
        {
            var effects = new AudioPlayer();
            var preferences = PlayerPreferences.Instance;         

            GameResult = result;
            Score = result.Score;
            Time = result.Time;
            if (result.IsVictory)
            {
                background.Source = "Assets/Backgrounds/winBackground.png";
                effects.LoadEffect(preferences.WinEffect + ".wav");
            }
            else
            {
                background.Source = "Assets/Backgrounds/lostBackground.png";
                effects.LoadEffect(preferences.LoseEffect + ".wav");
            }

            if (!AlreadyPlayed)
            {
                effects.Play();
                AlreadyPlayed = true;
            }

            if (result.LevelNumber > 0)
            {
                modeReminder.Text += $"{result.LevelNumber}"; 
            }
            else
            {
                modeReminder.Text = "Modo libre";
            }

            if (result.Score < 0)
            {
                PointsLabel.TextColor = Color.Red;
            }

            if (GameResult.IsVictory)
            {
                var saved = await Database.Instance.GetPlayerInfo();
                if (saved.LastLevelPassed < GameResult.LevelNumber)
                    saved.LastLevelPassed = GameResult.LevelNumber;
                await Database.Instance.SavePlayerInfo(saved);
            }
        }

        public void SetMultiplayerStatistics(GameResult result, Player winner, bool conclusive)
        {
            Score = winner.Score.Value;
            Time = result.Time;

            var player = new AudioPlayer();
            if (result.IsVictory)
            {
                background.Source = "Assets/Backgrounds/winBackground.png";
                player.LoadEffect(PlayerPreferences.Instance.WinEffect + ".wav");
            } 
            else
            {
                background.Source = "Assets/Backgrounds/lostBackground.png";
                player.LoadEffect(PlayerPreferences.Instance.LoseEffect + ".wav");
            }
            player.Play();

            modeReminder.Text = "Multijugador";

            multiplayerResultLabel.IsVisible = true;
            if (conclusive)
            {
                multiplayerResultLabel.Text = "Gana el jugador ";
                winningPlayerLabel.IsVisible = true;
                winningPlayerLabel.Text = winner.Name;
            }
            else
            {
                multiplayerResultLabel.Text = "Empate";
            }

            if (winner.Score.Value < 0)
            {
                PointsLabel.TextColor = Color.Red;
            }
        }

        public async void OnHome(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        public async void OnNext(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}