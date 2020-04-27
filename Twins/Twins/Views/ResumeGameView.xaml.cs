using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResumeGameView : AbsoluteLayout
    {
        public ResumeGameView()
        {
            InitializeComponent();
        }

        public void OnRetry(object sender, EventArgs e) 
        {

        }

        public async void OnHome(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
        public async void OnNext(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            var game = new Models.StandardGame(6, 4, Components.BasicDeck.CreateBasicDeck(),
                TimeSpan.FromMinutes(1),
                TimeSpan.FromSeconds(5));
            await Navigation.PushAsync(new Views.BoardView(game.Board));
        }
    }
}