using System;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Card : IEquatable<Card>
    {
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
