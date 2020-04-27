using System.Collections.Generic;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Deck
    {
        public ImageSource BackImage { get; }

        public List<Card> Cards { get; }

        public Deck(ImageSource backImage, Queue<ImageSource> imageCards)
        {
            BackImage = backImage;
            Cards = new List<Card>();

            int i = 0;
            foreach (ImageSource image in imageCards)
            {
                Cards.Add(new Card(i, this, image));
                i++;
            }
        }
    }
}
