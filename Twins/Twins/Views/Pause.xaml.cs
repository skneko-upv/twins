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
    public partial class Pause : StackLayout
    {
        public Pause(bool pausaTiempo)
        {
            InitializeComponent();
            notPause.IsVisible = pausaTiempo;
        }

        public Pause()
        {
            InitializeComponent();
            notPause.IsVisible = false;
        }

        public void RenaunudarClicked(object sender, EventArgs e) { MenuPausa.IsVisible = false; }
    }
}