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
                int previous = this.value;
                this.value = value;
                Changed?.Invoke(previous, value);
            }
        }
        private int value;

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
            int delta = MatchFailDeltaBase + flipCounts.Select(c => Math.Max(c - 1, 0) * MatchFailDeltaPerCell).Sum();
            Value -= Math.Min(delta, 10);
        }

        public void DecrementTimedOut()
        {
            Value -= TimedOutDelta;
        }

        public int CompareTo(Score other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            return value - other.value;
        }

        public override bool Equals(object other)
        {
            if (other is Score)
            {
                return this.Value == (other as Score).Value;
            } 
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Value;
        }

        public static bool operator ==(Score left, Score right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Score left, Score right)
        {
            return !(left == right);
        }

        public static bool operator <(Score left, Score right)
        {
            return left is null ? right is object : left.CompareTo(right) < 0;
        }

        public static bool operator <=(Score left, Score right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        public static bool operator >(Score left, Score right)
        {
            return left is object && left.CompareTo(right) > 0;
        }

        public static bool operator >=(Score left, Score right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }
    }
}
