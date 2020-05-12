using System;
using System.Collections.Generic;
using Twins.Models;
using Xamarin.Forms;

namespace Twins.Components
{
    public static class BuiltInDecks
    {
        public static Lazy<Deck> Animals = new Lazy<Deck>(CreateAnimalsDeck);
        public static Lazy<Deck> Numbers = new Lazy<Deck>(CreateNumbersDeck);
        public static Lazy<Deck> Sports = new Lazy<Deck>(CreateSportsDeck);

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
            return new Deck("Deportes", ImageSource.FromFile("Assets/Decks/Deck3/backimage.png"), LoadImages("Assets/Decks/Deck3/"));
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
