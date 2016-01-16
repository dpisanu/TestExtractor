using TestExtractor.Structure.Enums;

namespace ExtractorUi.Interfaces
{
    internal interface INodeTypeFilterViewModel : IViewModel
    {
        NodeTypes NodeType { get; }

        bool Enabled { get; set; }

        string ToString();
    }
}