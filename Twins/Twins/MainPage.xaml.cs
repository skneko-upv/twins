using System;
using System.ComponentModel;
using Twins.Utils;
using Xamarin.Forms;

namespace Twins
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            var player = new AudioPlayer();
            player.LoadSong("Solve The Puzzle.wav");
            player.Player.Play();
        }

        private void OnOption(object sender, EventArgs e)
        {

            // resume
            // Open Option menu
            CommingSoonView.ButtonNotImplemented();
        }

        private void OnMute(object sender, EventArgs e)
        {
            // resume
            // Mute music
            var player = new AudioPlayer();
            if ( player.GetVolume() == 0.0 ) { player.ChangeVolume(100.0); }
            else { player.ChangeVolume(0.0); }
        }

        private void OnLogout(object sender, EventArgs e)
        {
            // resume
            // Finish app
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void OnHistoryGame(object sender, EventArgs e)
        {
            // resume
            // Open History menu
            CommingSoonView.ButtonNotImplemented();
        }

        private async void OnFreeGame(object sender, EventArgs e)
        {
            // resume
            // Open Free Game menu
            await Navigation.PushAsync(new Views.FreeModeForm());
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
