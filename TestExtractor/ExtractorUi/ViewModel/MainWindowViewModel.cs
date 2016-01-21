using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestExtractor.ExtractorUi.Commands;
using TestExtractor.ExtractorUi.Interfaces;
using TestExtractor.Structure;
using TestExtractor.Structure.Enums;

namespace TestExtractor.ExtractorUi.ViewModel
{
    internal sealed class MainWindowViewModel : ViewModel, IMainWindowViewModel
    {
        private static readonly string ExtractTestsPropertyName = Reflection.PropertyName((IMainWindowViewModel vm) => vm.ExtractTests);
        private static readonly string ExtractSuitsPropertyName = Reflection.PropertyName((IMainWindowViewModel vm) => vm.ExtractSuits);
        
        private bool _extractSuits;
        private bool _extractTests;
        private string _information;
        private string _packageSize;
        private int _amoutOfFiles;

        public MainWindowViewModel()
        {
            AddFilesCommand = new AddFilesCommand(this);
            ExtractCommand = new ExtractCommand(this);
            ExportCommand = new ExportCommand(this);
            FilterCommand = new FilterCommand(this);
            PopulateCategoryFiltersCommand = new PopulateCategoryFiltersCommand(this);

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
                    FilterCommand.Execute(null);
                    PopulateCategoryFiltersCommand.Execute(null);
                };
            }
        }
        
        public List<string> Files { get; set; }

        public ICommand AddFilesCommand { get; private set; }

        public ICommand ExtractCommand { get; private set; }

        public ICommand ExportCommand { get; private set; }

        public ICommand FilterCommand { get; private set; }

        public ICommand PopulateCategoryFiltersCommand { get; private set; }

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

        public int AmoutOfFiles
        {
            get { return _amoutOfFiles; }
            set
            {
                _amoutOfFiles = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<INode> ExtractedData { get; set; }

        public List<INode> ExtractedDataShadow { get; set; }

        public ObservableCollection<INodeTypeFilterViewModel> NodeTypeFilters { get; set; }

        public ObservableCollection<ICategoryFilterViewModel> CategoryFilters { get; set; }

        public string Information
        {
            get { return _information; }
            set
            {
                _information = value;
                OnPropertyChanged();
            }
        }

        public string PackageSize
        {
            get { return _packageSize; }
            set
            {
                _packageSize = value;
                OnPropertyChanged();
            }
        }

        #region Private 

        #endregion
    }
}