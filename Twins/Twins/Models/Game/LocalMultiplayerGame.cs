using System;
using System.Collections.Generic;
using System.Text;
using Twins.Models.Properties;

namespace Twins.Models.Game
{
    public class LocalMultiplayerGame : IMultiplayerGame
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

        public Score Score {
            get => players[currentPlayer].Score;
        }
        public Board Board {
            get => inner.Board;
        }

        public int LevelNumber {
            get => inner.LevelNumber;
        }

        public ICollection<Player> Players {
            get => players;
        }

        public bool IsMultiplayer {
            get => true;
        }

        public event Action TurnTimedOut;
        public event Action<GameResult> GameEnded;
        public event Action<Player> PlayerChanged;
        public event Action<bool> AttemptedMatch;

        readonly IGame inner;
        readonly Player[] players;
        int currentPlayer;

        public LocalMultiplayerGame(IGame inner, params Player[] players)
        {
            this.inner = inner;
            this.players = players;

            inner.TurnTimedOut += TurnTimedOut;
            inner.GameEnded += GameEnded;
            inner.Score.Changed += delta =>
            {
                players[currentPlayer].Score.Value += delta;
            };
            inner.AttemptedMatch += success =>
            {
                if (success)
                {
                    players[currentPlayer].MatchSucceses.Value++;
                    AttemptedMatch(true);
                }
                else
                {
                    players[currentPlayer].MatchFailures.Value++;
                    AttemptedMatch(false);
                }
            };

            currentPlayer = 0;
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
