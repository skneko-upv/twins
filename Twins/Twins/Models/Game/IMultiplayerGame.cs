using System;
using System.Collections.Generic;
using System.Text;

namespace Twins.Models.Game
{
    interface IMultiplayerGame : IGame
    {
        ICollection<Player> Players { get; }

        event Action<Player> PlayerChanged;
    }
}
