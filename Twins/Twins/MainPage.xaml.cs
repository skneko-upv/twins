using System;
using System.ComponentModel;
using Twins.Components;
using Twins.Models;
using Twins.Models.Builders;
using Twins.Models.Singletons;
using Twins.Utils;
using Twins.Views;
using Xamarin.Forms;

namespace Twins
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    
    public partial class MainPage : ContentPage
    {
        public static AudioPlayer player { get; set; }
        public MainPage()
        {
            InitializeComponent();

        }

       
        protected override void OnAppearing()
        {
            player = new AudioPlayer();
            var defaultParameter = PlayerPreferences.Instance;
            player.LoadSong(defaultParameter.SelectedSong +".wav");
            player.Player.Play();
            player.ChangeVolume(defaultParameter.Volume);
        }

        private async void OnOption(object sender, EventArgs e)
        {

            // resume
            // Open Option menu
            await Navigation.PushAsync(new Views.OptionsView());
        }

        private void OnMute(object sender, EventArgs e)
        {
            // resume
            // Mute music
            var defaultparameters = PlayerPreferences.Instance;
            if ( player.GetVolume() == 0.0 ) {
                defaultparameters.Volume = 100.0;
                player.ChangeVolume(defaultparameters.Volume); 
            }
            else {
                defaultparameters.Volume = 0.0;
                player.ChangeVolume(defaultparameters.Volume); 
            }
        }

        private void OnLogout(object sender, EventArgs e)
        {
            // resume
            // Finish app
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void OnHistoryGame(object sender, EventArgs e)
        {
            // resume
            // Open History menu
            await Navigation.PushAsync(new Views.LevelsView());
        }

        private async void OnStandarGame(object sender, EventArgs e)
        {
            // resume
            // Open Free Game menu
            var defaultParameters = PlayerPreferences.Instance;
            Game game;
            var gameBuilder = new GameBuilder(defaultParameters.Column, defaultParameters.Row);
            SetTimeOfGame(gameBuilder);
            SetTurnTimeOfGame(gameBuilder);
            SetDeck(gameBuilder);
            gameBuilder.OfKind(GameBuilder.GameKind.Standard);
            game = gameBuilder.Build();

            await Navigation.PushAsync(new BoardView(game.Board));

        }

        private void SetDeck(GameBuilder gameBuilder)
        {
            var defaultParameters = PlayerPreferences.Instance;
            if (defaultParameters.SelectedDeck == "Animales")
            {
                gameBuilder.WithDeck(BuiltInDecks.Animals.Value);
            }
            else if (defaultParameters.SelectedDeck == "Numeros")
            {
                gameBuilder.WithDeck(BuiltInDecks.Numbers.Value);
            }
            else
            {
                gameBuilder.WithDeck(BuiltInDecks.Sports.Value);
            }
        }

        private void SetTurnTimeOfGame(GameBuilder gameBuilder)
        {
            var defaultParameters = PlayerPreferences.Instance;
            gameBuilder.WithTurnTimeLimit(defaultParameters.TurnTime);
        }

        private void SetTimeOfGame(GameBuilder gameBuilder)
        {
            var defaultParameters = PlayerPreferences.Instance;
            gameBuilder.WithTimeLimit(defaultParameters.LimitTime);
        }

        private async void OnCardGame(object sender, EventArgs e)
        {
            // resume
            // Open Free Game menu
            var defaultParameters = PlayerPreferences.Instance;
            Game game;
            var gameBuilder = new GameBuilder(defaultParameters.Column, defaultParameters.Row);
            SetTimeOfGame(gameBuilder);
            SetTurnTimeOfGame(gameBuilder);
            SetDeck(gameBuilder);
            gameBuilder.OfKind(GameBuilder.GameKind.ReferenceCard);
            game = gameBuilder.Build();

            await Navigation.PushAsync(new BoardView(game.Board));
        }
        private async void OnCategoryGame(object sender, EventArgs e)
        {
            // resume
            // Open Free Game menu
            var defaultParameters = PlayerPreferences.Instance;
            Game game;
            var gameBuilder = new GameBuilder(defaultParameters.Column, defaultParameters.Row);
            SetTimeOfGame(gameBuilder);
            SetTurnTimeOfGame(gameBuilder);
            SetDeck(gameBuilder);
            gameBuilder.OfKind(GameBuilder.GameKind.Category);
            game = gameBuilder.Build();

            await Navigation.PushAsync(new BoardView(game.Board));
        }

        private void OnMultiplayerGame(object sender, EventArgs e)
        {
            // resume
            // Open Multiplayer menu
            CommingSoonView.ButtonNotImplemented();
        }

        private void OnChallengeGame(object sender, EventArgs e)
        {
            // resume
            // Open Challenge menu
            CommingSoonView.ButtonNotImplemented();
        }

        private void OnDesck(object sender, EventArgs e)
        {
            // resume
            // Open Desck menu
            CommingSoonView.ButtonNotImplemented();
        }

    }
}
