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
        private Board.Cell CellBoard { get; }
        private Deck Deck { get; }
        private bool Flipped { get; set; }

        public CardComponent()
        {
            InitializeComponent();
        }

        public CardComponent(Board.Cell cell, Deck deck)
        {
            InitializeComponent();
            Deck = deck;
            CellBoard = cell;
            Flipped = false;
            button.ImageSource = deck.BackImage;
        }

        private async void CardClicked(object sender, EventArgs e)
        {
            if (!Flipped)
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
            Flipped = true;
            await AnimationFlip(90, 150);
            button.ImageSource = CellBoard.Card.Image;
            await AnimationFlip(180, 150);
        }

        public async Task CardUnflip()
        {
            Flipped = false;
            await AnimationFlip(90, 150);
            button.ImageSource = Deck.BackImage;
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