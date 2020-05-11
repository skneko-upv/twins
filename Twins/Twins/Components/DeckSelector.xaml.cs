using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckSelector : StackLayout
    {
        public DeckSelector()
        {
            InitializeComponent();
            InitSelectionDeckList();
        }

        private void InitSelectionDeckList()
        {
            var defaultparameters = Twins.Models.DefaultParameters.Instance;

            SelectDeck.ItemsSource = defaultparameters.ListDeck;
            SelectDeck.SelectedIndex = 0;
        }

        public void UpdateDeck() 
        {
            var defaultparameters = Twins.Models.DefaultParameters.Instance;
            defaultparameters.SelectedDeck = SelectDeck.SelectedItem.ToString();
            
        }

        private void SelectDeck_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Here we have to update ImageCard , with the new image of de deck selected
            //ImageCard.Source = ""
        }
    }
}