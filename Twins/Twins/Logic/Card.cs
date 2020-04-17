using System;
using System.Collections.Generic;
using System.Text;
using Twins.Models;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Card
    {
        public Card(ImageSource image) 
        {
            this.Image = image;
            this.Flipped = false;
        }

        public void Flip() 
        {
            this.Flipped = true;
        }

        public void Unflip()
        {
            this.Flipped = false;
        }

        public bool Equals(Card otherCard) 
        {
            return this.Id == otherCard.Id;
        }
    }
}
