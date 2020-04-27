using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResumeGameView : AbsoluteLayout
    {
        public int Score {
            set
            {
                PointsLabel.Text = "" + value;
            }
        }
        public int Tries 
        {
            set 
            {
                TriesLabel.Text = "" + value;
            }
        }
        public int Successes
        {
            set
            {
                SuccessLabel.Text = "" + value;
            }
        }
        public TimeSpan Time
        {
            set
            {
                TimeLabel.Text = string.Format("{0:D2}:{1:D2}", value.Minutes, value.Seconds);
            }
        }

        public ResumeGameView()
        {
            InitializeComponent();
        }

        public void OnRetry(object sender, EventArgs e) 
        {

        }

        public void SetStadistics(int score, int tries, int succeses, string time, bool isVictory)
        {
            Score = score;
            Tries = tries;
            Successes = successes;
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
            await Navigation.PopToRootAsync();
        }

        public async void OnNext(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            var game = new Models.StandardGame(6, 4, Components.BasicDeck.CreateBasicDeck(),
                TimeSpan.FromMinutes(1),
                TimeSpan.FromSeconds(5));
            await Navigation.PushAsync(new Views.BoardView(game.Board));
        }
    }
}