using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TestExtractor.ExtractorUi.Interfaces;
using TestExtractor.Structure;

namespace TestExtractor.ExtractorUi.ViewModel
{
    internal sealed class ExtractionGridViewModel : ViewModel, IExtractionGridViewModel
    {
        private static readonly string CountPropertyNames = Reflection.PropertyName((IExtractionGridViewModel vm) => vm.Count);

        public ExtractionGridViewModel()
        {
            Items = new List<INode>();
        }

        public void Add(INode item)
        {
            if (item == null)
            {
                return;
            }
            if (Items.Contains(item))
            {
                return;
            }
            Items.Add(item);
            OnPropertyChanged(CountPropertyNames);
            ForceReload();
        }

        public void AddRange(IEnumerable<INode> nodes)
        {
            if (nodes == null)
            {
                return;
            }
            var except = nodes.Except(Items);
            foreach (var node in except)
            {
                Items.Add(node);
            }
            OnPropertyChanged(CountPropertyNames);
            ForceReload();
        }

        public void AddRange<T>(IEnumerable<T> nodes) where T : INode
        {
            AddRange(nodes.Cast<INode>());
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public void Clear()
        {
            Items.Clear();
            OnPropertyChanged(CountPropertyNames);
            ForceReload();
        }

        public bool Contains(INode item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(INode[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public bool Remove(INode item)
        {
            var removed = Items.Remove(item);
            OnPropertyChanged(CountPropertyNames);
            ForceReload();
            return removed;
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public bool IsReadOnly { get { return Items.IsReadOnly; } }
        
        public IEnumerator<INode> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(INode item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, INode item)
        {
            Items.Insert(index, item);
            OnPropertyChanged(CountPropertyNames);
            ForceReload();
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
            OnPropertyChanged(CountPropertyNames);
            ForceReload();
        }

        public INode this[int index]
        {
            get
            {
                return Items[index];
            }
            set
            {
                Items[index] = value;
            }
        }

        private IList<INode> Items { get; set; }

        /// <summary>
        ///     Force reloading of the items to correctly display them
        /// </summary>
        internal void ForceReload()
        {
            var handler = CollectionChanged;
            if (handler != null)
            {
                handler(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
    }
}