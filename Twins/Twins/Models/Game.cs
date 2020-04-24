namespace Twins.Models
{
    public abstract partial class Game
    {
        public int Turn { get; protected set; } = 1;
        public int MatchSuccesses { get; protected set; } = 0;
        public int MatchFailures { get; protected set; } = 0;
        public int MatchAttempts { get => MatchSuccesses + MatchFailures; }

        public Board Board { get; protected set; }
    }
}
