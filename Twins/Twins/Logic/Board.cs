using System;
using System.Collections.Generic;
using System.Linq;

namespace Twins.Models
{
    public static class CollectionExtensions
    {
        static readonly Random random = new Random();

        public static T PickAndRemoveRandom<T>(this List<T> list)
            => list.PickAndRemove(random.Next(list.Count));

        public static T PickAndRemove<T>(this List<T> list, int index)
        {
            var element = list[index];
            list.RemoveAt(index);
            return element;
        }
    }

    partial class Board
    {
        readonly Dictionary<(int, int), Cell> cellMap = new Dictionary<(int, int), Cell>();

        public Cell this[int row, int column] 
        {
            get { 
                if (cellMap.TryGetValue((row, column), out Cell selected))
                {
                    return selected;
                } 
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public void FlipCard(int row, int column)
        {
            var flipped = this[row, column];

            if (flipped.KeepRevealed)
            {
                throw new InvalidOperationException();
            }

            FlippedCard = flipped;
            CardFlipped(flipped);
        }

        public void PutBackCard()
        {
            FlippedCard = null;
        }

        public void SetKeepCardRevealed(int row, int column, bool keepRevealed)
        {
            var cell = this[row, column];

            cell.KeepRevealed = keepRevealed;
            RevealedCardsChanged(cell, keepRevealed);
        }

        void Initialize()
        {
            List<(int, int)> emptyPositions = Enumerable.Range(0, Height - 1)
                                            .Zip(Enumerable.Range(0, Width - 1), 
                                                 (r, c) => (r, c))
                                            .ToList();
            List<Card> availableCards = Deck.Cards;

            while (emptyPositions.Count >= 2)
            {
                if (!availableCards.Any())
                {
                    // If there are no more cards available to make pairs, we
                    // reuse the same deck to introduce duplicate pairs.
                    availableCards = Deck.Cards;
                }
                var card = availableCards.PickAndRemoveRandom();

                var (row1, column1) = emptyPositions.PickAndRemoveRandom();
                var (row2, column2) = emptyPositions.PickAndRemoveRandom();

                cellMap.Add((row1, column1), new Cell(row1, column1, card));
                cellMap.Add((row2, column2), new Cell(row2, column2, card));
            }
        }
    }
}
