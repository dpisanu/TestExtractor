using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TestExtractor.Client.ExtractorUi.ViewModel;
using TestExtractor.Extractor;
using TestExtractor.Extractor.Extractor;
using TestExtractor.Extractors.NUnit.Extractor;
using TestExtractor.Structure;

namespace TestExtractor.Client.ExtractorUi.Commands
{
    /// <summary>
    ///     Command Class that Extracting of Unit Tests
    ///     Implements Interface : <see cref="ICommand" />
    /// </summary>
    internal class ExtractCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IExtractor _extractor;

        /// <summary>
        ///     Created a new instance of <see cref="ExtractCommand" />
        /// </summary>
        /// <param name="mainWindowViewModel"><see cref="MainWindowViewModel"/> to work on</param>
        internal ExtractCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _extractor = ExtractFactory.Extractor<NUnit>();
        }

        /// <summary>
        ///     Implements <see cref="ICommand.CanExecute" />
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _mainWindowViewModel != null && _mainWindowViewModel.Files.Any();
        }

        /// <summary>
        ///     Implements <see cref="ICommand.Execute" />
        /// </summary>
        public void Execute(object parameter)
        {
            _mainWindowViewModel.ExtractedData.Clear();
            _mainWindowViewModel.ExtractedDataShadow.Clear();

            var span = new TimeSpan();
            _mainWindowViewModel.Information = string.Format("Extracting");

            var nodes = new List<INode>();

            if (_mainWindowViewModel.ExtractTests)
            {
                var result = _extractor.ExtractTimed<IStubNode>(_mainWindowViewModel.Files);
                span = result.Item2;
                nodes.AddRange(result.Item1);
            }
            if (_mainWindowViewModel.ExtractSuits)
            {
                var result = _extractor.ExtractTimed<ISuiteNode>(_mainWindowViewModel.Files);
                span = result.Item2;
                nodes.AddRange(result.Item1);
            }

            _mainWindowViewModel.Information = string.Format("Extraction took {0} ms", span.Milliseconds);
            if (!nodes.Any())
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