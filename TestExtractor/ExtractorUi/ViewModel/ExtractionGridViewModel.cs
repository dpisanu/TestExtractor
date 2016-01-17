using System.Collections.Generic;
using System.Linq;
using ExtractorUi.Interfaces;
using TestExtractor.Structure;

namespace ExtractorUi.ViewModel
{
    internal sealed class ExtractionGridViewModel : ViewModel, IExtractionGridViewModel
    {
        private static readonly string ItemsPropertyNames = Reflection.PropertyName((IExtractionGridViewModel vm) => vm.Items);
        private IList<INode> _items;

        public ExtractionGridViewModel()
        {
            Items = new List<INode>();
        }

        public IList<INode> Items
        {
            get
            {
                return _items;
            }
            private set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public void Clear()
        {
            Items.Clear();
            OnPropertyChanged(ItemsPropertyNames);
        }

        public void AddItem(INode node)
        {
            if (node == null)
            {
                return;
            }
            if (Items.Contains(node))
            {
                return;
            }
            Items.Add(node);
            OnPropertyChanged(ItemsPropertyNames);
        }

        public void AddItem<T>(T node) where T : INode
        {
            AddItem(node as INode);
        }
        
        public void AddItems(IEnumerable<INode> nodes)
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
            OnPropertyChanged(ItemsPropertyNames);
        }

        public void AddItems<T>(IEnumerable<T> nodes) where T : INode
        {
            AddItems(nodes.Cast<INode>());
        }
    }
}