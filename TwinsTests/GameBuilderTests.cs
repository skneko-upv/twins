using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Twins.Components;
using Twins.Models;
using Twins.Models.Builders;
using Twins.Models.Game;

namespace Twins.Tests
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
        private static readonly StandardGame DefaultGame = new StandardGame(DefaultHeight, DefaultWidth, DefaultDeck,
            DefaultTimeLimit, DefaultTurnTimeLimit);

        private const int CustomtHeight =4;
        private const int CustomWidth = 4;
        private static readonly Deck CustomDeck = BuiltInDecks.Sports.Value;
        private static readonly TimeSpan CustomTimeLimit = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan CustomTurnTimeLimit = TimeSpan.FromSeconds(20);
        private static readonly ReferenceCardGame CustomKindGame = new ReferenceCardGame(CustomtHeight, CustomWidth, CustomDeck, CustomTimeLimit, CustomTurnTimeLimit, null, 0);

        private static readonly Player ExpectedPlayerOne = new Player("P1");
        private static readonly Player ExpectedPlayerTwo = new Player("P2");
        private static readonly IList<Player> ExpectedPlayersForMultiplayer = new List<Player>() { ExpectedPlayerOne, ExpectedPlayerTwo};
        private const bool ExpectedMultiplayer = true;
        private static readonly LocalCompetitiveGame ExpectedMultiplayerDefaultGame = new LocalCompetitiveGame(DefaultGame, ExpectedPlayersForMultiplayer.ToArray());
        private static readonly LocalCompetitiveGame ExpectedMultiplayerCustomGame = new LocalCompetitiveGame(CustomKindGame, ExpectedPlayersForMultiplayer.ToArray());

        [TestMethod]
        public void Build_WithDefaultValues_IsCorrect()
        {
            const int height = 4;
            const int width = 6;
            const int defaultLevel = 0;

            Deck defaultDeck = BuiltInDecks.Animals.Value;
            TimeSpan defaultTimeLimit = TimeSpan.FromMinutes(1);
            TimeSpan defaultTurnTimeLimit = TimeSpan.FromSeconds(5);

            GameBuilder builder = new GameBuilder(height, width);
            IGame game = builder.Build();

            Assert.AreEqual(height, builder.Height,
                $"La altura del tablero no es correcta: se esperaba {height}, pero se ha encontrado {builder.Height}.");
            Assert.AreEqual(width, builder.Width,
                $"La anchura del tablero no es correcta: se esperaba {width}, pero se ha encontrado {builder.Width}.");

            Assert.AreEqual(defaultDeck, game.Deck,
                "Se esperaba la baraja de animales.");
            Assert.AreEqual(defaultTimeLimit, game.GameClock.TimeLimit,
                "El límite de tiempo de juego no es el esperado.");
            Assert.AreEqual(defaultTurnTimeLimit, game.TurnClock.TimeLimit,
                "El límite de tiempo de turno no es el esperado.");
            Assert.AreEqual(defaultLevel, game.LevelNumber,
                $"El número de nivel no es correcto: se esperaba {defaultLevel}, pero se ha encontrado {game.LevelNumber}.");
            Assert.IsInstanceOfType(game, typeof(StandardGame),
                $"El tipo de partida que se esperaba era {nameof(StandardGame)}, pero el juego es de tipo {game.GetType().Name}.");
        }

        [TestMethod]
        public void Build_WithSeveralPlayers_IsMultiplayer()
        {
            const string namePlayer1 = "Alice";
            const string namePlayer2 = "Bob";

            IGame game = new GameBuilder(2, 2)
                .WithPlayer(new Player(namePlayer1))
                .WithPlayer(new Player(namePlayer2))
                .Build();

            Assert.IsInstanceOfType(game, typeof(IMultiplayerGame),
                "El juego no es multijugador: no implementa IMultiplayerGame");
            Assert.IsTrue(game.IsMultiplayer,
                "El juego no es multijugador: no ha establecido IsMultiplayer.");

            IMultiplayerGame multiGame = game as IMultiplayerGame;
            Assert.AreEqual(2, multiGame.Players.Count,
                "Los jugadores no se han establecido correctamente.");
            Assert.AreEqual(namePlayer1, multiGame.Players[0].Name);
            Assert.AreEqual(namePlayer2, multiGame.Players[1].Name);
        }

        [TestMethod]
        public void Build_WithOnePlayer_IsSingleplayer()
        {
            IGame game = new GameBuilder(2, 2)
                .WithPlayer(new Player("Mark"))
                .Build();

            Assert.IsFalse(game.IsMultiplayer,
                "El juego es multijugador, se esperaba configuración para un jugador.");
        }

        [TestMethod]
        public void Build_WithCustomValues_IsCorrect()
        {
            const int height = 4;
            const int width = 4;
            Deck customDeck = BuiltInDecks.Sports.Value;
            TimeSpan customTimeLimit = TimeSpan.FromMinutes(5);
            TimeSpan customTurnTimeLimit = TimeSpan.FromSeconds(20);

            GameBuilder builder = new GameBuilder(height, width);
            IGame game = builder.WithDeck(customDeck).WithTimeLimit(customTimeLimit).
                WithTurnTimeLimit(customTurnTimeLimit).OfKind(GameBuilder.GameKind.ReferenceCard).Build();

            Assert.AreEqual(height, builder.Height,
                $"La altura del tablero no es correcta: se esperaba {height}, pero se ha encontrado {builder.Height}.");
            Assert.AreEqual(width, builder.Width,
                $"La anchura del tablero no es correcta: se esperaba {width}, pero se ha encontrado {builder.Width}.");

            Assert.AreEqual(customDeck, game.Deck,
                "Se esperaba la baraja de animales.");
            Assert.AreEqual(customTimeLimit, game.GameClock.TimeLimit,
                "El límite de tiempo de juego no es el esperado.");
            Assert.AreEqual(customTurnTimeLimit, game.TurnClock.TimeLimit,
                "El límite de tiempo de turno no es el esperado.");
            Assert.IsInstanceOfType(game, typeof(ReferenceCardGame),
                $"El tipo de partida que se esperaba era {nameof(ReferenceCardGame)}, pero el juego es de tipo {game.GetType().Name}.");
        }

        [TestMethod]
        public void Build_WithInvalidSize_ThrowsException()
        {
            var builder = new GameBuilder(5, 3)
                .WithGroupSize(2);

            Assert.ThrowsException<ArgumentException>(builder.Build);
        }

        [TestMethod]
        public void Build_MultiplayerWithDefaultValues_IsCorrect()
        {
            GameBuilder builder = new GameBuilder(DefaultHeight, DefaultWidth);
            IGame game = builder.WithPlayer(ExpectedPlayerOne).WithPlayer(ExpectedPlayerTwo).Build();

            Assert.AreEqual(DefaultHeight, builder.Height,
                $"La altura del tablero no es correcta: se esperaba {DefaultHeight}, pero se ha encontrado {builder.Height}.");
            Assert.AreEqual(DefaultWidth, builder.Width,
                $"La anchura del tablero no es correcta: se esperaba {DefaultWidth}, pero se ha encontrado {builder.Width}.");
            Assert.AreEqual(DefaultDeck, game.Deck,
                "Se esperaba la baraja de animales.");
            Assert.AreEqual(ExpectedMultiplayer, game.IsMultiplayer, "Se esperaba que el juego fuera Multijugador, pero no lo es");
            Assert.AreEqual(DefaultTimeLimit, game.GameClock.TimeLimit,
                "El límite de tiempo de juego no es el esperado.");
            Assert.AreEqual(DefaultTurnTimeLimit, game.TurnClock.TimeLimit,
                "El límite de tiempo de turno no es el esperado.");
            Assert.AreEqual(DefaultLevel, game.LevelNumber,
                $"El número de nivel no es correcto: se esperaba {DefaultLevel}, pero se ha encontrado {game.LevelNumber}.");
            Assert.AreEqual(ExpectedMultiplayerDefaultGame.GetType(), game.GetType(),
                $"El tipo de partida que se esperaba era {ExpectedMultiplayerDefaultGame.GetType()}, pero el juego es de tipo {game.GetType()}.");
        }

        [TestMethod]
        public void Build_MultiplayerWithCustomValues_IsCorrect()
        {
            GameBuilder builder = new GameBuilder(CustomtHeight, CustomWidth);
            IGame game = builder.WithDeck(CustomDeck).WithTimeLimit(CustomTimeLimit).
                WithTurnTimeLimit(CustomTurnTimeLimit).OfKind(GameBuilder.GameKind.ReferenceCard).
                WithPlayer(ExpectedPlayerOne).WithPlayer(ExpectedPlayerTwo).Build();

            Assert.AreEqual(CustomtHeight, builder.Height,
                $"La altura del tablero no es correcta: se esperaba {CustomtHeight}, pero se ha encontrado {builder.Height}.");
            Assert.AreEqual(CustomWidth, builder.Width,
                $"La anchura del tablero no es correcta: se esperaba {CustomWidth}, pero se ha encontrado {builder.Width}.");
            Assert.AreEqual(ExpectedMultiplayer, game.IsMultiplayer, "Se esperaba que el juego fuera Multijugador, pero no lo es");
            Assert.AreEqual(CustomDeck, game.Deck,
                "Se esperaba la baraja de animales.");
            Assert.AreEqual(CustomTimeLimit, game.GameClock.TimeLimit,
                "El límite de tiempo de juego no es el esperado.");
            Assert.AreEqual(CustomTurnTimeLimit, game.TurnClock.TimeLimit,
                "El límite de tiempo de turno no es el esperado.");
            Assert.AreEqual(ExpectedMultiplayerCustomGame.GetType(), game.GetType(),
                $"El tipo de partida que se esperaba era {ExpectedMultiplayerCustomGame.GetType()}, pero el juego es de tipo {game.GetType()}.");
        }
    }
}
