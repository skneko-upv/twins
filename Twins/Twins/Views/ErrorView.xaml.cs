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
    public partial class ErrorView : Grid
    {
        public ErrorView()
        {
            InitializeComponent();
        }

        private void ErrorViewClicked(object sender, EventArgs e)
        {
            IsVisible = false;
        }

        public void SetTextError(string description) 
        {
            ErrorText.Text = description;
        }
    }
}