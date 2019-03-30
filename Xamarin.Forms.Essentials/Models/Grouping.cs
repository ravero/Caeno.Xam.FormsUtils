using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace FormsUtils.Models
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items) {
            Key = key;
            items.ToList().ForEach(i => Add(i));
        }
    }
}
