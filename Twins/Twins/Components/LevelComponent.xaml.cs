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
        private readonly GameBuilder gameBuilder;
        private readonly int number;

        public LevelComponent(GameBuilder game, int number, int lvlPassed)
        {
            InitializeComponent();
            gameBuilder = game;
            this.number = number;
            NumberText.Text = number + "";
            SetStatusImage(1 + lvlPassed);
        }

        public void SetStatusImage(int lvlPassed)
        {
            if (lvlPassed < number)
            {
                StatusImage.Source = "Assets/Icons/levelBlocked.png";

            }
            else if (lvlPassed > number)
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
            MainPage.EffectsPlayer.Play();
            var game = gameBuilder
                        .WithLevelNumber(number).Build();
            await Navigation.PushAsync(new BoardView(game.Board));
        }
    }
}