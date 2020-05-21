using System;
using System.Collections.Generic;
using Twins.Models.Properties;

namespace Twins.Models.Game
{
    public class LocalCompetitiveGame : IMultiplayerGame
    {
        public Deck Deck {
            get => inner.Deck;
        }

        public Observable<int> RemainingMatches {
            get => inner.RemainingMatches;
        }
        public Observable<int> MatchSuccesses {
            get => players[currentPlayer].MatchSucceses;
        }
        public Observable<int> MatchFailures {
            get => players[currentPlayer].MatchFailures;
        }

        public bool IsFinished {
            get => inner.IsFinished;
        }
        public Observable<int> Turn {
            get => inner.Turn;
        }

        public Clock GameClock {
            get => inner.GameClock;
        }
        public Clock TurnClock {
            get => inner.TurnClock;
        }

        // In this competitive game mode only player-wise scores are considered.
        // This field for a general score is always zero.
        public Score Score { get; } = new Score(0);
        public Board Board {
            get => inner.Board;
        }

        public int LevelNumber {
            get => inner.LevelNumber;
        }

        public IList<Player> Players {
            get => players;
        }

        public bool IsMultiplayer {
            get => true;
        }

        public Player CurrentPlayer {
            get => players[currentPlayer];
        }

        public event Action TurnTimedOut {
            add { inner.TurnTimedOut += value; }
            remove { inner.TurnTimedOut -= value; }
        }

        public event Action<GameResult> GameEnded {
            add { inner.GameEnded += value; }
            remove { inner.GameEnded -= value; }
        }

        public event Action<Player> PlayerChanged;
        public event Action<bool> AttemptedMatch;

        readonly IGame inner;
        readonly Player[] players;
        int currentPlayer;

        public LocalCompetitiveGame(IGame inner, params Player[] players)
        {
            this.inner = inner;
            this.players = players;

            inner.AttemptedMatch += success =>
            {
                if (success)
                {
                    players[currentPlayer].MatchSucceses.Value++;
                    AttemptedMatch?.Invoke(true);
                }
                else
                {
                    players[currentPlayer].MatchFailures.Value++;
                    AttemptedMatch?.Invoke(false);
                }
            };

            currentPlayer = 0;

            inner.Board.Game = this;
        }

        public void Resume() => inner.Resume();
        public void Pause() => inner.Pause();

        public bool ShouldTryMatch() => inner.ShouldTryMatch();
        public IEnumerable<Board.Cell> TryMatch() => inner.TryMatch();

        public bool ShouldEndTurn() => inner.ShouldEndTurn();

        public void EndTurn()
        {
            inner.EndTurn();
            NextPlayer();
        }

        void NextPlayer()
        {
            currentPlayer = (currentPlayer + 1) % players.Length;
            PlayerChanged(players[currentPlayer]);
        }
    }
}
