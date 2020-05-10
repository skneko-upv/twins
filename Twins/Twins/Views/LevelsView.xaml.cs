using System;
using System.CodeDom.Compiler;
using Twins.Components;
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
            FillGrid();
        }

        private async void FillGrid()
        {
            for (int i = 0; i < 2; i++)
            {
                Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(250)});
            }
            for (int i = 0; i < 5; i++)
            {
                Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200)});
            }
            int level = 1;
            var lvlPassed = await Database.Instance.GetPlayerInfo();
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 5; j++)
                    Grid.Children.Add(new LevelComponent(null, level++, lvlPassed.LastLevelPassed), j, i);
        }

        private async void Back(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}