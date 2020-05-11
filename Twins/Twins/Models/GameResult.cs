using System;

namespace Twins.Models
{
    public class GameResult
    {
        public bool IsVictory { get; }
        public int MatchSuccesses { get; }
        public int MatchFailures { get; }
        public int MatchAttempts => MatchSuccesses + MatchFailures;
        public int Score { get; }
        public TimeSpan Time { get; }

        public int LevelNumber { get; }

        public GameResult(int levelNumber) { IsVictory = false; LevelNumber = levelNumber; }

        public GameResult(bool isVictory, int matchSuccesses, int matchFailures, int levelNumber, int score, TimeSpan time)
        {
            IsVictory = isVictory;
            MatchSuccesses = matchSuccesses;
            MatchFailures = matchFailures;
            LevelNumber = levelNumber;
            Score = score;
        }
    }
}