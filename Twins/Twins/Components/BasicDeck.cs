using System.Collections.Generic;
using Twins.Models;
using Xamarin.Forms;

namespace Twins.Components
{
    public abstract class BasicDeck
    {
        public static readonly Deck Deck1 = new Deck(ImageSource.FromFile("Assets/Decks/Deck1/backimage.png"), LoadImages("Assets/Decks/Deck1/"));

        public static readonly Deck Deck2 = new Deck(ImageSource.FromFile("Assets/Decks/Deck2/backimage.png"), LoadImages("Assets/Decks/Deck2/"));

        public static readonly Deck Deck3 = new Deck(ImageSource.FromFile("Assets/Decks/Deck3/backimage.png"), LoadImages("Assets/Decks/Deck3/"));

        private static Queue<ImageSource> LoadImages(string path)
        {
            Queue<ImageSource> imageCards = new Queue<ImageSource>();
            for (int i = 1; i < 13; i++)
            {
                imageCards.Enqueue(ImageSource.FromFile(path + "card" + i + ".png"));
            }
            return imageCards;
        }
    }
}
