using TestExtractor.Structure.Enums;

namespace ExtractorUi.ViewModel
{
    internal interface INodeTypeFilterViewModel
    {
        NodeTypes NodeType { get; }
        string ToString();
    }
}