using System;
using Twins.Models.Singletons;
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
            
        }

        public void InitSelectionDeckList()
        {
            var defaultparameters = PlayerPreferences.Instance;

            SelectDeck.ItemsSource = defaultparameters.Decks;
            var index = defaultparameters.Decks.IndexOf(defaultparameters.SelectedDeck);
            SelectDeck.SelectedIndex = index;
            if (defaultparameters.SelectedDeck == "Animales")
            {
                ImageCard.Source = "Assets/Decks/Deck1/card1.png";
            }
            else if (defaultparameters.SelectedDeck == "Numeros")
            {
                ImageCard.Source = "Assets/Decks/Deck2/card1.png";
            }
            else
            {
                ImageCard.Source = "Assets/Decks/Deck3/card1.png";
            }
            
        }

        public void UpdateDeck() 
        {
            var defaultparameters = PlayerPreferences.Instance;
            defaultparameters.SelectedDeck = SelectDeck.SelectedItem.ToString();
            
        }

        private void SelectDeck_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            //Here we have to update ImageCard , with the new image of de deck selected
            if (SelectDeck.SelectedItem.ToString() == "Animales")
            {
                ImageCard.Source = "Assets/Decks/Deck1/card1.png";
            }
            else if (SelectDeck.SelectedItem.ToString() == "Numeros")
            {
                ImageCard.Source = "Assets/Decks/Deck2/card1.png";
            }
            else
            {
                ImageCard.Source = "Assets/Decks/Deck3/card1.png";
            }
        }
    }
}