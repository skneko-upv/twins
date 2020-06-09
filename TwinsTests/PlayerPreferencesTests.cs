using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Twins.Models;

namespace Twins.Tests
{
    [TestClass]
    public class PlayerPreferencesTests
    {
        private const int ExpectedColumn = 6;
        private const int ExpectedRow = 4;
        private static readonly IList<Deck> ExpectedBuiltInDecks = new List<Deck> { Twins.Components.BuiltInDecks.Animals.Value,
                Twins.Components.BuiltInDecks.Numbers.Value, Twins.Components.BuiltInDecks.Sports.Value };
        private static readonly IList<Deck> ExpectedPlayerDecks = new List<Deck>();
        private static readonly Deck ExpectedSelectedDeck = ExpectedBuiltInDecks[0];
        private const string ExpectedSelectedSong = "Solve The Puzzle";
        private const string ExpectedButtonEffect = "menuButtonSound";
        private const string ExpectedTurnCardEffect = "Voltear Carta";
        private const string ExpectedUnturnCardEffect = "Voltear Carta Invertido";
        private const string ExpectedWinEffect = "Ta Da";
        private const string ExpectedLoseEffect = "Lose Sound";
        private const string ExpectedClockTimerEffect = "clockticksound";
        private const double ExpectedVolume = 100.0;
        private static readonly TimeSpan ExpectedLimitTime = TimeSpan.Parse("0:01:00");
        private static readonly TimeSpan ExpectedTurnTime = TimeSpan.Parse("0:00:05");

        [TestMethod]
        public void PlayerPreferencesDefaultCorrectly()
        {
            Twins.Models.Singletons.PlayerPreferences playerPreferences = Twins.Models.Singletons.PlayerPreferences.Instance;

            Assert.AreEqual(ExpectedColumn, playerPreferences.Column, "Se esperaba como valor de columna " + ExpectedColumn
                + ", pero se obtuvo en su lugar " + playerPreferences.Column);

            Assert.AreEqual(ExpectedRow, playerPreferences.Row, "Se esperaba como valor de filas " + ExpectedRow
                + ", pero se obtuvo en su lugar " + playerPreferences.Row);

            Assert.AreEqual(ExpectedBuiltInDecks.Count, playerPreferences.BuiltInDecks.Count, "La lista de barajas pre-hechas es " +
                "diferente a la esperada");

            Assert.AreEqual(ExpectedPlayerDecks.Count, playerPreferences.PlayerDecks.Count, "La lista de barajas del jugador debería" +
                " de estar vacía e inicializada");

            Assert.AreEqual(ExpectedSelectedDeck, playerPreferences.SelectedDeck, "La baraja seleccionada no es la esperada, " +
                "deberia de ser la primera de la lista de decks");

            Assert.AreEqual(ExpectedSelectedSong, playerPreferences.SelectedSong, "La canción seleccionada debería de ser " +
                ExpectedSelectedSong + ", pero la seleccionada era " + playerPreferences.SelectedSong);

            Assert.AreEqual(ExpectedButtonEffect, playerPreferences.ButtonEffect, "El efecto de boton debería de ser " +
                ExpectedButtonEffect + ", pero la seleccionada era " + playerPreferences.ButtonEffect);

            Assert.AreEqual(ExpectedTurnCardEffect, playerPreferences.TurnCardEffect, "El efecto de giro de carta debería de ser " +
                ExpectedTurnCardEffect + ", pero la seleccionada era " + playerPreferences.TurnCardEffect);

            Assert.AreEqual(ExpectedUnturnCardEffect, playerPreferences.UnturnCardEffect, "El efecto de desgirar una carta debería de ser " +
                ExpectedUnturnCardEffect + ", pero la seleccionada era " + playerPreferences.UnturnCardEffect);

            Assert.AreEqual(ExpectedWinEffect, playerPreferences.WinEffect, "El efecto de ganar debería ser " + ExpectedWinEffect +
                ", pero la seleccionada era " + playerPreferences.WinEffect);

            Assert.AreEqual(ExpectedLoseEffect, playerPreferences.LoseEffect, "El efecto de perder debería ser " + ExpectedLoseEffect +
                ", pero la seleccionada era " + playerPreferences.LoseEffect);

            Assert.AreEqual(ExpectedClockTimerEffect, playerPreferences.ClockTimerEffect, "El efecto del reloj debería ser " + ExpectedClockTimerEffect +
                ", pero la seleccionada era " + playerPreferences.ClockTimerEffect);

            Assert.AreEqual(ExpectedVolume, playerPreferences.Volume, "El volumen debería ser " + ExpectedVolume +
                ", pero la seleccionada era " + playerPreferences.Volume);

            Assert.AreEqual(ExpectedLimitTime, playerPreferences.LimitTime, "El tiempo limite debería ser " + ExpectedLimitTime +
                ", pero la seleccionada era " + playerPreferences.LimitTime);

            Assert.AreEqual(ExpectedTurnTime, playerPreferences.TurnTime, "El tiempo limite por turno debería ser " + ExpectedTurnTime +
                ", pero la seleccionada era " + playerPreferences.TurnTime);
        }
    }
}
