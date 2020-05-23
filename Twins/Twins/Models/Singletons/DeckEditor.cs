using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Twins.Models.Singletons
{
    class DeckEditor
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
            foreach (Card card in Deck.Cards) {
                if (card.Id == id) { 
                    Deck.Cards.Remove(card);
                    break;
                }
            }
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
            Deck.Categories.Remove(category);
        }

        public List<Category> GetCategories() 
        {
            var categories = new List<Category>();
            foreach (Category category in Deck.Categories)
                categories.Add(category);
            return categories;
        }

        // Metodo que deberá llamar al metodo de persistencia de la BD para almacenar el Deck
        public void SaveDeck() { }

    }
}
