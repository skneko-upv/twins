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

        public void OnResume(object sender, EventArgs e) {
            ((BoardViewModel)BindingContext).Board.Game.Resume();
            window.IsVisible = false;
        }

        public void OnPause() { window.IsVisible = true; }

        public async void OnAbandon(object sender, EventArgs e) {
            await Navigation.PopToRootAsync();
        }

        public void OnOptions(object sender, EventArgs e)
        {
            CommingSoonView.ButtonNotImplemented();
        }
    }
}