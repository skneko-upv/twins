using System;
using System.Threading.Tasks;
using Twins.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Twins.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardComponent : Grid
    {
        public Card Card { get; set; }
        public bool Flipped { get; private set; }

        public event Action Clicked;

        public bool IsBlocked { get; set; }

        public BubblePoint BubblePoint { get;  }

        public CardComponent()
        {
            InitializeComponent();

        }

        public CardComponent(Card card, bool isBlocked = false)
        {
            InitializeComponent();

            Card = card;
            Flipped = false;
            button.ImageSource = card.Deck.BackImage;
            IsBlocked = isBlocked;


            BubblePoint = new BubblePoint();
            root.Children.Add(BubblePoint);
            BubblePoint.SetPosition(0, -30);
        }

        public async Task Flip()
        {
            if (!IsBlocked)
            {
                IsEnabled = false;
            }

            Flipped = true;
            await AnimationFlip(90, 150);
            button.ImageSource = Card.Image;
            await AnimationFlip(0, 150);
        }

        public async Task Unflip()
        {
            if (!IsBlocked)
            {
                IsEnabled = true;
            }

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
            if(!Flipped)
                Clicked();
        }
        private async Task AnimationFlip(int angle, uint seconds)
        {
            await button.RotateYTo(angle, seconds);
        }
        public async Task ShowRedPoints(int points)
        {

            await BubblePoint.SetRedBubble().SetPoints(points).GoUp();
            BubblePoint.SetPosition(0, -30);
        }
        public async Task ShowGreenPoints(int points)
        {

            await BubblePoint.SetGreenBubble().SetPoints(points).GoUp();
            BubblePoint.SetPosition(0, -30);
        }
    }
}