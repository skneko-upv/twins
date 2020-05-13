using SQLite;
using System.Collections.Generic;

namespace Twins.Persistence.DataTypes
{
   [Table ("DefaultConfiguration")]
    class PlayerPreferences
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int Columns { get; set; }

        public int Rows { get; set; }

        public List<string> Decks { get; set; }

        public string SelectedDeck { get; set; }
    }
}
