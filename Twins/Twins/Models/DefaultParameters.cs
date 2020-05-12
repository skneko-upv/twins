using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twins.Models
{
    class DefaultParameters
    {
        private static DefaultParameters _instance = null;


        public int Column { get; set; }

        public int Row { get; set; }

        public List<string> Decks { get; set; }
        
        public string SelectedDeck { get; set; }

        public string SelectedSong { get; set; }

        public double Volume { get; set; }

        private DefaultParameters()
        {
            var decks = new List<string>();
            decks.Add("Animales");
            decks.Add("Numeros");
            decks.Add("Deportes");
            Column = 6;
            Row = 4;
            Decks = decks;
            SelectedDeck = "Animales";
            SelectedSong = "Solve The Puzzle";
            Volume = 100.0;
        }

        public static DefaultParameters Instance
        {
            get
            {
                // The first call will create the one and only instance.
                if (_instance == null)
                {
                    _instance = new DefaultParameters();
                }

                // Every call afterwards will return the single instance created above.
                return _instance;
            }
        }
    }
}
