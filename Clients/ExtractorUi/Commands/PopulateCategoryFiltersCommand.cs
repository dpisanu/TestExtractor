using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestExtractor.Client.ExtractorUi.ViewModel;

namespace TestExtractor.Client.ExtractorUi.Commands
{
    /// <summary>
    ///     Command Class that handles the Populating of the Filters for the current Extracted List
    ///     Implements Interface : <see cref="ICommand" />
    /// </summary>
    internal class PopulateCategoryFiltersCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        /// <summary>
        ///     Created a new instance of <see cref="PopulateCategoryFiltersCommand" />
        /// </summary>
        /// <param name="mainWindowViewModel"><see cref="MainWindowViewModel"/> to work on</param>
        internal PopulateCategoryFiltersCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
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
            _mainWindowViewModel.CategoryFilters.Clear();

            var categories = new List<string>();
            foreach (var node in _mainWindowViewModel.ExtractedDataShadow)
            {
                categories.AddRange(node.Categories);
            }
            categories.Add(string.Empty);

            foreach (var categoryFilter in categories.Distinct().Select(category => new CategoryFilterViewModel(category)))
            {
                _mainWindowViewModel.CategoryFilters.Add(categoryFilter);
                categoryFilter.PropertyChanged += delegate { _mainWindowViewModel.FilterCommand.Execute(null); };
            }
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