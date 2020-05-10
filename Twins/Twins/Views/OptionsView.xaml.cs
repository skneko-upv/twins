using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twins.Components;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OptionsView : ContentPage
    {
        public OptionsView()
        {
            InitializeComponent();
            InitSelectionDeckList();
            InitSelectionSongList();
        }

        private void InitSelectionDeckList()
        {
            SelectorDeck = new DeckSelector();
        }

        private void InitSelectionSongList()
        {
            var songs = new List<string>();
            songs.Add("Cancion 1");
            songs.Add("Cancion 2");
            songs.Add("Cancion 3");

            SelectSong.ItemsSource = songs;
            SelectSong.SelectedIndex = 0;
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnApply(object sender, EventArgs e)
        {
            var defaultparameters = Twins.Models.DefaultParameters.Instance;
            if (DefaultRow.Text != null || DefaultColum.Text != null)
            {
                if (DefaultRow.Text != null && DefaultColum.Text != null)
                {
                    if (DefaultRow.Text.All(Char.IsNumber) && DefaultColum.Text.All(Char.IsNumber))
                    {
                        
                        defaultparameters.Row = int.Parse(DefaultRow.Text);
                        defaultparameters.Colum = int.Parse(DefaultColum.Text);
                        //defaultparameters.Desk = SelectDeck.SelectedItem.ToString();
                        //await Navigation.PopAsync();
                    }
                    else
                    {
                        
                    }

                }
                else
                {
                   
                }
            }
            else
            {
                //defaultparameters.Desk = SelectDeck.SelectedItem.ToString();
                //await Navigation.PopAsync();
            }

        }

        private void Volume_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            //here is the control logic volume

        }
    }
}
