using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Twins.Models
{
    public class Card : IEquatable<Card>, ICloneable
    {
        public int Id { get; }

        public Deck Deck { get; }

        public ISet<Category> Categories;

        public ImageSource Image { get; }

        public Card(int id, Deck deck, ImageSource image)
        {
            Id = id;
            Deck = deck;
            Image = image;
        }

        public Card(int id, Deck deck, ImageSource image, ISet<Category> categories)
            : this(id, deck, image)
        {
            Categories = categories;
        }

        public bool Equals(Card other)
        {
            return Id == other.Id;
        }

        public object Clone()
        {
            return new Card(Id, Deck, Image, Categories);
        }
    }
}
