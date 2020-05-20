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
        PlayerPreferences gameConfiguration = PlayerPreferences.Instance;
        Game game;
        GameBuilder gameBuilder; 
        public MainPage()
        {
            InitializeComponent();

        }

        private void InitGameConfiguration()
        {
            gameBuilder = new GameBuilder(gameConfiguration.Column, gameConfiguration.Row);
            SetTimeOfGame(gameBuilder);
            SetTurnTimeOfGame(gameBuilder);
            SetDeck(gameBuilder);
        }

        protected override void OnAppearing()
        {
            player = new AudioPlayer();
            player.LoadSong(gameConfiguration.SelectedSong +".wav");
            player.Player.Play();
            player.ChangeVolume(gameConfiguration.Volume);
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
            if ( player.GetVolume() == 0.0 ) {
                player.ChangeVolume(gameConfiguration.Volume); 
            }
            else {
                player.ChangeVolume(0.0); 
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
            try{
            InitGameConfiguration();
            gameBuilder.OfKind(GameBuilder.GameKind.Standard);
            game = gameBuilder.Build();
            await Navigation.PushAsync(new BoardView(game.Board));
            }
            catch (Exception error)
            {
                ErrorView.IsVisible = true;
                ErrorView.SetTextError(error.Message);
            }
        }

        private void SetDeck(GameBuilder gameBuilder)
        {
            if (gameConfiguration.SelectedDeck == "Animales")
            {
                gameBuilder.WithDeck(BuiltInDecks.Animals.Value);
            }
            else if (gameConfiguration.SelectedDeck == "Numeros")
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
            gameBuilder.WithTurnTimeLimit(gameConfiguration.TurnTime);
        }

        private void SetTimeOfGame(GameBuilder gameBuilder)
        {
            gameBuilder.WithTimeLimit(gameConfiguration.LimitTime);
        }

        private async void OnCardGame(object sender, EventArgs e)
        {
            // resume
            // Open Free Game men
            try 
            {
            InitGameConfiguration();
            gameBuilder.OfKind(GameBuilder.GameKind.ReferenceCard);
            game = gameBuilder.Build();
            await Navigation.PushAsync(new BoardView(game.Board));
            }
            catch (Exception error)
            {
                ErrorView.IsVisible = true;
                ErrorView.SetTextError(error.Message);
            }

    
        }
        private async void OnCategoryGame(object sender, EventArgs e)
        {
            // resume
            // Open Free Game menu
            try
            { 
            InitGameConfiguration();
            gameBuilder.OfKind(GameBuilder.GameKind.Category);
            game = gameBuilder.Build();
            await Navigation.PushAsync(new BoardView(game.Board));
            }
            catch (Exception error)
            {
                ErrorView.IsVisible = true;
                ErrorView.SetTextError(error.Message);
            }
            
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
