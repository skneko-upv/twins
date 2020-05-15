using System;
using System.Collections.Generic;
using System.Text;

namespace Twins.Models.Game
{
    interface IMultiplayerGame : IGame
    {
        int PlayerCount { get; }
    }
}
