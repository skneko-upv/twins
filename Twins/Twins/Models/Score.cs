using System;
using System.Linq;

namespace Twins.Models
{
    public class Score
    {
        public const int DefaultStartingScore = 0;

        public int MatchSuccessDelta { get; set; } = 10;
        public int MatchFailDeltaBase { get; set; } = 1;
        public int MatchFailDeltaPerCell { get; set; } = 1;
        public int TimedOutDelta { get; set; } = 2;

        public event Action<int> Changed;

        public int PositiveValue 
        {
            get => Math.Max(Value, 0);
        }

        public int Value {
            get => value;
            set {
                this.value = value;
                Changed?.Invoke(value);
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
            Value -= delta;
        }

        public void DecrementTimedOut()
        {
            Value -= TimedOutDelta;
        }
    }
}
