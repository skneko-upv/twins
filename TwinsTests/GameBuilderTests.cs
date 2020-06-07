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
        private const int ExpectedHeight = 4;
        private const int ExpectedWidth = 6;
        private const int ExpectedLevel = 0;

        private static readonly Deck ExpectedDeck = BuiltInDecks.Animals.Value;
        private static readonly Clock ExpectedTimeLimitClock = new Clock(TimeSpan.FromMinutes(1));
        private static readonly Clock ExpectedTurnTimeLimitClock = new Clock(TimeSpan.FromSeconds(5));

        [TestMethod]
        public void DefaultGameCreatedCorrectly()
        {
            GameBuilder builder = new GameBuilder(ExpectedHeight, ExpectedWidth);
            IGame defaultGame = builder.Build();

            Assert.AreEqual(ExpectedHeight, builder.Height, "La altura del tablero no es correcta, se esperaba " + ExpectedHeight +
                ", pero el generado es " + builder.Height);
            Assert.AreEqual(ExpectedWidth, builder.Width, "La anchura del tablero no es correcta, se esperaba " + ExpectedWidth + 
                ", pero el generado es " + builder.Width);

            Assert.AreEqual(ExpectedDeck, defaultGame.Deck, "La baraja del tablero no es laa que se esperaba," +
                " se esperaba el deck de Animales");
            Assert.AreEqual(ExpectedTimeLimitClock, defaultGame.GameClock, "El reloj de juego no es el esperado");
            Assert.AreEqual(ExpectedTurnTimeLimitClock, defaultGame.TurnClock, "El reloj de turno no es el esperado");
            Assert.AreEqual(ExpectedLevel, defaultGame.LevelNumber, "El nivel esperado era " + ExpectedLevel +
                ", pero el generado es " + defaultGame.LevelNumber);
        }
    }
}
