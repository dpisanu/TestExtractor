using System.Collections.Generic;

namespace TestExtractor.ExtractorUi.Interfaces
{
    /// <summary>
    ///     Interface specifying the API of the Node Type Filters View Model
    ///     Implements Interface : <see cref="IList{T}" />
    ///     Implements Interface : <see cref="IViewModel" />
    /// </summary>
    internal interface INodeTypeFiltersViewModel : IList<INodeTypeFilterViewModel>, IViewModel
    {
    }
}