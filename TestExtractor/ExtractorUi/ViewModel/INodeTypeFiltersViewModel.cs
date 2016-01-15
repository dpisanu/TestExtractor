using System.Collections.Generic;

namespace ExtractorUi.ViewModel
{
    internal interface INodeTypeFiltersViewModel
    {
        IList<INodeTypeFilterViewModel> NodeTypeFilters { get; }
    }
}