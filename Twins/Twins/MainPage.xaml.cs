using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twins.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals; 

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


        void OnOption(object sender, EventArgs e)
        {

            /// resume
            /// Open Option menu
            CommingSoonView.ButtonNotImplemented();
        }
        void OnMute(object sender, EventArgs e)
        {
            ///resume
            ///Mute music
            CommingSoonView.ButtonNotImplemented();
        }
        void OnLogout(object sender, EventArgs e)
        {
            ///resume
            ///Finish app
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        void OnHistoryGame(object sender, EventArgs e)
        {
            ///resume
            ///Open History menu
            CommingSoonView.ButtonNotImplemented();


        }
        async void OnFreeGame(object sender, EventArgs e)
        {
            ///resume
            ///Open Free Game menu
            await Navigation.PushAsync(new Views.FreeModeForm());


        }

        void OnMultiplayerGame(object sender, EventArgs e)
        {
            ///resume
            ///Open Multiplayer menu
            CommingSoonView.ButtonNotImplemented();
        }
        void OnChallengeGame(object sender, EventArgs e)
        {
            ///resume
            ///Open Challenge menu
            CommingSoonView.ButtonNotImplemented();
        }
        void OnDesck(object sender, EventArgs e)
        {
            ///resume
            ///Open Desck menu
            CommingSoonView.ButtonNotImplemented();
        }

    }
}
