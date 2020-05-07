using System.Collections.Generic;

namespace Twins.Models
{
    public class Category
    {
        public int Id { get; }

        public string Name { get; }

        public ICollection<Card> Cards { get; }

        public Category(int id, string name, ICollection<Card> cards)
        {
            Id = id;
            Name = name;
            Cards = cards;
        }
    }
}
