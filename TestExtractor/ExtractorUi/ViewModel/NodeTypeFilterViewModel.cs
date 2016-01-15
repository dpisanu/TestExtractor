using TestExtractor.Structure.Enums;

namespace ExtractorUi.ViewModel
{
    internal class NodeTypeFilterViewModel : INodeTypeFilterViewModel
    {
        public NodeTypeFilterViewModel(NodeTypes nodeType)
        {
            NodeType = nodeType;
        }

        public NodeTypes NodeType { get; private set; }

        public override string ToString()
        {
            return NodeType.ToString();
        }
    }
}