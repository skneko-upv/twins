using System;
using System.ComponentModel;
using Twins.Models;
using Twins.Utils;
using Xamarin.Forms;

namespace Twins
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    
    public partial class MainPage : ContentPage
    {
        public static AudioPlayer Player { get; set; }
        public static AudioPlayer Effects { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }

       
        protected override void OnAppearing()
        {
            if (Player == null) 
            { 
                Player = new AudioPlayer();
                Effects = new AudioPlayer();
            }
            
            var defaultParameter = DefaultParameters.Instance;

            if (Player.CurrentSong == "")
            {
                Player.LoadSong(defaultParameter.SelectedSong + ".wav");
                Player.ChangeVolume(defaultParameter.Volume);
                Effects.LoadEffect(defaultParameter.ButtonEffect + ".wav");
            }
        }

        private async void OnOption(object sender, EventArgs e)
        {

            // resume
            // Open Option menu
            await Navigation.PushAsync(new Views.OptionsView());
            MainPage.Effects.Play();
        }

        private void OnMute(object sender, EventArgs e)
        {
            // resume
            // Mute music
            var defaultparameters = DefaultParameters.Instance;
            if ( Player.GetVolume() == 0.0 ) {
                Player.ChangeVolume(defaultparameters.Volume);
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
            MainPage.Effects.Play();
        }

        private async void OnFreeGame(object sender, EventArgs e)
        {
            // resume
            // Open Free Game menu
            await Navigation.PushAsync(new Views.FreeModeForm());
            MainPage.Effects.Play();
        }

        private void OnMultiplayerGame(object sender, EventArgs e)
        {
            // resume
            // Open Multiplayer menu
            CommingSoonView.ButtonNotImplemented();
            MainPage.Effects.Play();
        }

        private void OnChallengeGame(object sender, EventArgs e)
        {
            // resume
            // Open Challenge menu
            CommingSoonView.ButtonNotImplemented();
            MainPage.Effects.Play();
        }

        private void OnDesck(object sender, EventArgs e)
        {
            // resume
            // Open Desck menu
            CommingSoonView.ButtonNotImplemented();
            MainPage.Effects.Play();
        }

    }
}
