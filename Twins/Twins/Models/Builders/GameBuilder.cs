﻿using System;
using Twins.Components;

namespace Twins.Models.Builders
{
    public class GameBuilder
    {
        public enum GameKind
        {
            Standard,
            ReferenceCard
        }

        public int Height { get; private set; }
        public int Width { get; private set; }

        GameKind kind = GameKind.Standard;
        readonly Deck deck = BasicDeck.CreateBasicDeck();

        TimeSpan timeLimit = TimeSpan.FromMinutes(1);
        TimeSpan turnTimeLimit = TimeSpan.FromSeconds(5);

        Board.Cell[,] cells = null;

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

        public GameBuilder WithCellDistribution(Board.Cell[,] cells)
        {
            this.cells = cells;
            return this;
        }

        public Game Build()
        {
            switch (kind)
            {
                case GameKind.ReferenceCard:
                    {
                        return new ReferenceCardGame(
                            Height,
                            Width,
                            deck,
                            timeLimit,
                            turnTimeLimit,
                            cells);
                    }
                case GameKind.Standard:
                default:
                    {
                        return new StandardGame(
                            Height,
                            Width,
                            deck,
                            timeLimit,
                            turnTimeLimit,
                            cells);
                    }
            }
        }
    }
}
