using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Twins.Components;
using Twins.Models;
using Twins.Models.Builders;
using Twins.Models.Game;
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
        IGame game;
        GameBuilder gameBuilder; 
        public MainPage()
        {
            InitializeComponent();
        }

        private async Task InitPlayerPreferences()
        {
            try
            {
                var database = Database.Instance;
                var saved = await database.GetPlayerPreferences();
                gameConfiguration.SelectedSong = saved.SelectedSong;
                gameConfiguration.Volume = saved.Volume;

                gameConfiguration.Column = saved.Column;
                gameConfiguration.Row = saved.Row;
                gameConfiguration.LimitTime = saved.LimitTime;
                gameConfiguration.TurnTime = saved.TurnTime;
            }
            catch (Exception) { }

            if (Player == null)
            {
                Player = new AudioPlayer();
                EffectsPlayer = new AudioPlayer();
            }

            if (string.IsNullOrEmpty(Player.CurrentSong))
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

        protected async override void OnAppearing()
        {
            await InitPlayerPreferences();
            volumeIcon.Source = Player.GetVolume() == 0.0 ? "Assets/Icons/mute.png" : "Assets/Icons/volume.png";
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
            Player.Mute();
            volumeIcon.Source = Player.GetVolume() == 0.0 ? "Assets/Icons/mute.png" : "Assets/Icons/volume.png";
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
            await Navigation.PushAsync(new LevelsView());
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
            gameBuilder.WithDeck(gameConfiguration.SelectedDeck);
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

        private async void OnMultiplayerGame(object sender, EventArgs e)
        {
            // resume
            // Open Multiplayer menu
            try
            {
                InitGameConfiguration();
                game = gameBuilder
                    .WithPlayer(new Player("1"))
                    .WithPlayer(new Player("2"))
                    .Build();
                await Navigation.PushAsync(new BoardView(game.Board));
            }
            catch (Exception error)
            {
                ErrorView.IsVisible = true;
                ErrorView.SetTextError(error.Message);
            }
            MainPage.EffectsPlayer.Play();
        }

        private async void OnDeckList(object sender, EventArgs e)
        {
            // resume
            // Open Desck menu
            await Navigation.PushAsync(new Views.DeckListForm());
            MainPage.EffectsPlayer.Play();
        }

    }
}
