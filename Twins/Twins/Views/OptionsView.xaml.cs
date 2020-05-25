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
    public partial class OptionsView : ContentPage
    {
        public OptionsView()
        {
            InitializeComponent();
            InitPlayerPreferences();
            
        }

        private  void InitPlayerPreferences()
        {
          
            InitSelectionSongList();
            InitVolume();
            InitTime();
        }

        private void InitTime()
        {
            var defaultParameters = PlayerPreferences.Instance;
            MinutesEntry.Text = defaultParameters.LimitTime.Minutes.ToString();
            SecondsEntry.Text = defaultParameters.LimitTime.Seconds.ToString();
            TMinutesEntry.Text = defaultParameters.TurnTime.Minutes.ToString();
            TSecondsEntry.Text = defaultParameters.TurnTime.Seconds.ToString();
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
            MainPage.EffectsPlayer.Play();
        }
        private void UpdateSong() 
        {
            var defaultparameters = PlayerPreferences.Instance;
            defaultparameters.SelectedSong = SelectSong.SelectedItem.ToString();
            MainPage.Player.LoadSong(SelectSong.SelectedItem.ToString() + ".wav");
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
                                    
                                    UpdateSong();
                                    UpdateVolume();
                                    await UpdateDatabase();
                                    await Navigation.PopAsync();
                                }
                                else
                                {
                                    defaultParameters.LimitTime = TimeSpan.Parse("0:" + MinutesEntry.Text + ":" + SecondsEntry.Text);
                                    defaultParameters.Row = int.Parse(DefaultRow.Text);
                                    defaultParameters.Column = int.Parse(DefaultColum.Text);
                                    
                                    UpdateSong();
                                    UpdateVolume();
                                    await UpdateDatabase();
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
                                    
                                    UpdateSong();
                                    UpdateVolume();
                                    await UpdateDatabase();
                                    await Navigation.PopAsync();
                                }
                                else
                                {
                                    defaultParameters.Row = int.Parse(DefaultRow.Text);
                                    defaultParameters.Column = int.Parse(DefaultColum.Text);
                                    
                                    UpdateSong();
                                    UpdateVolume();
                                    await UpdateDatabase();
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
                            
                            UpdateSong();
                            UpdateVolume();
                            await UpdateDatabase();
                            await Navigation.PopAsync();
                        }
                        else
                        {
                            defaultParameters.LimitTime = TimeSpan.Parse("0:" + MinutesEntry.Text + ":" + SecondsEntry.Text);
                            
                            UpdateSong();
                            UpdateVolume();
                            await UpdateDatabase();
                            await Navigation.PopAsync();
                        }

                    }
                    else
                    {
                        if (HasTimeTLimit.IsChecked && IsTurnTimeLimitCorrect())
                        {
                            defaultParameters.TurnTime = TimeSpan.Parse("0:" + TMinutesEntry.Text + ":" + TSecondsEntry.Text);
                            
                            UpdateSong();
                            UpdateVolume();
                            await Navigation.PopAsync();
                        }
                        else
                        {
                           
                            
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
            MainPage.EffectsPlayer.Play();
        }

        private async Task UpdateDatabase()
        {
            var database = Database.Instance;
            var playerPreferencesDB = await database.GetPlayerPreferences();
            var playerPreferences = PlayerPreferences.Instance;
            playerPreferencesDB.Column=playerPreferences.Column;
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

        private void Volume_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            //here is the control logic volume
            if (Volume.Value != 0.0 && MainPage.Player.GetVolume() != 0.0) 
            {
                MainPage.Player.ChangeVolume(Volume.Value);
                MainPage.EffectsPlayer.ChangeVolume(Volume.Value);
            }
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
