using System;
using System.Collections.Generic;
using Twins.Models.Properties;

namespace Twins.Models.Game
{
    public interface IGame
    {
        Deck Deck { get; }

        Observable<int> RemainingMatches { get; }
        Observable<int> MatchSuccesses { get; }
        Observable<int> MatchFailures { get; }

        bool IsFinished { get; }
        Observable<int> Turn { get; }

        Clock GameClock { get; }
        Clock TurnClock { get; }

        Score Score { get; }
        Board Board { get; }

        int LevelNumber { get; }

        bool IsMultiplayer { get; }

        event Action TurnTimedOut;
        event Action<GameResult> GameEnded;

        void Resume();
        void Pause();

        bool ShouldTryMatch();
        IEnumerable<Board.Cell> TryMatch();

        bool ShouldEndTurn();
        void EndTurn();

    }
}
