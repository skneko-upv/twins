using System;
using System.Collections.Generic;
using System.Text;
using Twins.Models;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Card
    {
        public Card(ImageSource image, int id) 
        {
            this.Image = image;
            this.Id = id;
        }

        public bool Equals(Card otherCard) 
        {
            return this.Id == otherCard.Id;
        }
    }
}
