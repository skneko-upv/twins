using System;
using System.Collections.Generic;

namespace Twins.Models
{
    public interface IBoardPopulationStrategy
    {
        Board.Cell[,] Populate(int height, int width, Deck deck);
    }

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
}
