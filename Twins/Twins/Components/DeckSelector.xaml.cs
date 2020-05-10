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
            var decks = new List<string>();
            decks.Add("Animales");
            decks.Add("Numeros");
            decks.Add("Paisajes");

            SelectDeck.ItemsSource = decks;
            SelectDeck.SelectedIndex = 0;
        }

        public void updateDeck() {
            var defaultparameters = Twins.Models.DefaultParameters.Instance;
            defaultparameters.Desk = SelectDeck.SelectedItem.ToString();
            //Here we have to update ImageCard , with the new image of de deck selected
            //ImageCard.Source = ""
        }


    }
}