using System;
using System.Collections.Generic;
using System.Text;
using Twins.Components;

namespace Twins.Models.Builders
{
    public class GameBuilder
    {
        public enum GameKind
        {
            Standard
        }

        public int Height { get; private set; }
        public int Width { get; private set; }

        GameKind kind = GameKind.Standard;
        readonly Deck deck = BasicDeck.CreateBasicDeck();

        TimeSpan timeLimit = TimeSpan.FromMinutes(1);
        TimeSpan turnTimeLimit = TimeSpan.FromSeconds(5);

        public GameBuilder(int height, int width)
        {
            Height = height;
            Width = width;
        }

        public GameBuilder OfKind(GameKind kind)
        {
            this.kind = kind;
            return this;
        }

        public GameBuilder WithTimeLimit(TimeSpan limit)
        {
            timeLimit = limit;
            return this;
        }

        public GameBuilder WithTurnTimeLimit(TimeSpan limit)
        {
            turnTimeLimit = limit;
            return this;
        }

        public Game Build()
        {
            if (kind == GameKind.Standard)
            {
                return new StandardGame(
                    Height,
                    Width,
                    deck,
                    timeLimit,
                    turnTimeLimit);
            }
            return null; // unreachable
        }
    }
}
