using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twins.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardComponent : StackLayout
    {
        private Card Card { get; }
        private ImageSource BackImage { get; }

        public CardComponent()
        {
            InitializeComponent();
            BackImage = ImageSource.FromFile("Assets/Cards/backimage.png");
        }

        public CardComponent(Card card)
        {
            InitializeComponent();
            BackImage = ImageSource.FromFile("Assets/Cards/backimage.png");
            Card = card;
        }

        private async void CardClicked(object sender, EventArgs e)
        {
            if (!Card.Flipped)
            {
                await CardFlip();
            }
        }

        private async Task AnimationFlip(int angle, uint seconds) 
        {
            await button.RotateYTo(angle, seconds);
        }


        public async Task CardFlip() 
        {
            Card.Flipped = true;
            await AnimationFlip(90, 150);
            button.ImageSource = Card.Image;
            await AnimationFlip(180, 150);
        }

        public async Task CardUnflip()
        {
            Card.Flipped = false;
            await AnimationFlip(90, 150);
            button.ImageSource = BackImage;
            await AnimationFlip(0, 150);
        }

        public async Task CardMatched() 
        {
            button.RotationY = 0;
            await AnimationFlip(360, 250);
            button.RotationY = 0;
        }
    }
}