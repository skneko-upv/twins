using System;
using System.Collections.Generic;
using System.Linq;
using Twins.Models.Properties;
using Twins.Models.Strategies;

namespace Twins.Models
{
    public abstract class Game
    {
        private const int GroupSize = 2;
        
        public Deck Deck { get; }

        public int RemainingMatches { get; private set; }

        public bool IsFinished { get; protected set; } = false;
        public Observable<int> Turn { get; protected set; } = new Observable<int>(1);

        public Observable<int> MatchSuccesses { get; protected set; } = new Observable<int>(0);
        public int MatchFailures { get; protected set; } = 0;
        public int MatchAttempts => MatchSuccesses.Value + MatchFailures;

        public Clock GameClock { get; protected set; }
        public Clock TurnClock { get; protected set; }

        public Score Score { get; protected set; }

        public Board Board { get; protected set; }
        public ResultOfGame ResultOfGame { get; set; }
        public int LevelNumber { get;  set; }

        public event Action TurnTimedOut;
        public event Action<bool> GameEnded;

        public Game(int height, int width, Deck deck, TimeSpan? timeLimit, TimeSpan? turnLimit, Board.Cell[,] cells = null, int levelNumber = 0)
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

            if (cells != null)
            {
                Board = new Board(height, width, this, cells);
            }
            else
            {
                var populationStrategy = new CyclicRandomPopulationStrategy(GroupSize, deck);
                Board = new Board(height, width, this, populationStrategy);
            }

            Deck = deck;

            RemainingMatches = height * width / GroupSize;

            Score = new Score();
            TurnTimedOut += () => { Score.DecrementTimedOut(); };
            LevelNumber = levelNumber;
            ResultOfGame = new ResultOfGame(0);
        }

        public abstract IEnumerable<Board.Cell> TryMatch();

        public virtual bool ShouldTryMatch()
        {
            return Board.FlippedCells.Count > GroupSize - 1;
        }

        public virtual bool ShouldEndTurn()
        {
            return true;
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
            ResultOfGame.SetVictory(victory).setMatchSyccesses(MatchSuccesses.Value).setLevelNumber(LevelNumber);
            GameEnded(victory);
        }

        public virtual void EndTurn()
        {
            TurnClock.Reset();
            Board.UnflipAllCells();
            Turn.Value++;
        }

        protected virtual IEnumerable<Board.Cell> HandleMatchResult(bool isMatch)
        {
            IEnumerable<Board.Cell> matched;
            if (isMatch)
            {
                MatchSuccesses.Value++;

                foreach (Board.Cell cell in Board.FlippedCells)
                {
                    Board.SetCellKeepRevealed(cell.Row, cell.Column, true);
                }

                Score.IncrementMatchSuccess();
                RemainingMatches--;
                matched = new List<Board.Cell>(Board.FlippedCells);

                if (RemainingMatches <= 0)
                {
                    EndGame(true);
                }
            }
            else
            {
                Score.DecrementMatchFail(Board.FlippedCells.Select(cell => cell.FlipCount).ToArray());
                MatchFailures++;
                matched = Enumerable.Empty<Board.Cell>();
            }

            return matched;
        }
    }
}
