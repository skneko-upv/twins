using System;
using Twins.Models;
using Twins.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelComponent : AbsoluteLayout
    {
        private Game Game;
        private int Number;

        public LevelComponent(Game game, int number, int lvlPassed)
        {
            InitializeComponent();
            Game = game;
            Number = number;
            NumberText.Text = number + "";
            setStatusImage(++lvlPassed);
        }

        private void setStatusImage(int lvlPassed)
        {
            if (lvlPassed < Number)
            {
                StatusImage.Source = "Assets/Icons/levelBlocked.png";

            }
            else if (lvlPassed > Number)
            {
                StatusImage.Source = "Assets/Icons/levelPassed.png";
                ButtonLvl.IsVisible = true;
            }
            else
            {
                StatusImage.Source = "";
                ButtonLvl.IsVisible = true;
            }
        }

        private async void ClickedLevel(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BoardView(Game.Board));
        }
    }
}