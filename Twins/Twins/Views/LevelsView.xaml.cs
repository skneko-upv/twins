using System;
using Twins.Components;
using Twins.Models;
using Twins.Persistence;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LevelsView : ContentPage
    {
        public LevelsView()
        {
            InitializeComponent();

            for (int i = 0; i < 2; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }
            for (int i = 0; i < 5; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
        }

        protected override void OnAppearing()
        {
            FillGrid();
        }

        private async void FillGrid()
        {
            int level = 1;

            PlayerInfo saved = await Database.Instance.GetPlayerInfo();
            Grid.Children.Add(new LevelComponent(Levels.Level1, level++, saved.LastLevelPassed), 0, 0);
            Grid.Children.Add(new LevelComponent(Levels.Level2, level++, saved.LastLevelPassed), 1, 0);
            Grid.Children.Add(new LevelComponent(Levels.Level3, level++, saved.LastLevelPassed), 2, 0);
            Grid.Children.Add(new LevelComponent(Levels.Level4, level++, saved.LastLevelPassed), 3, 0);
            Grid.Children.Add(new LevelComponent(Levels.Level5, level++, saved.LastLevelPassed), 4, 0);
            Grid.Children.Add(new LevelComponent(Levels.Level6, level++, saved.LastLevelPassed), 0, 1);
            Grid.Children.Add(new LevelComponent(Levels.Level7, level++, saved.LastLevelPassed), 1, 1);
            Grid.Children.Add(new LevelComponent(Levels.Level8, level++, saved.LastLevelPassed), 2, 1);
            Grid.Children.Add(new LevelComponent(Levels.Level9, level++, saved.LastLevelPassed), 3, 1);
            Grid.Children.Add(new LevelComponent(Levels.Level10, level++, saved.LastLevelPassed), 4, 1);
        }

        private async void Back(object sender, EventArgs e)
        {
            MainPage.EffectsPlayer.Play();
            await Navigation.PopAsync();
        }
    }
}