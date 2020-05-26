using System;
using System.Collections.Generic;
using Twins.Models.Singletons;
using Xamarin.Forms;

namespace Twins.Models
{
    internal class DeckEditor
    {

        public Deck Deck { get; }
        public event EventHandler CategoriesModified;
        public event EventHandler CardsModified;

        public DeckEditor()
        {
            Deck = new Deck("NewDeck", null, new List<ImageSource>());
        }

        public DeckEditor(Deck deck)
        {
            Deck = deck;
        }

        public void AddCard(ImageSource image, Category category)
        {
            Deck.Cards.Add(new Card(Deck.Cards.Count + 1, Deck, image, new HashSet<Category> { category }));
            CardsModified?.Invoke(this, null);
        }

        public void RemoveCard(int id)
        {
            foreach (Card card in Deck.Cards)
            {
                if (card.Id == id)
                {
                    Deck.Cards.Remove(card);
                    break;
                }
            }
            CardsModified?.Invoke(this, null);
        }

        public void AddBackImage(ImageSource image)
        {
            Deck.BackImage = image;
        }

        public void AddCategory(string name)
        {
            Category category = new Category(Deck.Categories.Count + 1, name);
            Deck.Categories.Add(category);
            CategoriesModified?.Invoke(this, null);
        }

        internal void AddName(string name)
        {
            Deck.Name = name;
        }

        public void RemoveCategory(Category category)
        {
            if (IsCategoryAlreadyAsigned(category))
            {
                throw new Exception("No se puede borrar la categoría porque tiene " +
                 "cartas asiganadas. Borre las cartas primero y luego la categoría");
            }

            Deck.Categories.Remove(category);
            CategoriesModified?.Invoke(this, null);
        }

        private bool IsCategoryAlreadyAsigned(Category category)
        {
            foreach (Card card in Deck.Cards)
            {
                if (card.Categories.Contains(category)) { return true; }
            }
            return false;
        }

        public List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            foreach (Category category in Deck.Categories)
            {
                categories.Add(category);
            }

            return categories;
        }

        // Metodo que deberá llamar al metodo de persistencia de la BD para almacenar el Deck
        public void SaveDeck()
        {
            PlayerPreferences.Instance.PlayerDecks.Add(Deck);
        }

    }
}
