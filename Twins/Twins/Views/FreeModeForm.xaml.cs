using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FreeModeForm : ContentPage
    {
        public FreeModeForm()
        {
            InitializeComponent();
        }

        private async void OnBackMainMenu(object sender, EventArgs e)
        {
            ///resume
            ///Go Back to Main Menu
            await Navigation.PopAsync();
        }

        private async void OnStartGame(object sender, EventArgs e)
        {
            ///resume
            ///Start the game with de parameter of the form
            Models.StandardGame game = new Models.StandardGame(6, 4,
                Components.BasicDeck.CreateBasicDeck(),
                TimeSpan.FromMinutes(1),
                TimeSpan.FromSeconds(5));
            await Navigation.PushAsync(new Views.BoardView(game.Board));
        }

    }
}