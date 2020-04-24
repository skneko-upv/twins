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
        public Card Card { get; set; }
        public bool Flipped { get; private set; }

        public event Action Clicked;

        public CardComponent()
        {
            InitializeComponent();
        }

        public CardComponent(Card card)
        {
            InitializeComponent();

            this.Card = card;
            Flipped = false;
            button.ImageSource = card.Deck.BackImage;
        }

        public async Task Flip() 
        {
            Flipped = true;
            await AnimationFlip(90, 150);
            button.ImageSource = Card.Image;
            await AnimationFlip(180, 150);
        }

        public async Task Unflip()
        {
            Flipped = false;
            await AnimationFlip(90, 150);
            button.ImageSource = Card.Deck.BackImage;
            await AnimationFlip(0, 150);
        }

        public async Task Matched() 
        {
            button.RotationY = 0;
            await AnimationFlip(360, 250);
            button.RotationY = 0;
        }

        private void OnClicked(object sender, EventArgs e)
        {
            Clicked();
        }

        private async Task AnimationFlip(int angle, uint seconds)
        {
            await button.RotateYTo(angle, seconds);
        }
    }
}