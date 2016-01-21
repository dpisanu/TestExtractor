using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestExtractor.ExtractorUi.ViewModel;

namespace TestExtractor.ExtractorUi.Commands
{
    internal class PopulateCategoryFiltersCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        internal PopulateCategoryFiltersCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return _mainWindowViewModel != null;
        }

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

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}