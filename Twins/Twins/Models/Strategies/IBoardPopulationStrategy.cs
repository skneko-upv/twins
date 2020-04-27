using System;
using System.Collections.Generic;

namespace Twins.Models.Strategies
{
    public interface IBoardPopulationStrategy
    {
        Board.Cell[,] Populate(int height, int width);
    }

    public static class CollectionExtensions
    {
        private static readonly Random random = new Random();

        public static T PickAndRemoveRandom<T>(this List<T> list)
        {
            return list.PickAndRemove(random.Next(list.Count));
        }

        public static T PickAndRemove<T>(this List<T> list, int index)
        {
            T element = list[index];
            list.RemoveAt(index);
            return element;
        }
    }
}
