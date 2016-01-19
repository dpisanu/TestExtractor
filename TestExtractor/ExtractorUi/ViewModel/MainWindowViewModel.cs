using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using ExtractorUi.Commands;
using ExtractorUi.Interfaces;
using Microsoft.Win32;
using TestExtractor.Extractor.Extractor;
using TestExtractor.Extractor.Filter;
using TestExtractor.Extractors.NUnit.Extractor;
using TestExtractor.Structure;
using TestExtractor.Structure.Enums;

namespace ExtractorUi.ViewModel
{
    internal sealed class MainWindowViewModel : ViewModel, IMainWindowViewModel
    {
        private static readonly string AmountOfFilesPropertyName = Reflection.PropertyName((IMainWindowViewModel vm) => vm.AmoutOfFiles);
        private static readonly string ExtractTestsPropertyName = Reflection.PropertyName((IMainWindowViewModel vm) => vm.ExtractTests);
        private static readonly string ExtractSuitsPropertyName = Reflection.PropertyName((IMainWindowViewModel vm) => vm.ExtractSuits);
        private static readonly string InformationPropertyName = Reflection.PropertyName((IMainWindowViewModel vm) => vm.Information);

        private readonly IExtractor _extractor;
        private readonly IFilter _filter;
        private readonly OpenFileDialog _fileDialog;
        private readonly List<string> _files;
        private bool _extractSuits;
        private bool _extractTests;
        private string _information;

        public MainWindowViewModel()
        {
            AddFilesCommand = new RelayCommand(AddFilesCommandExecute, AddFilesCommandCanExecute);
            ExtractCommand = new RelayCommand(ExtractCommandCommandExecute, ExtractCommandCommandCanExecute);
            ExtractedData = new ObservableCollection<INode>();
            ExtractedDataShadow = new List<INode>();
            NodeTypeFilters = new ObservableCollection<INodeTypeFilterViewModel>();

            foreach (var filter in Enum.GetValues(typeof(NodeTypes))
                        .Cast<NodeTypes>()
                        .Select(nodeType => new NodeTypeFilterViewModel(nodeType)))
            {
                NodeTypeFilters.Add(filter);
                filter.PropertyChanged += delegate { Filter(); };
            }

            _fileDialog = new OpenFileDialog { Multiselect = true };
            _files = new List<string>();
            _extractor = new NUnit();
            _filter = new Filter();
            ExtractSuits = true;
        }
        
        public ICommand AddFilesCommand { get; private set; }

        public ICommand ExtractCommand { get; private set; }

        public bool ExtractTests
        {
            get { return _extractTests; }
            set
            {
                _extractTests = value;
                _extractSuits = !value;
                OnPropertyChanged();
                OnPropertyChanged(ExtractSuitsPropertyName);
            }
        }

        public bool ExtractSuits
        {
            get { return _extractSuits; }
            set
            {
                _extractSuits = value;
                _extractTests = !value;
                OnPropertyChanged();
                OnPropertyChanged(ExtractTestsPropertyName);
            }
        }

        public string AmoutOfFiles
        {
            get { return _files.Count.ToString(CultureInfo.InvariantCulture); }
        }

        public ObservableCollection<INode> ExtractedData { get; private set; }
        private List<INode> ExtractedDataShadow { get; set; }

        public ObservableCollection<INodeTypeFilterViewModel> NodeTypeFilters { get; private set; }

        public string Information
        {
            get { return _information; }
            set
            {
                _information = value;
                OnPropertyChanged(InformationPropertyName);
            }
        }

        private bool AddFilesCommandCanExecute(object o)
        {
            return _fileDialog != null && _files != null;
        }

        private void AddFilesCommandExecute(object o)
        {
            _fileDialog.ShowDialog();
            _files.AddRange(_fileDialog.FileNames);
            OnPropertyChanged(AmountOfFilesPropertyName);
        }

        private bool ExtractCommandCommandCanExecute(object o)
        {
            return _files.Any();
        }

        private void ExtractCommandCommandExecute(object o)
        {
            ExtractedData.Clear();
            ExtractedDataShadow.Clear();
            Information = string.Format("Clearing Extracted Data");

            var span = new TimeSpan();
            Information = string.Format("Extracting");

            List<INode> nodes = null;

            if (ExtractTests)
            {
                var result = _extractor.ExtractTimed<IStubNode>(_files);
                span = result.Item2;
                nodes = new List<INode>(result.Item1);
            }
            if (ExtractSuits)
            {
                var result = _extractor.ExtractTimed<ISuiteNode>(_files);
                span = result.Item2;
                nodes = new List<INode>(result.Item1);
            }

            Information = string.Format("Extraction took {0} ms", span.Milliseconds);
            if (nodes == null || !nodes.Any())
            {
                return;
            }

            foreach (var node in nodes)
            {
                ExtractedData.Add(node);
            }
            ExtractedDataShadow.AddRange(ExtractedData);
            
            Filter();
        }

        private void Filter()
        {
            ExtractedData.Clear();
            
            var filteredNodes = new List<INode>();
            // Node Types Filter
            var nodeTypes = (from nodeTypeFilterViewModel in NodeTypeFilters where nodeTypeFilterViewModel.Enabled select nodeTypeFilterViewModel.NodeType).ToList();

            filteredNodes.AddRange(_filter.FilterNodeTypes(ExtractedDataShadow, nodeTypes).OfFilters);
            foreach (var filteredNode in filteredNodes)
            {
                ExtractedData.Add(filteredNode);
            }
        }
    }
}