using SQLite;
using SQLiteNetExtensions.Attributes;
using Twins.Persistence.DataTypes;

namespace Twins.Models
{
    public class PlayerInfo
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int LastLevelPassed { get; set; }
        [OneToOne]
        public PlayerPreferences PlayerOptions { get; set; }

        public PlayerInfo() { LastLevelPassed = 0; ID = 1; }
    }
}
