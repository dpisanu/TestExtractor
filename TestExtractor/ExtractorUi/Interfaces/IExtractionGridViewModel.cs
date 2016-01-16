using System.Collections.Generic;
using TestExtractor.Structure;

namespace ExtractorUi.Interfaces
{
    internal interface IExtractionGridViewModel : IViewModel
    {
        IList<INode> Items { get; }

        void Clear();

        void AddItem(INode node);

        void AddItem<T>(T node) where T : INode;

        void AddItems(IEnumerable<INode> nodes);

        void AddItems<T>(IEnumerable<T> nodes) where T : INode;
    }
}