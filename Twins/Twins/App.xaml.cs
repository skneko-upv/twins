using Xamarin.Forms;

namespace Twins
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var game = new Models.StandardGame(6, 4, Components.BasicDeck.CreateBasicDeck());
            MainPage = new Views.BoardView(game.Board);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
