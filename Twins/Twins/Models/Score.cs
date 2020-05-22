using System;
using System.Linq;

namespace Twins.Models
{
    public class Score : IComparable<Score>
    {
        public const int DefaultStartingScore = 0;

        public int MatchSuccessDelta { get; set; } = 10;
        public int MatchFailDeltaBase { get; set; } = 1;
        public int MatchFailDeltaPerCell { get; set; } = 1;
        public int TimedOutDelta { get; set; } = 2;
        public int MatchFailDeltaMax { get; set; } = 10;

        public event Action<int, int> Changed;

        public int Value {
            get => value;
            set {
                var previous = this.value;
                this.value = value;
                Changed?.Invoke(previous, value);
            }
        }
        public int value;

        public Score(int value = DefaultStartingScore)
        {
            Value = value;
        }

        public void IncrementMatchSuccess()
        {
            Value += MatchSuccessDelta;
        }

        public void DecrementMatchFail(params int[] flipCounts)
        {
            var delta = MatchFailDeltaBase + flipCounts.Select(c => Math.Max(c - 1, 0) * MatchFailDeltaPerCell).Sum();
            Value -= Math.Min(delta, 10);
        }

        public void DecrementTimedOut()
        {
            Value -= TimedOutDelta;
        }

        public int CompareTo(Score other)
        {
            return value - other.value;
        }
    }
}
