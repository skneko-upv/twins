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

        public CardComponent()
        {
            InitializeComponent();
        }

        public CardComponent(Card card) 
        {
            InitializeComponent();
            Card = card;
        }

        private async void CardClicked(object sender, EventArgs e)
        {
            if (!Card.Flipped) 
            {
                Card.Flipped = true;
                await AnimationFlip(90);
                button.ImageSource = Card.Image;
                await AnimationFlip(180);
            }
        }

        private async Task AnimationFlip(int angle) 
        {
            await button.RotateYTo(angle, 250);
        }

    }
}