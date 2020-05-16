using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
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

        private bool IsTimeLimitCorrect() {
             
            if (Int32.Parse(MinutesEntry.Text) != 0 || Int32.Parse(SecondsEntry.Text) != 0)
            {
                return true;
            } else throw new Exception("El tiempo de la partida no puede ser 00:00.");
        }

        private bool IsTurnTimeLimitCorrect()
        {
            if (Int32.Parse(TMinutesEntry.Text) != 0 || Int32.Parse(TSecondsEntry.Text) != 0)
                if (0 <= TimeSpan.Parse("0:" + MinutesEntry.Text + ":" + SecondsEntry.Text).CompareTo(TimeSpan.Parse("0:" + TMinutesEntry.Text + ":" + TSecondsEntry.Text)))
                    return true;
                else throw new Exception("El tiempo por turno no puede ser superior al tiempo limite.");
            else throw new Exception("El tiempo por turno no puede ser 00:00.");
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
                            if (HasTimeLimit.IsChecked && IsTimeLimitCorrect())
                            {
                                if (HasTimeTLimit.IsChecked && IsTurnTimeLimitCorrect())
                                {
                                    defaultParameters.LimitTime = TimeSpan.Parse("0:" + MinutesEntry.Text + ":" + SecondsEntry.Text);
                                    defaultParameters.TurnTime = TimeSpan.Parse("0:" + TMinutesEntry.Text + ":" + TSecondsEntry.Text);
                                    defaultParameters.Row = int.Parse(DefaultRow.Text);
                                    defaultParameters.Column = int.Parse(DefaultColum.Text);
                                    SelectorDeck.UpdateDeck();
                                    UpdateSong();
                                    UpdateVolume();
                                    await Navigation.PopAsync();
                                }
                                else
                                {
                                    defaultParameters.LimitTime = TimeSpan.Parse("0:" + MinutesEntry.Text + ":" + SecondsEntry.Text);
                                    defaultParameters.Row = int.Parse(DefaultRow.Text);
                                    defaultParameters.Column = int.Parse(DefaultColum.Text);
                                    SelectorDeck.UpdateDeck();
                                    UpdateSong();
                                    UpdateVolume();
                                    await Navigation.PopAsync();
                                }

                            }
                            else
                            {
                                if (HasTimeTLimit.IsChecked && IsTurnTimeLimitCorrect())
                                {
                                    defaultParameters.TurnTime = TimeSpan.Parse("0:" + TMinutesEntry.Text + ":" + TSecondsEntry.Text);
                                    defaultParameters.Row = int.Parse(DefaultRow.Text);
                                    defaultParameters.Column = int.Parse(DefaultColum.Text);
                                    SelectorDeck.UpdateDeck();
                                    UpdateSong();
                                    UpdateVolume();
                                    await Navigation.PopAsync();
                                }
                                else
                                {
                                    defaultParameters.Row = int.Parse(DefaultRow.Text);
                                    defaultParameters.Column = int.Parse(DefaultColum.Text);
                                    SelectorDeck.UpdateDeck();
                                    UpdateSong();
                                    UpdateVolume();
                                    await Navigation.PopAsync();
                                }
                            
                            } 
                        } else throw new Exception("El tamaño del tablero debe de ser Par y de un tamaño mayor que 6.");
                    }
                    else throw new Exception("Se deben de rellenar tanto las filas como las columnas.");
                }
                else
                {
                    if (HasTimeLimit.IsChecked && IsTimeLimitCorrect())
                    {
                        if (HasTimeTLimit.IsChecked && IsTurnTimeLimitCorrect())
                        {
                            defaultParameters.LimitTime = TimeSpan.Parse("0:" + MinutesEntry.Text + ":" + SecondsEntry.Text);
                            defaultParameters.TurnTime = TimeSpan.Parse("0:" + TMinutesEntry.Text + ":" + TSecondsEntry.Text);
                            SelectorDeck.UpdateDeck();
                            UpdateSong();
                            UpdateVolume();
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            defaultParameters.LimitTime = TimeSpan.Parse("0:" + MinutesEntry.Text + ":" + SecondsEntry.Text);
                            SelectorDeck.UpdateDeck();
                            UpdateSong();
                            UpdateVolume();
                            await Navigation.PopAsync();
                        }

                    }
                    else
                    {
                        if (HasTimeTLimit.IsChecked && IsTurnTimeLimitCorrect())
                        {
                            defaultParameters.TurnTime = TimeSpan.Parse("0:" + TMinutesEntry.Text + ":" + TSecondsEntry.Text);
                            SelectorDeck.UpdateDeck();
                            UpdateSong();
                            UpdateVolume();
                            await Navigation.PopAsync();
                        }
                        else
                        {
                           
                            SelectorDeck.UpdateDeck();
                            UpdateSong();
                            UpdateVolume();
                            await Navigation.PopAsync();
                        }

                    }
                }
            }
            catch (Exception error)
            {
                ErrorView.IsVisible = true;
                ErrorView.SetTextError(error.Message);
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
