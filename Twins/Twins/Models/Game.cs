using System.Collections.Generic;
using Twins.Logic;
using Twins.Model;

namespace Twins.Models
{
    public abstract partial class Game
    {
        public TurnProperty Turn { get; set; } = new TurnProperty();
        public int MatchSuccesses { get; protected set; } = 0;
        public int MatchFailures { get; protected set; } = 0;
        public int MatchAttempts { get => MatchSuccesses + MatchFailures; }

        public Clock GameClock { get; protected set; }
        public Clock TurnClock { get; protected set; }

        public Board Board { get; protected set; }

        public abstract void Resume();

        public abstract void Pause();

        public abstract void Win();

        public abstract bool ShouldTryMatch();

        public abstract IEnumerable<Board.Cell> TryMatch();

        public abstract bool ShouldEndTurn();

        public abstract void EndTurn();
    }
}
