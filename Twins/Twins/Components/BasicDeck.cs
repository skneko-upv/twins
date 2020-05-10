using System.Collections.Generic;
using Twins.Models;
using Xamarin.Forms;

namespace Twins.Components
{
    public abstract class BasicDeck
    {
        public static readonly Deck Animales = new Deck(ImageSource.FromFile("Assets/Decks/Deck1/backimage.png"), LoadImages("Assets/Decks/Deck1/"), "Animales");

        public static readonly Deck Numeros = new Deck(ImageSource.FromFile("Assets/Decks/Deck2/backimage.png"), LoadImages("Assets/Decks/Deck2/"), "Números");

        public static readonly Deck Deportes = new Deck(ImageSource.FromFile("Assets/Decks/Deck3/backimage.png"), LoadImages("Assets/Decks/Deck3/"), "Deportes");

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
