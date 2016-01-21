using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using TestExtractor.ExtractorUi.ViewModel;
using TestExtractor.Filter;
using TestExtractor.Structure;

namespace TestExtractor.ExtractorUi.Commands
{
    internal class FilterCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IFilter _filter;

        internal FilterCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _filter = new Filter.Filter();
        }

        public bool CanExecute(object parameter)
        {
            return _mainWindowViewModel != null;
        }

        public void Execute(object parameter)
        {
            _mainWindowViewModel.ExtractedData.Clear();

            // Node Types Filter
            var nodeFilteredNodes = new List<INode>();
            var nodeTypes = (
                from nodeTypeFilterViewModel in _mainWindowViewModel.NodeTypeFilters
                where nodeTypeFilterViewModel.Enabled
                select nodeTypeFilterViewModel.NodeType).ToList();
            nodeFilteredNodes.AddRange(_filter.FilterNodeTypes(_mainWindowViewModel.ExtractedDataShadow, nodeTypes).OfFilters);

            var categoryFilterNodes = new List<INode>();
            var categories = (
                from categoryFilterViewModel in _mainWindowViewModel.CategoryFilters
                where categoryFilterViewModel.Enabled
                select categoryFilterViewModel.Category).ToList();
            categoryFilterNodes.AddRange(_filter.FilterCategories(nodeFilteredNodes, categories).OfFilters);

            foreach (var filteredNode in categoryFilterNodes)
            {
                _mainWindowViewModel.ExtractedData.Add(filteredNode);
            }
            _mainWindowViewModel.PackageSize = _mainWindowViewModel.ExtractedData.Count().ToString(CultureInfo.InvariantCulture);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}