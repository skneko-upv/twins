using Twins.Models.Properties;

namespace Twins.Models
{
    public class Player
    {
        public string Name { get; }

        public Score Score { get; }

        public Observable<int> MatchSucceses { get; set; } = new Observable<int>(0);

        public Observable<int> MatchFailures { get; set; } = new Observable<int>(0);

        public Player(string name, Score score = null)
        {
            Name = name;
            if (score != null)
            {
                Score = score;
            }
            else
            {
                Score = new Score();
            }
        }
    }
}
