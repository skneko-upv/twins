using System;
using System.Collections.Generic;
using Twins.Models.Singletons;
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
            InitSelectionSongList();
            InitVolume();
        }

        private void InitVolume() 
        {
            var defaultParameters = PlayerPreferences.Instance;
            Volume.Value = defaultParameters.Volume;
        }

        private void InitSelectionSongList()
        {
            var songs = new List<string>();
            var defaultParameters = PlayerPreferences.Instance;
            songs.Add("Solve The Puzzle");
            songs.Add("Chiptronical");

            SelectSong.ItemsSource = songs;
            var index = songs.IndexOf(defaultParameters.SelectedSong);
            SelectSong.SelectedIndex = index;
        }

        private void UpdateVolume() {
            var defaultParameters = PlayerPreferences.Instance;
            defaultParameters.Volume = Volume.Value;
        }

        private async void OnCancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void UpdateSong() 
        {
            var defaultparameters = PlayerPreferences.Instance;
            defaultparameters.SelectedSong = SelectSong.SelectedItem.ToString();
        } 
        private void OnlyNumbers(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (((Entry)sender).Text.Length != 0)
                    Int32.Parse(((Entry)sender).Text);
            }
            catch (Exception)
            {
                ((Entry)sender).Text = ((Entry)sender).Text.Substring(0, ((Entry)sender).Text.Length - 1);
            }
        }

        private bool CheckSizeboard(int widgt, int height) 
        {
            return (widgt * height) > 6 && (widgt * height) % 2 == 0;

        }

        private async void OnApply(object sender, EventArgs e)
        {
            try
            {
                var defaultParameters = PlayerPreferences.Instance;
                if (DefaultRow.Text != null || DefaultColum.Text != null)
                {
                    if (DefaultRow.Text != null && DefaultColum.Text != null)
                    {
                        if (CheckSizeboard(int.Parse(DefaultColum.Text), int.Parse(DefaultRow.Text)))
                        {

                            defaultParameters.Row = int.Parse(DefaultRow.Text);
                            defaultParameters.Column = int.Parse(DefaultColum.Text);
                            SelectorDeck.UpdateDeck();
                            UpdateSong();
                            UpdateVolume();
                            await Navigation.PopAsync();
                        }
                        else throw new Exception("El tamaño del tablero debe de ser Par y de un tamaño mayor que 6.");
                    }
                    else throw new Exception("Se deben de rellenar tanto las filas como las columnas.");
                }
                else
                {
                    SelectorDeck.UpdateDeck();
                    UpdateSong();
                    UpdateVolume();
                    await Navigation.PopAsync();
                }
            }
            catch (Exception error)
            {
                ErrorView.IsVisible = true;
                TextError.Text = error.Message;
            }

        }

        private void ErrorViewClicked(object sender, EventArgs e)
        {
            ErrorView.IsVisible = false;
        }

        private void Volume_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            //here is the control logic volume
            MainPage.player.ChangeVolume(Volume.Value);
        }
    }
}
