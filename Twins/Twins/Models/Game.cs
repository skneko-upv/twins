using System;
using System.Collections.Generic;
using Twins.Logic;
using Twins.Model;

namespace Twins.Models
{
    public abstract partial class Game
    {
        public TimeProperty GlobalTime { get; set; }
        public TimeProperty TurnTime {get; set;}
        public TurnProperty Turn { get; set; } = new TurnProperty();
        public TurnProperty MatchSuccesses { get; protected set; } = new TurnProperty(1,0);
        public int MatchFailures { get; protected set; } = 0;
        public int MatchAttempts { get => MatchSuccesses.Match + MatchFailures; }

        public Clock GameClock { get; protected set; }
        public Clock TurnClock { get; protected set; }

        public Board Board { get; protected set; }

        public event Action TurnTimedOut;

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
            // TODO
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
