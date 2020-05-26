using System;
using System.Collections.Generic;

namespace Twins.Models.Game
{
    internal interface IMultiplayerGame : IGame
    {
        IList<Player> Players { get; }

        Player CurrentPlayer { get; }

        event Action<Player> PlayerChanged;

        Player DetermineWinner(out bool conclusive);
    }
}
