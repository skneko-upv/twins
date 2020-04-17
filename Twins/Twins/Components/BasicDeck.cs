using System;
using System.Collections.Generic;
using System.Text;
using Twins.Models;
using Xamarin.Forms;

namespace Twins.Components
{
    public class BasicDeck : Deck
    {
        public BasicDeck(ImageSource backImage, Queue<ImageSource> imageCards) : base(backImage, imageCards)
        {
        }

        public static BasicDeck CreateBasicDeck() 
        {
            Queue<ImageSource> imageCards = new Queue<ImageSource>();
            for (int i = 1; i < 10; i++)
                imageCards.Enqueue(ImageSource.FromFile("Assets/Cards/card" + i + ".png"));
            return new BasicDeck(ImageSource.FromFile("Assets/Cards/backimage.png"), imageCards);
        }
    }
}
