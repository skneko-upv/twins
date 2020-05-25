using System;
using System.Linq;
using Twins.Models.Singletons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Twins.Utils.CollectionExtensions;

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

            SelectDeck.ItemsSource = defaultparameters.Decks.Select(x => x.Name).ToList();
            var index = defaultparameters.Decks.IndexOf(defaultparameters.SelectedDeck);
            SelectDeck.SelectedIndex = index;
            ImageCard.Source = defaultparameters.SelectedDeck.Cards[0].Image;
        }

        public void UpdateDeck()
        {
            var defaultparameters = PlayerPreferences.Instance;
            defaultparameters.SelectedDeck = defaultparameters.Decks[SelectDeck.SelectedIndex];
        }

        private void SelectDeck_SelectedIndexChanged(object sender, EventArgs e)
        {
            var defaultparameters = PlayerPreferences.Instance;
            ImageCard.Source = defaultparameters.Decks[SelectDeck.SelectedIndex].Cards[0].Image;
        }
    }
}