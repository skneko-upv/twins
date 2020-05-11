using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
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
        public ResultOfGame LastLevelResult;
        public LevelsView()
        {
            InitializeComponent();
            FillGrid();
        }

        private async void FillGrid()
        {
            for (int i = 0; i < 2; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1,GridUnitType.Star)});
            }
            for (int i = 0; i < 5; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }
            int level = 1;
            var lvlPassed = await Database.Instance.GetPlayerInfo();
            LastLevelResult = new ResultOfGame(lvlPassed.LastLevelPassed);
            Grid.Children.Add(new LevelComponent(Levels.level1, level++, lvlPassed.LastLevelPassed, LastLevelResult), 0, 0);
            Grid.Children.Add(new LevelComponent(Levels.level2, level++, lvlPassed.LastLevelPassed, LastLevelResult), 1, 0);
            Grid.Children.Add(new LevelComponent(Levels.level3, level++, lvlPassed.LastLevelPassed, LastLevelResult), 2, 0);
            Grid.Children.Add(new LevelComponent(Levels.level4, level++, lvlPassed.LastLevelPassed, LastLevelResult), 3, 0);
            Grid.Children.Add(new LevelComponent(Levels.level5, level++, lvlPassed.LastLevelPassed, LastLevelResult), 4, 0);
            Grid.Children.Add(new LevelComponent(Levels.level6, level++, lvlPassed.LastLevelPassed, LastLevelResult), 0, 1);
            Grid.Children.Add(new LevelComponent(Levels.level7, level++, lvlPassed.LastLevelPassed, LastLevelResult), 1, 1);
            Grid.Children.Add(new LevelComponent(Levels.level8, level++, lvlPassed.LastLevelPassed, LastLevelResult), 2, 1);
            Grid.Children.Add(new LevelComponent(Levels.level9, level++, lvlPassed.LastLevelPassed, LastLevelResult), 3, 1);
            Grid.Children.Add(new LevelComponent(Levels.level10, level++, lvlPassed.LastLevelPassed, LastLevelResult), 4, 1);
        }

        private async void Back(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void AppearingForm(object sender, EventArgs e)
        {
            if (LastLevelResult.IsVictory)
            {
                var info = await Database.Instance.GetPlayerInfo();
                if(info.LastLevelPassed < LastLevelResult.LevelNumber) {
                    info.LastLevelPassed = LastLevelResult.LevelNumber;
                    for (int i = 0; i < 10; i++)
                        ((LevelComponent)Grid.Children.ElementAt(i)).setStatusImage(info.LastLevelPassed+1);
                    
                    await Database.Instance.SavePlayerInfo(info);
                }
            }else
                LastLevelResult.LevelNumber = 0;
        }
    }
}