using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Twins.Tests")]
namespace Twins.Models.Singletons
{

    internal class PlayerPreferences
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

        public IReadOnlyList<Deck> Decks => BuiltInDecks.Concat(PlayerDecks).ToList();

        public IList<Deck> BuiltInDecks { get; }

        public IList<Deck> PlayerDecks { get; }

        public Deck SelectedDeck { get; set; }

        public string SelectedSong { get; set; }

        public string ButtonEffect { get; set; }

        public string TurnCardEffect { get; set; }

        public string UnturnCardEffect { get; set; }

        public string WinEffect { get; set; }

        public string LoseEffect { get; set; }

        public string ClockTimerEffect { get; set; }

        public double Volume { get; set; }

        public TimeSpan LimitTime { get; set; }

        public TimeSpan TurnTime { get; set; }

        private PlayerPreferences()
        {
            Column = 6;
            Row = 4;
            BuiltInDecks = new List<Deck> { Components.BuiltInDecks.Animals.Value, Components.BuiltInDecks.Numbers.Value, Components.BuiltInDecks.Sports.Value };
            PlayerDecks = new List<Deck>();
            SelectedDeck = BuiltInDecks[0];
            SelectedSong = "Solve The Puzzle";
            ButtonEffect = "menuButtonSound";
            TurnCardEffect = "Voltear Carta";
            UnturnCardEffect = "Voltear Carta Invertido";
            WinEffect = "Ta Da";
            LoseEffect = "Lose Sound";
            ClockTimerEffect = "clockticksound";
            Volume = 100.0;
            LimitTime = TimeSpan.Parse("0:01:00");
            TurnTime = TimeSpan.Parse("0:00:05");
        }
    }
}
