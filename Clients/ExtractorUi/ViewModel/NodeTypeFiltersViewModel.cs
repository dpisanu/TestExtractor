using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestExtractor.Client.ExtractorUi.Interfaces;
using TestExtractor.Structure.Enums;

namespace TestExtractor.Client.ExtractorUi.ViewModel
{
    /// <summary>
    ///     Concrete implementation of the Node Type Filters View Model
    ///     Inherrits Class : <see cref="INodeTypeFiltersViewModel" />
    ///     Implements Interface : <see cref="ViewModel" />
    /// </summary>
    internal sealed class NodeTypeFiltersViewModel : Client.ExtractorUi.ViewModel.ViewModel, INodeTypeFiltersViewModel
    {
        /// <summary>
        ///     Created a new instance of <see cref="NodeTypeFiltersViewModel" />
        /// </summary>
        internal NodeTypeFiltersViewModel()
        {
            NodeTypeFilters = new List<INodeTypeFilterViewModel>();

            foreach (NodeTypeFilterViewModel fiter in Enum.GetValues(typeof (NodeTypes))
                .Cast<NodeTypes>()
                .Select(nodeType => new NodeTypeFilterViewModel(nodeType)))
            {
                Add(fiter);
            }
        }

        /// <summary>
        ///     Contains Node Type Filters
        /// </summary>
        private IList<INodeTypeFilterViewModel> NodeTypeFilters { get; set; }

        /// <summary>
        ///     Implements <see cref="IList.GetEnumerator" />
        /// </summary>
        public IEnumerator<INodeTypeFilterViewModel> GetEnumerator()
        {
            return NodeTypeFilters.GetEnumerator();
        }

        /// <summary>
        ///     Implements <see cref="System.Collections.IEnumerable.GetEnumerator" />
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Implements <see cref="IList.Add" />
        /// </summary>
        public void Add(INodeTypeFilterViewModel item)
        {
            NodeTypeFilters.Add(item);
        }

        /// <summary>
        ///     Implements <see cref="IList.Clear" />
        /// </summary>
        public void Clear()
        {
            NodeTypeFilters.Clear();
        }

        /// <summary>
        ///     Implements <see cref="IList.Contains" />
        /// </summary>
        public bool Contains(INodeTypeFilterViewModel item)
        {
            return NodeTypeFilters.Contains(item);
        }

        /// <summary>
        ///     Implements <see cref="IList.CopyTo" />
        /// </summary>
        public void CopyTo(INodeTypeFilterViewModel[] array, int arrayIndex)
        {
            NodeTypeFilters.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Implements <see cref="IList.Remove" />
        /// </summary>
        public bool Remove(INodeTypeFilterViewModel item)
        {
            return NodeTypeFilters.Remove(item);
        }

        /// <summary>
        ///     Implements <see cref="IList.Count" />
        /// </summary>
        public int Count
        {
            get { return NodeTypeFilters.Count; }
        }

        /// <summary>
        ///     Implements <see cref="IList.IsReadOnly" />
        /// </summary>
        public bool IsReadOnly
        {
            get { return NodeTypeFilters.IsReadOnly; }
        }

        /// <summary>
        ///     Implements <see cref="IList.IndexOf" />
        /// </summary>
        public int IndexOf(INodeTypeFilterViewModel item)
        {
            return NodeTypeFilters.IndexOf(item);
        }

        /// <summary>
        ///     Implements <see cref="IList.Insert" />
        /// </summary>
        public void Insert(int index, INodeTypeFilterViewModel item)
        {
            NodeTypeFilters.Insert(index, item);
        }

        /// <summary>
        ///     Implements <see cref="IList.RemoveAt" />
        /// </summary>
        public void RemoveAt(int index)
        {
            NodeTypeFilters.RemoveAt(index);
        }

        /// <summary>
        ///     Implements <see cref="IList.this" />
        /// </summary>
        public INodeTypeFilterViewModel this[int index]
        {
            get { return NodeTypeFilters[index]; }
            set { NodeTypeFilters[index] = value; }
        }
    }
}