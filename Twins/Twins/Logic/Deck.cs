using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Deck
    {
        public Deck(ImageSource backImage, Queue<ImageSource> imageCards) 
        {
            this.BackImage = backImage;
            this.Cards = new List<Card>();

            Card cardA, cardB;
            
            int i = 0;
            foreach (ImageSource image in imageCards) 
            {
                cardA = new Card(image, i);
                cardB = new Card(image, i);
                cardA.Pair = cardB;
                cardB.Pair = cardA;
                this.Cards.Add(cardA);
                this.Cards.Add(cardB);
                i++;
            }
        }
    }
}
