using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;

namespace FormsUtils.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable) =>
            new ObservableCollection<T>(enumerable);

        /// <summary>
        /// Adds a Range of elements to the collection.
        /// </summary>
        /// <param name="collection">The collection to wich append the range of items.</param>
        /// <param name="items">The enumeration of items to be appended.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items) =>
            items.Iter(i => collection.Add(i));

        /// <summary>
        /// Clears the collection and Add a Range of elements.
        /// </summary>
        /// <param name="collection">The collection to which reset with the range of new items.</param>
        /// <param name="items">The enumeration of items to be appended.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void ResetWithRange<T>(this ICollection<T> collection, IEnumerable<T> items) {
            collection.Clear();
            collection.AddRange(items);
        }

        /// <summary>
        /// Iterates over the Enumerable object, applying the specified action to each element of the enumeration.
        /// </summary>
        /// <param name="enumerable">The enumerable on which to iter.</param>
        /// <param name="action">The action to be executed against each element of the iteration.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static void Iter<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (var item in enumerable)
                action(item);
        }
    }
}
