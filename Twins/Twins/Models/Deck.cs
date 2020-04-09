using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Deck
    {
        private ImageSource backImage;
        private List<Card> cards;

        public ImageSource BackImage { get => backImage; set => backImage = value; }
        public List<Card> Cards { get => cards; set => cards = value; }
    }
}
