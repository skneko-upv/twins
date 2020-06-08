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
        }
    }
}
