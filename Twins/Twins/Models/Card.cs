using System;
using Xamarin.Forms;

namespace Twins.Models
{
    public class Card : IEquatable<Card>
    {
        public int Id { get; }

        public Deck Deck { get; }

        public ImageSource Image { get; }

        public Card(int id, Deck deck, ImageSource image)
        {
            Id = id;
            Deck = deck;
            Image = image;
        }

        public bool Equals(Card other)
        {
            return Id == other.Id;
        }
    }
}
