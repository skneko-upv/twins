using SQLite;

namespace Twins.Models
{
    public class PlayerInfo
    {
        [PrimaryKey]
        public int ID { get; set; }
        public int LastLevelPassed { get; set; }

        public PlayerInfo() { LastLevelPassed = 0; ID = 1; }
    }
}
