using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BubblePoint : Grid
    {
        public BubblePoint()
        {
            InitializeComponent();
        }

        public BubblePoint SetRedBubble()
        {
            ImageBubble.Source = "Assets/Backgrounds/bubbleRBackground.png";
            return this;
        }
        public BubblePoint SetGreenBubble()
        {
            ImageBubble.Source = "Assets/Backgrounds/bubbleGBackground.png";
            return this;
        }

        public BubblePoint SetPoints(int points)
        {
            if (points <= 0)
                LabelText.Text = "" + points;
            else
                LabelText.Text = "+" + points;
            return this;
        }

        public async void SetPosition(int x, int y)
        {
            await this.TranslateTo(x, y, 0);
        }

        public async void GoUp() 
        {
            this.IsVisible = true;
            await this.TranslateTo(this.X, this.Y + 5, 1500);
            await this.FadeTo(0);
            this.IsVisible = false;
        }
    }
}