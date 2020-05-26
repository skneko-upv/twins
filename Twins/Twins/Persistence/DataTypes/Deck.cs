using SQLite;
using System.Collections.Generic;
using System.Linq;
using Twins.Models;

namespace Twins.Persistence.DataTypes
{
    [SQLite.Table("Decks")]
    public class Deck
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string[] Images { get; set; }
        public string[][] Categories { get; set; }

        public Deck()
        {
            Images = new string[0];
        }

        public Deck(ICollection<string> images, IDictionary<int, ISet<Category>> categories)
        {
            Images = images.ToArray();
            foreach (KeyValuePair<int, ISet<Category>> entry in categories)
            {
                Categories[entry.Key] = entry.Value.Select(e => e.Name).ToArray();
            }
        }
    }
}
