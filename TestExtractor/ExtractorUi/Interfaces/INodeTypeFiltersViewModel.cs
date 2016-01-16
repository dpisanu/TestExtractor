using System.Collections.Generic;

namespace ExtractorUi.Interfaces
{
    internal interface INodeTypeFiltersViewModel : IViewModel
    {
        IList<INodeTypeFilterViewModel> NodeTypeFilters { get; }
    }
}