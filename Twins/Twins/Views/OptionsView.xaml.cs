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
            InitSelectionSongList();
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
                var defaultparameters = Twins.Models.DefaultParameters.Instance;
                if (DefaultRow.Text != null || DefaultColum.Text != null)
                {
                    if (DefaultRow.Text != null && DefaultColum.Text != null)
                    {
                        if (CheckSizeboard(int.Parse(DefaultColum.Text), int.Parse(DefaultRow.Text)))
                        {

                            defaultparameters.Row = int.Parse(DefaultRow.Text);
                            defaultparameters.Colum = int.Parse(DefaultColum.Text);
                            SelectorDeck.UpdateDeck();
                            await Navigation.PopAsync();
                        }
                        else throw new Exception("El tamaño del tablero debe de ser Par y de un tamaño mayor que 6.");
                    }
                    else throw new Exception("Se deben de rellenar tanto las filas como las columnas.");
                }
                else
                {
                    SelectorDeck.UpdateDeck();
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

        }
    }
}
