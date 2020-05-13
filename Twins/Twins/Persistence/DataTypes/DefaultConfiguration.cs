using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Twins.Persistence.DataTypes
{
   [Table ("DefaultConfiguration")]
    class DefaultConfiguration
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int Column { get; set; }

        public int Row { get; set; }

        public List<string> Decks { get; set; }

        public string SelectedDeck { get; set; }

         
    }
}
