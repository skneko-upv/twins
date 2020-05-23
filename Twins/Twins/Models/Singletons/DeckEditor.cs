using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Twins.Models.Singletons
{
    class DeckEditor
    {

        public Deck Deck { get; }

        public event EventHandler CardsModified;

        public DeckEditor() 
        {
            Deck = new Deck("NewDeck", null, new List<ImageSource>());
        }

        public DeckEditor(Deck deck) 
        {
            Deck = deck;
        }

        public void AddCard(ImageSource image, ISet<Category> category) 
        {
            Deck.Cards.Add(new Card(Deck.Cards.Count + 1, Deck, image, category));
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
            Deck.Categories.Add(new Category(Deck.Categories.Count+1, name));
        }

        public void RemoveCategory(Category category)
        {
            Deck.Categories.Remove(category);
        }

        public void SaveDeck() { }

    }
}
