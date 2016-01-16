using System;
using System.Collections.Generic;
using System.Linq;
using ExtractorUi.Interfaces;
using TestExtractor.Structure.Enums;

namespace ExtractorUi.ViewModel
{
    internal class NodeTypeFiltersViewModel : ViewModel, INodeTypeFiltersViewModel
    {
        public NodeTypeFiltersViewModel()
        {
            NodeTypeFilters = new List<INodeTypeFilterViewModel>();

            foreach (var fiter in Enum.GetValues(typeof (NodeTypes))
                        .Cast<NodeTypes>()
                        .Select(nodeType => new NodeTypeFilterViewModel(nodeType)))
            {
                NodeTypeFilters.Add(fiter);
            }
        }

        public IList<INodeTypeFilterViewModel> NodeTypeFilters { get; private set; }
    }
}