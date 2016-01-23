using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using TestExtractor.ExtractorUi.Interfaces;
using TestExtractor.Structure;

namespace TestExtractor.ExtractorUi.ViewModel
{
    /// <summary>
    ///     Concrete implementation of the Extraction Grid View Model
    ///     Inherrits Class : <see cref="ViewModel" />
    ///     Implements Interface : <see cref="IExtractionGridViewModel" />
    /// </summary>
    internal sealed class ExtractionGridViewModel : ViewModel, IExtractionGridViewModel
    {
        /// <summary>
        ///     Created a new instance of <see cref="ExtractionGridViewModel" />
        /// </summary>
        public ExtractionGridViewModel()
        {
            Items = new List<INode>();
        }

        /// <summary>
        ///     Internal Items
        /// </summary>
        private IList<INode> Items { get; set; }

        /// <summary>
        ///     Implements <see cref="IList.Add" />
        /// </summary>
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
            ForceReload();
        }

        /// <summary>
        ///     Implements <see cref="IExtractionGridViewModel.AddRange" />
        /// </summary>
        public void AddRange(IEnumerable<INode> nodes)
        {
            if (nodes == null)
            {
                return;
            }
            IEnumerable<INode> except = nodes.Except(Items);
            foreach (INode node in except)
            {
                Items.Add(node);
            }
            ForceReload();
        }

        /// <summary>
        ///     Implements <see cref="IExtractionGridViewModel.AddRange{T}" />
        /// </summary>
        public void AddRange<T>(IEnumerable<T> nodes) where T : INode
        {
            AddRange(nodes.Cast<INode>());
        }

        /// <summary>
        ///     Collection Changed event
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        /// <summary>
        ///     Implements <see cref="IList.Clear" />
        /// </summary>
        public void Clear()
        {
            Items.Clear();
            ForceReload();
        }

        /// <summary>
        ///     Implements <see cref="IList.Contains" />
        /// </summary>
        public bool Contains(INode item)
        {
            return Items.Contains(item);
        }

        /// <summary>
        ///     Implements <see cref="IList.CopyTo" />
        /// </summary>
        public void CopyTo(INode[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Implements <see cref="IList.Remove" />
        /// </summary>
        public bool Remove(INode item)
        {
            bool removed = Items.Remove(item);
            ForceReload();
            return removed;
        }

        /// <summary>
        ///     Implements <see cref="IList.Count" />
        /// </summary>
        public int Count
        {
            get { return Items.Count; }
        }

        /// <summary>
        ///     Implements <see cref="IList.IsReadOnly" />
        /// </summary>
        public bool IsReadOnly
        {
            get { return Items.IsReadOnly; }
        }

        /// <summary>
        ///     Implements <see cref="IList.GetEnumerator" />
        /// </summary>
        public IEnumerator<INode> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Implements <see cref="IList.IndexOf" />
        /// </summary>
        public int IndexOf(INode item)
        {
            return Items.IndexOf(item);
        }

        /// <summary>
        ///     Implements <see cref="IList.Insert" />
        /// </summary>
        public void Insert(int index, INode item)
        {
            Items.Insert(index, item);
            ForceReload();
        }

        /// <summary>
        ///     Implements <see cref="IList.RemoveAt" />
        /// </summary>
        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
            ForceReload();
        }

        /// <summary>
        ///     Implements <see cref="IList.this" />
        /// </summary>
        public INode this[int index]
        {
            get { return Items[index]; }
            set { Items[index] = value; }
        }

        /// <summary>
        ///     Force reloading of the items to correctly display them
        /// </summary>
        internal void ForceReload()
        {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null)
            {
                handler(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }
    }
}