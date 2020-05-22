using System;
using System.Collections.Generic;
using System.Linq;
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
        // This hidden field will aggregate deltas from all players.
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
            inner.Score.Changed += (old, @new) =>
            {
                var delta = @new - old;
                players[currentPlayer].Score.Value += delta;
                Score.Value += delta;
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

        public Player DetermineWinner()
        {
            if (IsFinished)
            {
                return players.Aggregate((p1, p2) =>
                    p1.Score.CompareTo(p2.Score) > 0 ? p1 : p2);    // select player with the higher score
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        void NextPlayer()
        {
            currentPlayer = (currentPlayer + 1) % players.Length;
            PlayerChanged(players[currentPlayer]);
        }
    }
}
