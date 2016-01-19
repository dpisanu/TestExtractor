using ExtractorUi.Interfaces;
using TestExtractor.Structure.Enums;

namespace ExtractorUi.ViewModel
{
    internal sealed class NodeTypeFilterViewModel : ViewModel, INodeTypeFilterViewModel
    {
        private NodeTypes _nodeType;
        private bool _enabled;

        public NodeTypeFilterViewModel(NodeTypes nodeType)
        {
            NodeType = nodeType;
            Enabled = true;
        }

        public NodeTypes NodeType
        {
            get
            {
                return _nodeType;
            }
            private set
            {
                _nodeType = value;
                OnPropertyChanged();
            }
        }

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return NodeType.ToString();
        }
    }
}