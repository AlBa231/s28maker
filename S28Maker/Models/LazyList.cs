using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace S28Maker.Models
{
    internal class LazyList<T>
    {
        private readonly IEnumerable<T> data;

        private ObservableCollection<T> items;
        public ObservableCollection<T> Items => items ??= new ObservableCollection<T>(data);

        public LazyList(IEnumerable<T> data)
        {
            this.data = data;
        }
    }
}
