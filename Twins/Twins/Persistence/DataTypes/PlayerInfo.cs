using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Twins.Persistence.DataTypes
{
    public class PlayerInfo
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int LastLevelPassed { get; set; }
    }
}
