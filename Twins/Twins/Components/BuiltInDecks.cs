using System;
using System.Collections.Generic;
using Twins.Models;
using Xamarin.Forms;

namespace Twins.Components
{
    public static class BuiltInDecks
    {
        public static readonly Lazy<Deck> Animals = new Lazy<Deck>(CreateAnimalsDeck);
        public static readonly Lazy<Deck> Numbers = new Lazy<Deck>(CreateNumbersDeck);
        public static readonly Lazy<Deck> Sports = new Lazy<Deck>(CreateSportsDeck);

        private static Deck CreateAnimalsDeck()
        {
            var mammals = new Category(0, "Mamíferos");
            var birds = new Category(1, "Aves");
            var others = new Category(2, "Otros");

            var animalsCategories = new Dictionary<int, ISet<Category>>
            {
                [0] = new HashSet<Category> { mammals },
                [1] = new HashSet<Category> { mammals },
                [2] = new HashSet<Category> { mammals },
                [3] = new HashSet<Category> { mammals },
                [4] = new HashSet<Category> { birds },
                [5] = new HashSet<Category> { birds },
                [6] = new HashSet<Category> { mammals },
                [7] = new HashSet<Category> { mammals },
                [8] = new HashSet<Category> { birds },
                [9] = new HashSet<Category> { mammals },
                [10] = new HashSet<Category> { others },
                [11] = new HashSet<Category> { mammals },
            };

            return new Deck("Animales", ImageSource.FromFile("Assets/Decks/Deck1/backimage.png"), LoadImages("Assets/Decks/Deck1/"), animalsCategories);
        }
        
        private static Deck CreateNumbersDeck()
        {
            var numbers = new Category(0, "Números");
            var operators = new Category(1, "Operadores");

            var numbersCategories = new Dictionary<int, ISet<Category>>
            {
                [0] = new HashSet<Category> { numbers },
                [1] = new HashSet<Category> { numbers },
                [2] = new HashSet<Category> { numbers },
                [3] = new HashSet<Category> { numbers },
                [4] = new HashSet<Category> { numbers },
                [5] = new HashSet<Category> { numbers },
                [6] = new HashSet<Category> { numbers },
                [7] = new HashSet<Category> { numbers },
                [8] = new HashSet<Category> { numbers },
                [9] = new HashSet<Category> { numbers },
                [10] = new HashSet<Category> { operators },
                [11] = new HashSet<Category> { operators },
            };

            return new Deck("Números", ImageSource.FromFile("Assets/Decks/Deck2/backimage.png"), LoadImages("Assets/Decks/Deck2/"), numbersCategories);
        }

        private static Deck CreateSportsDeck()
        {
            var indoors = new Category(0, "De salón");
            var outdoors = new Category(1, "De exterior");
            var table = new Category(2, "De mesa");

            var sportsCategories = new Dictionary<int, ISet<Category>>
            {
                [0] = new HashSet<Category> { indoors, table },
                [1] = new HashSet<Category> { outdoors },
                [2] = new HashSet<Category> { indoors, outdoors },
                [3] = new HashSet<Category> { indoors, table },
                [4] = new HashSet<Category> { outdoors },
                [5] = new HashSet<Category> { outdoors, indoors },
                [6] = new HashSet<Category> { indoors },
                [7] = new HashSet<Category> { outdoors },
                [8] = new HashSet<Category> { outdoors, indoors },
                [9] = new HashSet<Category> { indoors },
                [10] = new HashSet<Category> { outdoors, indoors },
                [11] = new HashSet<Category> { outdoors }
            };

            return new Deck("Deportes", ImageSource.FromFile("Assets/Decks/Deck3/backimage.png"), LoadImages("Assets/Decks/Deck3/"), sportsCategories);
        }

        private static IList<ImageSource> LoadImages(string path)
        {
            var imageCards = new List<ImageSource>();
            for (int i = 1; i < 13; i++)
            {
                imageCards.Add(ImageSource.FromFile(path + "card" + i + ".png"));
            }
            return imageCards;
        }
    }
}
