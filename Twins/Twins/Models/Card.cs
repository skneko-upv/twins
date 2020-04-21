using System;

namespace Twins.Models
{
    public partial class Card : IEquatable<Card>
    {
        readonly int id;

        public bool Equals(Card other) 
            => id == other.id;
    }
}
