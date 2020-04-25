using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        void OnOption(object sender, EventArgs e)
        {
           
           /// resume
           /// Open Option menu
        }
        void OnMute(object sender, EventArgs e)
        {
            ///resume
            ///Mute music
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
            
        }
        void OnFreeGame(object sender, EventArgs e)
        {
            ///resume
            ///Open Free Game menu
        }
        void OnMultiplayerGame(object sender, EventArgs e)
        {
            ///resume
            ///Open Multiplayer menu
        }
        void OnChallengeGame(object sender, EventArgs e)
        {
            ///resume
            ///Open Challenge menu
        }
        void OnDesck(object sender, EventArgs e)
        {
            ///resume
            ///Open Desck menu
        }

    }
}
