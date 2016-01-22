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
    /// <summary>
    ///     Command Class that handles the Filtering of the extraced Unit Test List by specific criterias
    ///     Implements Interface : <see cref="ICommand" />
    /// </summary>
    internal class FilterCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IFilter _filter;

        /// <summary>
        ///     Created a new instance of <see cref="FilterCommand" />
        /// </summary>
        /// <param name="mainWindowViewModel"><see cref="MainWindowViewModel"/> to work on</param>
        internal FilterCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _filter = new Filter.Filter();
        }

        /// <summary>
        ///     Implements <see cref="ICommand.CanExecute" />
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _mainWindowViewModel != null;
        }

        /// <summary>
        ///     Implements <see cref="ICommand.Execute" />
        /// </summary>
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

        /// <summary>
        ///     Implements <see cref="ICommand.CanExecuteChanged" />
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}