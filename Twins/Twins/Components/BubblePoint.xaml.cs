﻿using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BubblePoint : Grid
    {
        public double PosX;
        public double PosY;
        public double PointOfReferenceX;
        public double PointOfReferenceY;
        public double CardWidth;
        public double CardHeight;

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

        public async void SetPosition(double x, double y)
        {
            PosX = x;
            PosY = y;
            await this.TranslateTo(x, y, 0);
        }

        public void setReferences(double pointX, double pointY, double w, double h) 
        {
            PointOfReferenceX = pointX;
            PointOfReferenceY = pointY;
            CardHeight = h;
            CardWidth = w;
        }

        public async Task GoUp() 
        {
            this.Opacity = 1;
            this.IsVisible = true;
            await this.TranslateTo(PosX, PosY - 15, 700);
            await this.FadeTo(0);
            this.IsVisible = false;
        }
    }
}