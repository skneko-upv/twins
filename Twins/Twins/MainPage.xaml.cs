using System;
using System.ComponentModel;
using Twins.Models.Singletons;
using Twins.Utils;
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
