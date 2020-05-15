using System;
using Twins.Components;
using Twins.Models.Game;
using Twins.Models.Strategies;

namespace Twins.Models.Builders
{
    public class GameBuilder
    {
        public enum GameKind
        {
            Standard,
            ReferenceCard,
            Category
        }

        public int Height { get; private set; }
        public int Width { get; private set; }

        GameKind kind = GameKind.Standard;
         
        Deck deck = BuiltInDecks.Animals.Value;

        TimeSpan timeLimit = TimeSpan.FromMinutes(1);
        TimeSpan turnTimeLimit = TimeSpan.FromSeconds(5);

        Board.Cell[,] cells = null;
        int groupSize = 2;

        int level = 0;

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

        public GameBuilder WithDeck(Deck deck) 
        {
            this.deck = deck;
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

        public GameBuilder WithCellDistribution(Board.Cell[,] cells, int groupSize)
        {
            this.cells = cells;
            this.groupSize = groupSize;
            return this;
        }

        public GameBuilder WithGroupSize(int groupSize)
        {
            this.groupSize = groupSize;
            return this;
        }

        /// <summary>
        /// Makes a game easy to win. Used for tests or tutorials.
        /// </summary>
        public GameBuilder WithPredictablePopulation()
        {
            cells = new PredictablePopulationStrategy(groupSize, deck).Populate(Height, Width);
            return this;
        }

        public GameBuilder WithLevelNumber(int level)
        {
            this.level = level;
            return this;
        }

        public IGame Build()
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
                            cells,
                            level);
                    }
                case GameKind.Category:
                    {
                        return new CategoryGame(
                            Height,
                            Width,
                            deck,
                            timeLimit,
                            turnTimeLimit,
                            cells,
                            level);
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
                            cells,
                            level);
                    }
            }
        }
    }
}
