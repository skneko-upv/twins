using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Twins.Models
{
    partial class Board
    {
        public static bool IsValidSize(int height, int width)
            => height * width % 2 == 0;

        public static bool IsDeckBigEnough(Deck deck, int height, int width)
            => deck.Cards.Count >= height * width / 2;

        public int Height { get; }

        public int Width { get; }

        public Game Game { get; }

        public Deck Deck { get; }

        public Card ReferenceCard {
            get => referenceCard;
            set {
                referenceCard = value;
                ReferenceChanged(value);
            } 
        }
        private Card referenceCard;

        public Cell FlippedCard { get; private set; }

        public event Action<Card> ReferenceChanged;

        public event Action<Cell> CardFlipped;

        public event Action<Cell, bool> RevealedCardsChanged;

        public IReadOnlyCollection<Cell> Cells => cellMap.Values;

        public Board(int height, int width, Game game, Deck deck)
        {
            Height = height;
            Width = width;
            Game = game;
            Deck = deck;

            Initialize();
        }
    }
}
