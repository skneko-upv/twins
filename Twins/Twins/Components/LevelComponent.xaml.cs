using System;
using Twins.Models;
using Twins.Models.Builders;
using Twins.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelComponent : AbsoluteLayout
    {
        private GameBuilder GameBuilder;
        private Game Game;
        private int Number;
        private ResultOfGame ResultOfGame;

        public LevelComponent(GameBuilder game, int number, int lvlPassed, ResultOfGame result)
        {
            InitializeComponent();
            GameBuilder = game;
            Number = number;
            ResultOfGame = result;
            NumberText.Text = number + "";
            setStatusImage(1+lvlPassed);
        }

        public void setStatusImage(int lvlPassed)
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
            Game = GameBuilder.Build();
            Game.LevelNumber = Number;
            Game.ResultOfGame = ResultOfGame;
            await Navigation.PushAsync(new BoardView(Game.Board));

        }
    }
}