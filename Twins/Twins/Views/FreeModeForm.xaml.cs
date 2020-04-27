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
    public partial class FreeModeForm : ContentPage
    {
        public FreeModeForm()
        {
            InitializeComponent();
        }
        async void OnBackMainMenu(object sender, EventArgs e)
        {
            ///resume
            ///Go Back to Main Menu
            await Navigation.PopAsync();
        }

        async void OnStartGame(object sender, EventArgs e)
        {
            ///resume
            ///Start the game with de parameter of the form
            var game = new Models.StandardGame(6, 4, Components.BasicDeck.CreateBasicDeck());
            await Navigation.PushAsync(new Views.BoardView(game.Board));
        }

    }
}