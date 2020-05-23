using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Twins.Components;
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

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            MainPage.EffectsPlayer.Play();
        }

        private async void OnCreateDeck(object sender, EventArgs e)
        {
            //await Navigation.PopAsync();
            MainPage.EffectsPlayer.Play();
        }

        private async void OnApply(object sender, EventArgs e)
        {
            MainPage.EffectsPlayer.Play();
        }

        private async Task UpdateDatabase()
        {
            var database = Database.Instance;
            var playerPreferencesDB = await database.GetPlayerPreferences();
            var playerPreferences = PlayerPreferences.Instance;
            playerPreferencesDB.Column = playerPreferences.Column;
            playerPreferencesDB.Row = playerPreferences.Row;
            playerPreferencesDB.SelectedDeck = playerPreferences.SelectedDeck;
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

        private void OnlyNumbersTime(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (((Entry)sender).Text.Length != 0)
                {
                    if (Int32.Parse(((Entry)sender).Text) / 60 > 0)
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                ((Entry)sender).Text = ((Entry)sender).Text.Substring(0, ((Entry)sender).Text.Length - 1);
            }
        }
    }
}