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
            this.cards = new List<Card>();

            foreach (ImageSource image in imageCards) 
            {
                this.cards.Add(new Card(image));
                this.cards.Add(new Card(image));
            }
        }
    }
}
