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
            get => /* TODO */;
        }
        public Observable<int> MatchFailures {
            get => /* TODO */;
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
            get => /* TODO */;
        }
        public Board Board {
            get => inner.Board;
        }

        public int LevelNumber {
            get => inner.LevelNumber;
        }

        public int PlayerCount { get; }

        public event Action TurnTimedOut;
        public event Action<GameResult> GameEnded;

        readonly IGame inner;
        int currentPlayer;

        public LocalMultiplayerGame(IGame inner, int playerCount)
        {
            this.inner = inner;
            inner.TurnTimedOut += TurnTimedOut;
            inner.GameEnded += GameEnded;

            PlayerCount = playerCount;
        }

        public void Resume() => inner.Resume();
        public void Pause() => inner.Pause();

        public bool ShouldTryMatch() => inner.ShouldTryMatch();
        public IEnumerable<Board.Cell> TryMatch() => inner.TryMatch();

        public bool ShouldEndTurn()
        {
            /* TODO */
        }

        public void EndTurn()
        {
            /* TODO */
        }
    }
}
