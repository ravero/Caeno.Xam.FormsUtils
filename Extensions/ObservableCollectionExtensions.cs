using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;

namespace FormsUtils.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void ClearAndAdd<T>(this ObservableCollection<T> collection, IEnumerable<T> items) {
            collection.Clear();
            foreach (var item in items)
                collection.Add(item);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable) =>
            new ObservableCollection<T>(enumerable);
    }
}
