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
        public ResultOfGame ResultOfGame { get; private set; }
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

        public void SetStadistics(int score, TimeSpan time, bool isVictory, ResultOfGame result)
        {
            ResultOfGame = result;
            Score = score;
            Time = time;
            if (isVictory)
            {
                ResultLabel.Text = "Victoria";
                ResultLabel.TextColor = Color.Green;
            }
            else
            {
                ResultLabel.Text = "Derrota";
                ResultLabel.TextColor = Color.Red;
            }
        }

        public async void OnHome(object sender, EventArgs e)
        {
            if (ResultOfGame.IsVictory)
            {
                var playerInfo = await Database.Instance.GetPlayerInfo();
                if(((PlayerInfo)playerInfo).LastLevelPassed < ResultOfGame.LevelNumber)
                    ((PlayerInfo)playerInfo).LastLevelPassed = ResultOfGame.LevelNumber;
                await Database.Instance.SavePlayerInfo(playerInfo);
            }
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