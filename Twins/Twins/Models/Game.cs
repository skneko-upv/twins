using System;
using System.Collections.Generic;
using Twins.Model;
using Twins.Models.Properties;

namespace Twins.Models
{
    public abstract partial class Game
    {
        public TimeProperty GlobalTime { get; }
        public TimeProperty TurnTime { get; }

        public bool IsFinished { get; protected set; } = false;
        public Observable<int> Turn { get; protected set; } = new Observable<int>(1);
        public Observable<int> MatchSuccesses { get; protected set; } = new Observable<int>(0);
        public int MatchFailures { get; protected set; } = 0;
        public int MatchAttempts => MatchSuccesses.Value + MatchFailures;

        public Clock GameClock { get; protected set; }
        public Clock TurnClock { get; protected set; }

        public Score Score { get; protected set; }

        public Board Board { get; protected set; }

        public event Action TurnTimedOut;
        public event Action<bool> GameEnded;

        public Game(TimeSpan? timeLimit, TimeSpan? turnLimit)
        {
            if (timeLimit != null)
            {
                GameClock = new Clock((TimeSpan)timeLimit);
            }
            else
            {
                GameClock = new Clock();
            }
            GameClock.TimedOut += () => EndGame(false);

            if (turnLimit != null)
            {
                TurnClock = new Clock((TimeSpan)turnLimit);
            }
            else
            {
                TurnClock = new Clock();
            }
            TurnClock.TimedOut += () => TurnTimedOut();

            Score = new Score();
            TurnTimedOut += () => { Score.DecrementTimedOut(); };
        }

        public virtual void Resume()
        {
            GameClock.Start();
            TurnClock.Start();
        }

        public virtual void Pause()
        {
            GameClock.Stop();
            TurnClock.Stop();
        }

        public virtual void EndGame(bool victory)
        {
            Pause();
            IsFinished = true;
            GameEnded(victory);
        }

        public abstract bool ShouldTryMatch();

        public abstract IEnumerable<Board.Cell> TryMatch();

        public abstract bool ShouldEndTurn();

        public virtual void EndTurn()
        {
            TurnClock.Reset();
        }
    }
}
