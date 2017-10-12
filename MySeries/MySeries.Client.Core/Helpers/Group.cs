using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MySeries.Client.Core.Helpers
{
    public class Group<T, TV> : ObservableCollection<TV>
    {
        public T Key { get; }

        public Group(T key, IEnumerable<TV> items)
        {
            Key = key;
            foreach (var item in items)
            {
                Add(item);
            }
        }
    }
}
