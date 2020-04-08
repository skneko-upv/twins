using System.Collections.Generic;

namespace Twins.Models
{
    partial class Board
    {
        public static bool IsValidSize(int Height, int Width)
            => Height * Width % 2 == 0;

        public int Height { get; }

        public int Width { get; }

        public int Cells { get { return Height * Width; } }

        public Game Game { get; }

        public Deck Deck { get; }

        public Board(int height, int width, Game game, Deck deck)
        {
            Height = height;
            Width = width;
            Game = game;
            Deck = deck;
        }
    }
}
