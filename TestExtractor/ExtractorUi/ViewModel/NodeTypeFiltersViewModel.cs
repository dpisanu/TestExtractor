using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExtractorUi.Interfaces;
using TestExtractor.Structure.Enums;

namespace ExtractorUi.ViewModel
{
    internal sealed class NodeTypeFiltersViewModel : ViewModel, INodeTypeFiltersViewModel
    {
        public NodeTypeFiltersViewModel()
        {
            NodeTypeFilters = new List<INodeTypeFilterViewModel>();

            foreach (var fiter in Enum.GetValues(typeof (NodeTypes))
                        .Cast<NodeTypes>()
                        .Select(nodeType => new NodeTypeFilterViewModel(nodeType)))
            {
                Add(fiter);
            }
        }
        
        public IEnumerator<INodeTypeFilterViewModel> GetEnumerator()
        {
            return NodeTypeFilters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(INodeTypeFilterViewModel item)
        {
            NodeTypeFilters.Add(item);
        }

        public void Clear()
        {
            NodeTypeFilters.Clear();
        }

        public bool Contains(INodeTypeFilterViewModel item)
        {
            return NodeTypeFilters.Contains(item);
        }

        public void CopyTo(INodeTypeFilterViewModel[] array, int arrayIndex)
        {
            NodeTypeFilters.CopyTo(array, arrayIndex);
        }

        public bool Remove(INodeTypeFilterViewModel item)
        {
            return NodeTypeFilters.Remove(item);
        }

        public int Count 
        { 
            get { return NodeTypeFilters.Count; } 
        }

        public bool IsReadOnly
        {
            get { return NodeTypeFilters.IsReadOnly; }
        }
        
        public int IndexOf(INodeTypeFilterViewModel item)
        {
            return NodeTypeFilters.IndexOf(item);
        }

        public void Insert(int index, INodeTypeFilterViewModel item)
        {
            NodeTypeFilters.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            NodeTypeFilters.RemoveAt(index);
        }

        public INodeTypeFilterViewModel this[int index]
        {
            get { return NodeTypeFilters[index]; }
            set { NodeTypeFilters[index] = value; }
        }

        private IList<INodeTypeFilterViewModel> NodeTypeFilters { get; set; }
    }
}