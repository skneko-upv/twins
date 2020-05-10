using Twins.Models.Builders;

namespace Twins.Models
{
    public abstract class Levels
    {
        public static GameBuilder level1 = new GameBuilder(3, 2).OfKind(GameBuilder.GameKind.Standard).
            WithTimeLimit(System.TimeSpan.Parse("0:02:0")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:10"));
    }
}
