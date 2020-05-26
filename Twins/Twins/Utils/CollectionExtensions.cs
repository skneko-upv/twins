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
        {
            return list.PickAndRemove(random.Next(list.Count));
        }

        public static T PickAndRemove<T>(this IList<T> list, int index)
        {
            T element = list[index];
            list.RemoveAt(index);
            return element;
        }

        public static T PickRandom<T>(this IList<T> list)
        {
            return list[random.Next(list.Count)];
        }

        public static IList<T> Repeat<T>(this IList<T> list, int count)
        {
            List<T> result = new List<T>(list.Count * count);
            foreach (T item in list)
            {
                for (int i = 0; i < count; i++)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public static int IndexOf<T>(this IReadOnlyList<T> self, T elementToFind)
        {
            int i = 0;
            foreach (T element in self)
            {
                if (Equals(element, elementToFind))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
    }
}
