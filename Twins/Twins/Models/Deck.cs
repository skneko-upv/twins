using System.Collections.Generic;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Deck
    {
        public ImageSource BackImage { get; }

        public ISet<Category> Categories;

        public IList<Card> Cards { get; }

        public string Name { get; }

        public Deck(ImageSource backImage, Queue<ImageSource> imageCards, string name)
        {
            BackImage = backImage;
            Cards = new List<Card>();
            Name = name;

            int i = 0;
            foreach (ImageSource image in imageCards)
            {
                Cards.Add(new Card(i, this, image));
                i++;
            }
        }
    }
}
