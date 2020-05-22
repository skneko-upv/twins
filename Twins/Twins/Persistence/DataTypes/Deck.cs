using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Twins.Persistence.DataTypes
{
    [SQLite.Table("Decks")]
    public class Deck
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }

        public Deck() { ID = 1; }

        }
}
