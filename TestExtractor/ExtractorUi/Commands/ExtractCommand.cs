using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestExtractor.Extractor.Extractor;
using TestExtractor.Extractors.NUnit.Extractor;
using TestExtractor.ExtractorUi.ViewModel;
using TestExtractor.Structure;

namespace TestExtractor.ExtractorUi.Commands
{
    internal class ExtractCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IExtractor _extractor;

        internal ExtractCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _extractor = new NUnit();
        }

        public bool CanExecute(object parameter)
        {
            return _mainWindowViewModel != null && _mainWindowViewModel.Files.Any();
        }

        public void Execute(object parameter)
        {
            _mainWindowViewModel.ExtractedData.Clear();
            _mainWindowViewModel.ExtractedDataShadow.Clear();

            var span = new TimeSpan();
            _mainWindowViewModel.Information = string.Format("Extracting");

            List<INode> nodes = null;

            if (_mainWindowViewModel.ExtractTests)
            {
                var result = _extractor.ExtractTimed<IStubNode>(_mainWindowViewModel.Files);
                span = result.Item2;
                nodes = new List<INode>(result.Item1);
            }
            if (_mainWindowViewModel.ExtractSuits)
            {
                var result = _extractor.ExtractTimed<ISuiteNode>(_mainWindowViewModel.Files);
                span = result.Item2;
                nodes = new List<INode>(result.Item1);
            }

            _mainWindowViewModel.Information = string.Format("Extraction took {0} ms", span.Milliseconds);
            if (nodes == null || !nodes.Any())
            {
                return;
            }

            foreach (var node in nodes)
            {
                _mainWindowViewModel.ExtractedData.Add(node);
            }
            _mainWindowViewModel.ExtractedDataShadow.AddRange(nodes);

            _mainWindowViewModel.PopulateCategoryFiltersCommand.Execute(null);
            _mainWindowViewModel.FilterCommand.Execute(null);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}