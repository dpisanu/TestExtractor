using TestExtractor.ExtractorUi.Interfaces;
using TestExtractor.Structure.Enums;

namespace TestExtractor.ExtractorUi.ViewModel
{
    /// <summary>
    ///     Concrete implementation of the a Node Type Filer View Model
    ///     Inherrits Class : <see cref="ViewModel" />
    ///     Implements Interface : <see cref="INodeTypeFilterViewModel" />
    /// </summary>
    internal sealed class NodeTypeFilterViewModel : ViewModel, INodeTypeFilterViewModel
    {
        private bool _enabled;
        private NodeTypes _nodeType;

        /// <summary>
        ///     Created a new instance of <see cref="NodeTypeFilterViewModel" />
        /// </summary>
        /// <param name="nodeType"><see cref="NodeTypes" /> contained within</param>
        public NodeTypeFilterViewModel(NodeTypes nodeType)
        {
            NodeType = nodeType;
            Enabled = true;
        }

        /// <summary>
        ///     Implements <see cref="INodeTypeFilterViewModel.NodeType" />
        /// </summary>
        public NodeTypes NodeType
        {
            get { return _nodeType; }
            private set
            {
                _nodeType = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Implements <see cref="INodeTypeFilterViewModel.Enabled" />
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Implements <see cref="INodeTypeFilterViewModel.ToString" />
        /// </summary>
        /// <remarks>
        ///     Overrides <see cref="System.Object.ToString" />
        /// </remarks>
        public override string ToString()
        {
            return NodeType.ToString();
        }
    }
}