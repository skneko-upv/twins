using System;
using Twins.Models;
using Twins.Models.Builders;
using Twins.Utils;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FreeModeForm : ContentPage
    {
        private bool categoryNotSelectedYet;
        public FreeModeForm()
        {            

            InitializeComponent();
            categoryNotSelectedYet = true;
        }

        private async void OnBackMainMenu(object sender, EventArgs e)
        {
            //resume
            //Go Back to Main Menu
            await Navigation.PopAsync();
        }

        private async void OnStartGame(object sender, EventArgs e)
        {
            //resume
            //Start the game with de parameter of the form
            try
            {
                if (Int32.Parse(ColumnsEntry.Text) < 2)
                    throw new Exception("Se necesita como mínimo 2 columnas");
                if(Int32.Parse(RowsEntry.Text) < 2)
                    throw new Exception("Se necesita como mínimo 2 filas");
                if (Int32.Parse(RowsEntry.Text) * Int32.Parse(ColumnsEntry.Text) % 2 != 0)
                    throw new Exception("Se necesita un número par de cartas para el tablero. Elija un número de columnas y filas correcto.");
                if (SongPicker.SelectedItem==null)
                    throw new Exception("Se necesita seleccionar una canción");


                Game game;
                var gameBuilder = new GameBuilder(Int32.Parse(ColumnsEntry.Text), Int32.Parse(RowsEntry.Text));

                SetTypeOfGame(gameBuilder);
                SetTimeOfGame(gameBuilder);
                SetTurnTimeOfGame(gameBuilder);
                SetSongGame();

                game = gameBuilder.Build();

                await Navigation.PushAsync(new BoardView(game.Board));
            } 
            catch (Exception error) 
            {
                ErrorView.IsVisible = true;
                TextError.Text = error.Message;
            }
}

        private void SetTurnTimeOfGame(GameBuilder gameBuilder)
        {
            if (HasTimeTLimit.IsChecked && IsTurnTimeLimitCorrect())
                    gameBuilder.WithTurnTimeLimit(TimeSpan.Parse("0:" + TMinutesEntry.Text + ":" + TSecondsEntry.Text));
        }

        private void SetTimeOfGame(GameBuilder gameBuilder)
        {
            if (HasTimeLimit.IsChecked) {
                if (Int32.Parse(MinutesEntry.Text) != 0 || Int32.Parse(SecondsEntry.Text) != 0)
                    gameBuilder.WithTimeLimit(TimeSpan.Parse("0:" + MinutesEntry.Text + ":" + SecondsEntry.Text));
                else throw new Exception("El tiempo de la partida no puede ser 00:00.");
            }
        }

        private void SetTypeOfGame(GameBuilder gameBuilder)
        {
            if (categoryNotSelectedYet) throw new Exception("Elije un tipo de partida antes de jugar, por favor.");
            switch (CategoryPicker.SelectedIndex) {
                case 0:
                    gameBuilder.OfKind(GameBuilder.GameKind.Standard);
                    break;

                case 1:
                    gameBuilder.OfKind(GameBuilder.GameKind.ReferenceCard);
                    break;

                case 2:
                    throw new NotImplementedException();
            }
        }

        private void SetSongGame() 
        {
            var player = new AudioPlayer();
            player.LoadSong(SongPicker.SelectedItem + ".wav");
        }

        private bool IsTurnTimeLimitCorrect()
        {
            if (Int32.Parse(TMinutesEntry.Text) != 0 || Int32.Parse(TSecondsEntry.Text) != 0)
                if (0 <= TimeSpan.Parse("0:" + MinutesEntry.Text + ":" + SecondsEntry.Text).CompareTo(TimeSpan.Parse("0:" + TMinutesEntry.Text + ":" + TSecondsEntry.Text)))
                    return true;
                else throw new Exception("El tiempo por turno no puede ser superior al tiempo limite.");
            else throw new Exception("El tiempo por turno no puede ser 00:00.");
        }

        private void OnlyNumbers(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(((Entry)sender).Text.Length!=0)
                    Int32.Parse(((Entry) sender).Text);
            }
            catch (Exception) {
                ((Entry)sender).Text = ((Entry)sender).Text.Substring(0, ((Entry)sender).Text.Length - 1);
            }
        }

        private void CategoryPickerChanged(object sender, EventArgs e)
        {
            categoryNotSelectedYet = false;
        }

        private void ErrorViewClicked(object sender, EventArgs e)
        {
            ErrorView.IsVisible = false;
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