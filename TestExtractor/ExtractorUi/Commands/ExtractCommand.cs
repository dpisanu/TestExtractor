using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using ExtractorUi.ViewModel;
using TestExtractor.Structure;

namespace ExtractorUi.Commands
{
    internal class ExtractCommand : ICommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        internal ExtractCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (_mainWindowViewModel == null)
            {
                return false;
            }

            return _mainWindowViewModel.Files.Any();
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
                Tuple<IList<IStubNode>, TimeSpan> result =
                    _mainWindowViewModel.Extractor.ExtractTimed<IStubNode>(_mainWindowViewModel.Files);
                span = result.Item2;
                nodes = new List<INode>(result.Item1);
            }
            if (_mainWindowViewModel.ExtractSuits)
            {
                Tuple<IList<ISuiteNode>, TimeSpan> result =
                    _mainWindowViewModel.Extractor.ExtractTimed<ISuiteNode>(_mainWindowViewModel.Files);
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
        }

        public event EventHandler CanExecuteChanged;
    }
}