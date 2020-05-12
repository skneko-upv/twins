using System;
using System.Collections.Generic;
using System.Linq;

namespace Twins.Utils
{
    public static class CollectionExtensions
    {
        private static readonly Random random = new Random();

        public static IList<T> Clone<T>(this IList<T> list)
            where T : ICloneable
        {
            return list.Select(item => (T)item.Clone()).ToList();
        }

        public static T PickAndRemoveRandom<T>(this IList<T> list)
            => list.PickAndRemove(random.Next(list.Count));

        public static T PickAndRemove<T>(this IList<T> list, int index)
        {
            T element = list[index];
            list.RemoveAt(index);
            return element;
        }

        public static T PickRandom<T>(this IList<T> list)
            => list[random.Next(list.Count)];

        public static IList<T> Repeat<T>(this IList<T> list, int count)
        {
            var result = new List<T>(list.Count * count);
            foreach (var item in list)
            {
                for (int i = 0; i < count; i++)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
