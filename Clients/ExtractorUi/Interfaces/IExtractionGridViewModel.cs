using System.Collections.Generic;
using System.Collections.Specialized;
using TestExtractor.Structure;

namespace TestExtractor.Client.ExtractorUi.Interfaces
{
    /// <summary>
    ///     Interface specifying the API of the Extraction Grid View Model
    ///     Implements Interface : <see cref="IList{T}" />
    ///     Implements Interface : <see cref="IViewModel" />
    /// </summary>
    internal interface IExtractionGridViewModel : IList<INode>, IViewModel
    {
        /// <summary>
        ///     Add a Range of <see cref="INode" />
        /// </summary>
        /// <param name="nodes"><see cref="IEnumerable{T}" /> of items to add</param>
        void AddRange(IEnumerable<INode> nodes);

        /// <summary>
        ///     Add a Range of Items T
        /// </summary>
        /// <typeparam name="T">T of Item to that. Needs to be of <see cref="INode" /></typeparam>
        /// <param name="nodes"><see cref="IEnumerable{T}" /> of items to add</param>
        void AddRange<T>(IEnumerable<T> nodes) where T : INode;

        /// <summary>
        ///     Collection Changed event
        /// </summary>
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}