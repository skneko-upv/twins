﻿using System.Collections.Generic;
using Xamarin.Forms;

namespace Twins.Models
{
    public partial class Deck
    {
        public ImageSource BackImage { get; set; }

        public ISet<Category> Categories = new HashSet<Category>();

        public IList<Card> Cards { get; } = new List<Card>();

        public string Name { get; }

        public Deck(string name, ImageSource backImage, IList<ImageSource> cardImages, IDictionary<int, ISet<Category>> categories = null)
        {
            BackImage = backImage;
            Name = name;

            if (categories == null)
            {
                categories = new Dictionary<int, ISet<Category>>();
            }

            int i = 0;
            foreach(var image in cardImages)
            {
                var cardCategories = new HashSet<Category>(); 
                if (categories.TryGetValue(i, out ISet<Category> declaredCategories)) {
                    cardCategories.UnionWith(declaredCategories);
                }

                var card = new Card(i, this, image, cardCategories);
                Cards.Add(card);
                Categories.UnionWith(cardCategories);

                i++;
            }
        }
    }
}
