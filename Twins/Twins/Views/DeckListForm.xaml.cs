using System;
using System.Threading.Tasks;
using Twins.Components;
using Twins.Models;
using Twins.Models.Singletons;
using Twins.Persistence;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeckListForm : ContentPage
    {

        public DeckListForm()
        {
            InitializeComponent();
        }

        private void FillScrollView()
        {
            deckArea.Children.Clear();
            bool hasTwoStackLayoutChilds = false;
            StackLayout generatedStackLayout = null;
            foreach (Deck deck in PlayerPreferences.Instance.Decks)
            {
                if (!hasTwoStackLayoutChilds)
                {
                    generatedStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                    deckArea.Children.Add(generatedStackLayout);
                    generatedStackLayout.Children.Add(new DeckViewComponent(deck));
                    hasTwoStackLayoutChilds = true;
                }
                else
                {
                    generatedStackLayout.Children.Add(new DeckViewComponent(deck));
                    hasTwoStackLayoutChilds = false;
                }
            }
        }

        protected override void OnAppearing()
        {
            FillScrollView();

            PlayerPreferences defaultParameters = PlayerPreferences.Instance;

            foreach (View stackLayout in deckArea.Children)
            {
                foreach (DeckViewComponent deck in ((StackLayout)stackLayout).Children)
                {
                    if (deck.Deck == defaultParameters.SelectedDeck)
                    {
                        deck.MarkChecked();
                    }
                }
            }
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            MainPage.EffectsPlayer.Play();
        }

        private async void OnCreateDeck(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditDeckView());
        }

        private async void OnApply(object sender, EventArgs e)
        {
            PlayerPreferences defaultParameters = PlayerPreferences.Instance;
            defaultParameters.SelectedDeck = GetSelectedDeck();

            MainPage.EffectsPlayer.Play();
            await UpdateDatabase();
            await Navigation.PopAsync();
        }

        private Deck GetSelectedDeck()
        {
            foreach (View stackLayout in deckArea.Children)
            {
                foreach (DeckViewComponent deck in ((StackLayout)stackLayout).Children)
                {
                    if (deck.IsChecked())
                    {
                        System.Diagnostics.Debug.WriteLine(deck.Deck.Name);
                        return deck.Deck;
                    }
                }
            }
            return null;
        }

        //Intentar centralizar
        private async Task UpdateDatabase()
        {
            Database database = Database.Instance;
            Persistence.DataTypes.PlayerPreferences playerPreferencesDB = await database.GetPlayerPreferences();
            PlayerPreferences playerPreferences = PlayerPreferences.Instance;
            playerPreferencesDB.Column = playerPreferences.Column;
            playerPreferencesDB.Row = playerPreferences.Row;
            playerPreferencesDB.SelectedSong = playerPreferences.SelectedSong;
            playerPreferencesDB.Volume = playerPreferences.Volume;
            playerPreferencesDB.LimitTime = playerPreferences.LimitTime;
            playerPreferencesDB.TurnTime = playerPreferences.TurnTime;

            Database.Instance.SavePlayerPreferences(playerPreferencesDB);
        }



        private void ErrorViewClicked(object sender, EventArgs e)
        {
            ErrorView.IsVisible = false;
            MainPage.EffectsPlayer.Play();
        }

    }
}