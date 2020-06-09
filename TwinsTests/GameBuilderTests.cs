using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Twins.Components;
using Twins.Models;
using Twins.Models.Builders;
using Twins.Models.Game;

namespace TwinsTests
{
    [TestClass]
    public class GameBuilderTests
    {
        private const int DefaultHeight = 4;
        private const int DefaultWidth = 6;
        private const int DefaultLevel = 0;
        private static readonly Deck DefaultDeck = BuiltInDecks.Animals.Value;
        private static readonly TimeSpan DefaultTimeLimit = TimeSpan.FromMinutes(1);
        private static readonly TimeSpan DefaultTurnTimeLimit = TimeSpan.FromSeconds(5);
        private static readonly StandardGame DefaultKindGame = new StandardGame(DefaultHeight, DefaultWidth, DefaultDeck,
            DefaultTimeLimit, DefaultTurnTimeLimit);

        private const int CustomtHeight =5;
        private const int CustomWidth = 3;
        private static readonly Deck CustomDeck = BuiltInDecks.Sports.Value;
        private static readonly TimeSpan CustomTimeLimit = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan CustomTurnTimeLimit = TimeSpan.FromSeconds(20);
        private static readonly ReferenceCardGame CustomKindGame = new ReferenceCardGame(CustomtHeight, CustomWidth, CustomDeck,
            CustomTimeLimit, CustomTurnTimeLimit);

        [TestMethod]
        public void Build_WithDefaultValues_IsCorrect()
        {
            GameBuilder builder = new GameBuilder(DefaultHeight, DefaultWidth);
            IGame game = builder.Build();

            Assert.AreEqual(DefaultHeight, builder.Height, 
                $"La altura del tablero no es correcta: se esperaba {DefaultHeight}, pero se ha encontrado {builder.Height}.");
            Assert.AreEqual(DefaultWidth, builder.Width,
                $"La anchura del tablero no es correcta: se esperaba {DefaultWidth}, pero se ha encontrado {builder.Width}.");

            Assert.AreEqual(DefaultDeck, game.Deck,
                "Se esperaba la baraja de animales.");
            Assert.AreEqual(DefaultTimeLimit, game.GameClock.TimeLimit,
                "El límite de tiempo de juego no es el esperado.");
            Assert.AreEqual(DefaultTurnTimeLimit, game.TurnClock.TimeLimit,
                "El límite de tiempo de turno no es el esperado.");
            Assert.AreEqual(DefaultLevel, game.LevelNumber,
                $"El número de nivel no es correcto: se esperaba {DefaultLevel}, pero se ha encontrado {game.LevelNumber}.");
            Assert.AreEqual(DefaultKindGame.GetType(), game.GetType(),
                $"El tipo de partida que se esperaba era {DefaultKindGame.GetType()}, pero el juego es de tipo {game.GetType()}.");
        }

        [TestMethod]
        public void Build_WithCustomValues_IsCorrect()
        {
            GameBuilder builder = new GameBuilder(CustomtHeight, CustomWidth);
            IGame game = builder.WithDeck(CustomDeck).WithTimeLimit(CustomTimeLimit).
                WithTurnTimeLimit(CustomTurnTimeLimit).OfKind(GameBuilder.GameKind.ReferenceCard).Build();

            Assert.AreEqual(CustomtHeight, builder.Height,
                $"La altura del tablero no es correcta: se esperaba {CustomtHeight}, pero se ha encontrado {builder.Height}.");
            Assert.AreEqual(CustomWidth, builder.Width,
                $"La anchura del tablero no es correcta: se esperaba {CustomWidth}, pero se ha encontrado {builder.Width}.");

            Assert.AreEqual(CustomDeck, game.Deck,
                "Se esperaba la baraja de animales.");
            Assert.AreEqual(CustomTimeLimit, game.GameClock.TimeLimit,
                "El límite de tiempo de juego no es el esperado.");
            Assert.AreEqual(CustomTurnTimeLimit, game.TurnClock.TimeLimit,
                "El límite de tiempo de turno no es el esperado.");
            Assert.AreEqual(CustomKindGame.GetType(), game.GetType(),
                $"El tipo de partida que se esperaba era {CustomKindGame.GetType()}, pero el juego es de tipo {game.GetType()}.");
        }
    }
}
