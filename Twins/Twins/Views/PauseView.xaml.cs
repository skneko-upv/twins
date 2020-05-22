using System;
using Twins.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PauseView : AbsoluteLayout
    {
        public PauseView(bool isTimeHalted = false)
        {
            InitializeComponent();
            timeNotHaltedWarning.IsVisible = isTimeHalted;
        }

        public PauseView() : this(false) { }

        public void OnResume(object sender, EventArgs e)
        {
            MainPage.EffectsPlayer.Play();
            ((BoardViewModel)BindingContext).Board.Game.Resume();
            window.IsVisible = false;
        }

        public void OnPause() {
            MainPage.EffectsPlayer.Play();
            window.IsVisible = true;
        }

        public async void OnAbandon(object sender, EventArgs e)
        {
            MainPage.EffectsPlayer.Play();
            await Navigation.PopToRootAsync();
        }

        
    }
}