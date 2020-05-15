using System;
using System.Threading.Tasks;
using Twins.Models;
using Twins.Persistence;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResumeGameView : AbsoluteLayout
    {
        public GameResult GameResult { get; private set; }

        public int Score {
            set => PointsLabel.Text = value.ToString();
        }

        public TimeSpan Time {
            set => TimeLabel.Text = string.Format("{0:D2}:{1:D2}", value.Minutes, value.Seconds);
        }

        public ResumeGameView()
        {
            InitializeComponent();
        }

        public void OnRetry(object sender, EventArgs e)
        {

        }

        public async void SetStadistics(GameResult result)
        {
            GameResult = result;
            Score = result.Score;
            Time = result.Time;
            if (result.IsVictory)
            {
                ResultLabel.Text = "Victoria";
                ResultLabel.TextColor = Color.Green;
            }
            else
            {
                ResultLabel.Text = "Derrota";
                ResultLabel.TextColor = Color.Red;
            }

            if (result.LevelNumber > 0)
            {
                modeReminder.Text = $"Nivel {result.LevelNumber}"; 
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

        public async void OnHome(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        public async void OnNext(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            //Models.StandardGame game = new Models.StandardGame(6, 4, Components.BasicDeck.Animales,
                //TimeSpan.FromMinutes(1),
                //TimeSpan.FromSeconds(5));
            //await Navigation.PushAsync(new Views.BoardView(game.Board));
        }
    }
}