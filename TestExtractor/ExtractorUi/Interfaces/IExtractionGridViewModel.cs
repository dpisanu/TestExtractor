using System.Collections.Generic;
using System.Collections.Specialized;
using TestExtractor.Structure;

namespace TestExtractor.ExtractorUi.Interfaces
{
    internal interface IExtractionGridViewModel : IList<INode>, IViewModel
    {
        void AddRange(IEnumerable<INode> nodes);

        void AddRange<T>(IEnumerable<T> nodes) where T : INode;

        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
}