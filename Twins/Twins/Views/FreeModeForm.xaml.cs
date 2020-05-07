using System;
using Twins.Models.Builders;
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
            var game = new GameBuilder(6, 4)
                        .OfKind(GameBuilder.GameKind.ReferenceCard)
                        .WithPredictablePopulation()
                        .Build();
            await Navigation.PushAsync(new BoardView(game.Board));
        }

    }
}