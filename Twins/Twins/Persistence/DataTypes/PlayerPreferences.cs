using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using Twins.Models;

namespace Twins.Persistence.DataTypes
{
   [SQLite.Table("PlayerPreferences")]
    public class PlayerPreferences
    {
        [PrimaryKey]
        public int ID { get; set; }

        public int Column { get; set; }

        public int Row { get; set; }

        //public List<string> Decks { get; set; }

        public string SelectedDeck { get; set; }

        public string SelectedSong { get; set; }

        public double Volume { get; set; }

        public TimeSpan LimitTime { get; set; }

        public TimeSpan TurnTime { get; set; }

        [ForeignKey(typeof(PlayerInfo))]
        public int IdPlayer { get; }

        [OneToMany]
        public List<Deck> PlayerDecks { get; set; }

        public PlayerPreferences() {
            ID = 1;
           /* var decks = new List<string>
            {
                "Animales",
                "Numeros",
                "Deportes"
            };*/
            Column = 6;
            Row = 4;
           // Decks = decks;
            SelectedDeck = "Animales";
            SelectedSong = "Solve The Puzzle";
            Volume = 1.0;
            LimitTime = TimeSpan.Parse("0:01:00");
            TurnTime = TimeSpan.Parse("0:00:05");
        }
    }
}
