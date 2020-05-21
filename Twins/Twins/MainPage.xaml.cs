using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Twins.Components;
using Twins.Models;
using Twins.Models.Builders;
using Twins.Models.Singletons;
using Twins.Persistence;
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
        public static AudioPlayer Player { get; set; }
        public static AudioPlayer EffectsPlayer { get; set; }

        readonly PlayerPreferences gameConfiguration = PlayerPreferences.Instance;
        Game game;
        GameBuilder gameBuilder; 
        public MainPage()
        {
            InitializeComponent();

        }

        private async void InitPlayerPreferences()
        {
            try
            {
                var database = Database.Instance;
                var saved = await database.GetPlayerPreferences();
                gameConfiguration.SelectedSong = saved.SelectedSong;
                gameConfiguration.Volume = saved.Volume;

                var savedDecks = await database.GetDecksAsync();
                var decks = new List<string>();
                foreach (var d in savedDecks)
                {
                    decks.Add(d.Name);
                };

                gameConfiguration.Column = saved.Column;
                gameConfiguration.Row = saved.Row;
                gameConfiguration.Decks = decks;
                gameConfiguration.SelectedDeck = saved.SelectedDeck;
                gameConfiguration.LimitTime = saved.LimitTime;
                gameConfiguration.TurnTime = saved.TurnTime;
            }
            catch (Exception) { }

            if (Player == null)
            {
                Player = new AudioPlayer();
                EffectsPlayer = new AudioPlayer();
            }

            if (Player.CurrentSong == "")
            {
                Player.LoadSong(gameConfiguration.SelectedSong + ".wav");
                Player.ChangeVolume(gameConfiguration.Volume);
                EffectsPlayer.LoadEffect(gameConfiguration.ButtonEffect + ".wav");
            }

        }

        private void InitGameConfiguration()
        {
            gameBuilder = new GameBuilder(gameConfiguration.Column, gameConfiguration.Row);
            SetTimeOfGame(gameBuilder);
            SetTurnTimeOfGame(gameBuilder);
            SetDeck(gameBuilder);
        }

        protected override void  OnAppearing()
        {
            InitPlayerPreferences();
        }

        private async void OnOption(object sender, EventArgs e)
        {

            // resume
            // Open Option menu
            await Navigation.PushAsync(new Views.OptionsView());
            MainPage.EffectsPlayer.Play();
        }

        private void OnMute(object sender, EventArgs e)
        {
            // resume
            // Mute music
            if ( Player.GetVolume() == 0.0 ) {
                Player.ChangeVolume(gameConfiguration.Volume); 
            }
            else {
                Player.ChangeVolume(0.0); 
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
            EffectsPlayer.Play();
        }

        private async void OnStandardGame(object sender, EventArgs e)
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
                MainPage.EffectsPlayer.Play();
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
            MainPage.EffectsPlayer.Play();
        }

        private void OnChallengeGame(object sender, EventArgs e)
        {
            // resume
            // Open Challenge menu
            CommingSoonView.ButtonNotImplemented();
            MainPage.EffectsPlayer.Play();
        }

        private void OnDesck(object sender, EventArgs e)
        {
            // resume
            // Open Desck menu
            CommingSoonView.ButtonNotImplemented();
            MainPage.EffectsPlayer.Play();
        }

    }
}
