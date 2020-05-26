using System;
using System.Collections.Generic;
using System.Linq;
using Twins.Models.Properties;

namespace Twins.Models.Game
{
    public class LocalCompetitiveGame : IMultiplayerGame
    {
        public Deck Deck => inner.Deck;

        public Observable<int> RemainingMatches => inner.RemainingMatches;
        public Observable<int> MatchSuccesses => players[currentPlayer].MatchSucceses;
        public Observable<int> MatchFailures => players[currentPlayer].MatchFailures;

        public bool IsFinished => inner.IsFinished;
        public Observable<int> Turn => inner.Turn;

        public Clock GameClock => inner.GameClock;
        public Clock TurnClock => inner.TurnClock;

        // In this competitive game mode only player-wise scores are considered.
        // This hidden field will aggregate deltas from all players.
        public Score Score { get; } = new Score(0);
        public Board Board => inner.Board;

        public int LevelNumber => inner.LevelNumber;

        public IList<Player> Players => players;

        public bool IsMultiplayer => true;

        public Player CurrentPlayer => players[currentPlayer];

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

        private readonly IGame inner;
        private readonly Player[] players;
        private int currentPlayer;

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
                int delta = @new - old;
                players[currentPlayer].Score.Value += delta;
                Score.Value += delta;
            };

            currentPlayer = 0;

            inner.Board.Game = this;
        }

        public void Resume()
        {
            inner.Resume();
        }

        public void Pause()
        {
            inner.Pause();
        }

        public bool ShouldTryMatch()
        {
            return inner.ShouldTryMatch();
        }

        public IEnumerable<Board.Cell> TryMatch()
        {
            return inner.TryMatch();
        }

        public bool ShouldEndTurn()
        {
            return inner.ShouldEndTurn();
        }

        public void EndTurn()
        {
            inner.EndTurn();
            NextPlayer();
        }

        public Player DetermineWinner(out bool conclusive)
        {
            if (IsFinished)
            {
                Player winner = players.Aggregate((p1, p2) =>
                    p1.Score.CompareTo(p2.Score) > 0 ? p1 : p2);    // select player with the higher score

                if (players.Any(p => p != winner && p.Score.Value == winner.Score.Value))
                {
                    conclusive = false;    // draw
                }
                else
                {
                    conclusive = true;
                }
                return winner;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void NextPlayer()
        {
            currentPlayer = (currentPlayer + 1) % players.Length;
            PlayerChanged(players[currentPlayer]);
        }
    }
}
