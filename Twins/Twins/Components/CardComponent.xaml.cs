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
        readonly Card card;
        bool flipped;

        public CardComponent()
        {
            InitializeComponent();
        }

        public CardComponent(Card card)
        {
            InitializeComponent();

            this.card = card;
            flipped = false;
            button.ImageSource = card.Deck.BackImage;
        }

        public async Task Flip() 
        {
            flipped = true;
            await AnimationFlip(90, 150);
            button.ImageSource = card.Image;
            await AnimationFlip(180, 150);
        }

        public async Task Unflip()
        {
            flipped = false;
            await AnimationFlip(90, 150);
            button.ImageSource = card.Deck.BackImage;
            await AnimationFlip(0, 150);
        }

        public async Task Matched() 
        {
            button.RotationY = 0;
            await AnimationFlip(360, 250);
            button.RotationY = 0;
        }

        private async void CardClicked(object sender, EventArgs e)
        {
            if (!flipped)
            {
                await Flip();
            }
        }

        private async Task AnimationFlip(int angle, uint seconds)
        {
            await button.RotateYTo(angle, seconds);
        }
    }
}