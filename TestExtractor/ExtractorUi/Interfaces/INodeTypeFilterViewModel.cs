using TestExtractor.Structure.Enums;

namespace TestExtractor.ExtractorUi.Interfaces
{
    /// <summary>
    ///     Interface specifying the API of the Node Type Filter View Model
    ///     Implements Interface : <see cref="IViewModel" />
    /// </summary>
    internal interface INodeTypeFilterViewModel : IViewModel
    {
        /// <summary>
        ///     <see cref="NodeTypes" /> of the Filter
        /// </summary>
        NodeTypes NodeType { get; }

        /// <summary>
        ///     Is Filter Enabled
        /// </summary>
        bool Enabled { get; set; }

        /// <summary>
        ///     To String of the Filter
        /// </summary>
        /// <returns>Returns a string representation of the Filter</returns>
        string ToString();
    }
}