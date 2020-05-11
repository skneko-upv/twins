using Twins.Components;
using Twins.Models.Builders;

namespace Twins.Models
{
    public abstract class Levels
    {
        public static GameBuilder level1 = new GameBuilder(3, 2).OfKind(GameBuilder.GameKind.Standard).
            WithTimeLimit(System.TimeSpan.Parse("0:02:0")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:10")).WithDeck(BasicDeck.Numeros);
        public static GameBuilder level2 = new GameBuilder(4, 2).OfKind(GameBuilder.GameKind.Standard).
            WithTimeLimit(System.TimeSpan.Parse("0:02:0")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:10")).WithDeck(BasicDeck.Numeros);
        public static GameBuilder level3 = new GameBuilder(4, 3).OfKind(GameBuilder.GameKind.Standard).
            WithTimeLimit(System.TimeSpan.Parse("0:02:0")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:10")).WithDeck(BasicDeck.Numeros);
        public static GameBuilder level4 = new GameBuilder(4, 4).OfKind(GameBuilder.GameKind.Standard).
            WithTimeLimit(System.TimeSpan.Parse("0:02:0")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:10")).WithDeck(BasicDeck.Deportes);
        public static GameBuilder level5 = new GameBuilder(5, 4).OfKind(GameBuilder.GameKind.Standard).
            WithTimeLimit(System.TimeSpan.Parse("0:02:0")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:10")).WithDeck(BasicDeck.Deportes);
        public static GameBuilder level6 = new GameBuilder(6, 4).OfKind(GameBuilder.GameKind.Standard).
            WithTimeLimit(System.TimeSpan.Parse("0:02:0")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:10")).WithDeck(BasicDeck.Deportes);
        public static GameBuilder level7 = new GameBuilder(6, 4).OfKind(GameBuilder.GameKind.Standard).
            WithTimeLimit(System.TimeSpan.Parse("0:01:30")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:10")).WithDeck(BasicDeck.Animales);
        public static GameBuilder level8 = new GameBuilder(6, 4).OfKind(GameBuilder.GameKind.ReferenceCard).
            WithTimeLimit(System.TimeSpan.Parse("0:01:30")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:10")).WithDeck(BasicDeck.Animales);
        public static GameBuilder level9 = new GameBuilder(6, 4).OfKind(GameBuilder.GameKind.ReferenceCard).
            WithTimeLimit(System.TimeSpan.Parse("0:01:30")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:05")).WithDeck(BasicDeck.Animales);
        public static GameBuilder level10 = new GameBuilder(6, 4).OfKind(GameBuilder.GameKind.ReferenceCard).
            WithTimeLimit(System.TimeSpan.Parse("0:01:00")).WithTurnTimeLimit(System.TimeSpan.Parse("0:0:05")).WithDeck(BasicDeck.Animales);
    }
}
