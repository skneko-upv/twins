using System;

namespace Twins.Models
{
    public class ResultOfGame
    {
        public bool IsVictory { get; set; }
        public int MatchSuccesses { get; set; }
        public int MatchFailures { get; protected set; }
        public int MatchAttempts => MatchSuccesses + MatchFailures;
        public int LevelNumber { get; set; }
        public ResultOfGame(int levelNumber) { IsVictory = false; LevelNumber = levelNumber; }

        public ResultOfGame(bool isVictory, int matchSuccesses, int matchFailures, int levelNumber)
        {
            IsVictory = isVictory;
            MatchSuccesses = matchSuccesses;
            MatchFailures = matchFailures;
            LevelNumber = levelNumber;
        }

        internal ResultOfGame SetVictory(bool victory)
        {
            IsVictory = victory;
            return this;
        }

        internal ResultOfGame setMatchSyccesses(int value)
        {
            MatchSuccesses = value;
            return this;
        }

        internal ResultOfGame setLevelNumber(int levelNumber)
        {
            LevelNumber = levelNumber;
            return this;
        }
    }
}