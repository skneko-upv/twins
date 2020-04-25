using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PauseView : AbsoluteLayout
    {

        public PauseView(bool isTimeHalted)
        {
            InitializeComponent();
            timeNotHaltedWarning.IsVisible = isTimeHalted;
        }

        public PauseView()
        {
            InitializeComponent();
            timeNotHaltedWarning.IsVisible = false;
        }

        public void OnResume(object sender, EventArgs e) { MenuPausa.IsVisible = false; }
        public void OnPause() { MenuPausa.IsVisible = true;  }
    }
}