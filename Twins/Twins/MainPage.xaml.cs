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
           /// Aqui va el codigo para abrir el menu de opciones
        }
        void OnMute(object sender, EventArgs e)
        {
            //Aqui va el codigo para solenciar la musica
        }
        void OnLogout(object sender, EventArgs e)
        {
            ///Aqui va el codigo para cerrar la aplicacion
        }

        void OnHistoryGame(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        void OnFreeGame(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        void OnMultiplayerGame(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        void OnChallengeGame(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }
        void OnDesck(object sender, EventArgs e)
        {
            (sender as Button).Text = "Click me again!";
        }

    }
}
