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

        private readonly ICommand _extractCommand;
        private readonly OpenFileDialog _fileDialog;
        private readonly IFilter _filter;
        private bool _extractSuits;
        private bool _extractTests;
        private string _information;

        public MainWindowViewModel()
        {
            AddFilesCommand = new RelayCommand(AddFilesCommandExecute, AddFilesCommandCanExecute);
            ExtractCommand = new RelayCommand(ExtractCommandCommandExecute, ExtractCommandCommandCanExecute);

            Extractor = new NUnit();
            Files = new List<string>();
            ExtractedData = new ObservableCollection<INode>();
            ExtractedDataShadow = new List<INode>();
            NodeTypeFilters = new ObservableCollection<INodeTypeFilterViewModel>();
            CategoryFilters = new ObservableCollection<ICategoryFilterViewModel>();
            ExtractSuits = true;

            foreach (var filter in Enum.GetValues(typeof (NodeTypes)) .Cast<NodeTypes>().Select(nodeType => new NodeTypeFilterViewModel(nodeType)))
            {
                NodeTypeFilters.Add(filter);
                filter.PropertyChanged += delegate
                {
                    Filter();
                    PopulateCategoryFilters();
                };
            }

            _fileDialog = new OpenFileDialog {Multiselect = true};
            _filter = new Filter();
            _extractCommand = new ExtractCommand(this);
        }

        public IExtractor Extractor { get; private set; }
        public List<string> Files { get; private set; }
        public List<INode> ExtractedDataShadow { get; set; }

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
            get { return Files.Count.ToString(CultureInfo.InvariantCulture); }
        }

        public ObservableCollection<INode> ExtractedData { get; set; }

        public ObservableCollection<INodeTypeFilterViewModel> NodeTypeFilters { get; set; }
        public ObservableCollection<ICategoryFilterViewModel> CategoryFilters { get; set; }

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
            return _fileDialog != null && Files != null;
        }

        private void AddFilesCommandExecute(object o)
        {
            _fileDialog.ShowDialog();
            Files.AddRange(_fileDialog.FileNames);
            OnPropertyChanged(AmountOfFilesPropertyName);
        }

        private bool ExtractCommandCommandCanExecute(object o)
        {
            return _extractCommand.CanExecute(o);
        }

        private void ExtractCommandCommandExecute(object o)
        {
            _extractCommand.Execute(o);

            PopulateCategoryFilters();
            Filter();
        }

        private void Filter()
        {
            ExtractedData.Clear();

            // Node Types Filter
            var nodeFilteredNodes = new List<INode>();
            var nodeTypes = (from nodeTypeFilterViewModel in NodeTypeFilters
                where nodeTypeFilterViewModel.Enabled
                select nodeTypeFilterViewModel.NodeType).ToList();
            nodeFilteredNodes.AddRange(_filter.FilterNodeTypes(ExtractedDataShadow, nodeTypes).OfFilters);

            var categoryFilterNodes = new List<INode>();
            var categories = (from categoryFilterViewModel in CategoryFilters
                where categoryFilterViewModel.Enabled
                select categoryFilterViewModel.Category).ToList();
            categoryFilterNodes.AddRange(_filter.FilterCategories(nodeFilteredNodes, categories).OfFilters);

            foreach (var filteredNode in categoryFilterNodes)
            {
                ExtractedData.Add(filteredNode);
            }
        }

        private void PopulateCategoryFilters()
        {
            CategoryFilters.Clear();

            var categories = new List<string>();
            foreach (var node in ExtractedDataShadow)
            {
                categories.AddRange(node.Categories);
            }
            categories.Add(string.Empty);

            foreach (var categoryFilter in categories.Distinct().Select(category => new CategoryFilterViewModel(category)))
            {
                CategoryFilters.Add(categoryFilter);
                categoryFilter.PropertyChanged += delegate { Filter(); };
            }
        }
    }
}