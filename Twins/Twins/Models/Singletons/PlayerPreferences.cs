using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using Twins.Persistence;
using Twins.Persistence.DataTypes;

namespace Twins.Models.Singletons
{
    class PlayerPreferences
    {
        public static PlayerPreferences Instance {
            get {
                if (_instance == null)
                {
                    _instance = new PlayerPreferences();
                }
                return _instance;
            }
        }
        private static PlayerPreferences _instance = null;

        public int Column { get; set; }

        public int Row { get; set; }

        public List<string> Decks { get; set; }
        
        public string SelectedDeck { get; set; }

        public string SelectedSong { get; set; }

        public double Volume { get; set; }

        public TimeSpan LimitTime { get; set; }

        public TimeSpan TurnTime { get; set; }

        

        private PlayerPreferences()
        {
            var decks = new List<string>
            {
                "Animales",
                "Numeros",
                "Deportes"
            };
            Column = 6;
            Row = 4;
            Decks = decks;
            SelectedDeck = "Animales";
            SelectedSong = "Solve The Puzzle";
            Volume = 100.0;
            LimitTime = TimeSpan.Parse("0:01:00");
            TurnTime = TimeSpan.Parse("0:00:05");
        }
    }
}
