using System;

namespace Twins.Models
{
    partial class Card : IEquatable<Card>
    {
        readonly int id;

        public bool Equals(Card other) 
            => id == other.id;
    }
}
