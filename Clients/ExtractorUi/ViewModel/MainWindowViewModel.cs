using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestExtractor.Client.ExtractorUi.Commands;
using TestExtractor.Client.ExtractorUi.Interfaces;
using TestExtractor.ExtractorUi;
using TestExtractor.Structure;
using TestExtractor.Structure.Enums;

namespace TestExtractor.Client.ExtractorUi.ViewModel
{
    /// <summary>
    ///     Concrete implementation of the Main Window View Model
    ///     Inherrits Class : <see cref="ViewModel" />
    ///     Implements Interface : <see cref="IMainWindowViewModel" />
    /// </summary>
    internal sealed class MainWindowViewModel : ViewModel, IMainWindowViewModel
    {
        private static readonly string ExtractTestsPropertyName =
            Reflection.PropertyName((IMainWindowViewModel vm) => vm.ExtractTests);

        private static readonly string ExtractSuitsPropertyName =
            Reflection.PropertyName((IMainWindowViewModel vm) => vm.ExtractSuits);

        private int _amoutOfFiles;

        private bool _extractSuits;
        private bool _extractTests;
        private string _information;
        private string _packageSize;

        /// <summary>
        ///     Created a new instance of <see cref="MainWindowViewModel" />
        /// </summary>
        internal MainWindowViewModel()
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

            foreach (
                var filter in
                    Enum.GetValues(typeof (NodeTypes))
                        .Cast<NodeTypes>()
                        .Select(nodeType => new NodeTypeFilterViewModel(nodeType)))
            {
                NodeTypeFilters.Add(filter);
                filter.PropertyChanged += delegate
                {
                    FilterCommand.Execute(null);
                    PopulateCategoryFiltersCommand.Execute(null);
                };
            }
        }

        /// <summary>
        ///     List of Files
        /// </summary>
        public List<string> Files { get; set; }

        /// <summary>
        ///     Command to Populate the Category Filters
        /// </summary>
        public ICommand PopulateCategoryFiltersCommand { get; private set; }

        /// <summary>
        ///     Extracted Data Shadow
        /// </summary>
        public List<INode> ExtractedDataShadow { get; set; }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.AddFilesCommand" />
        /// </summary>
        public ICommand AddFilesCommand { get; private set; }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.ExtractCommand" />
        /// </summary>
        public ICommand ExtractCommand { get; private set; }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.ExportCommand" />
        /// </summary>
        public ICommand ExportCommand { get; private set; }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.FilterCommand" />
        /// </summary>
        public ICommand FilterCommand { get; private set; }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.ExtractTests" />
        /// </summary>
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

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.ExtractSuits" />
        /// </summary>
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

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.AmoutOfFiles" />
        /// </summary>
        public int AmoutOfFiles
        {
            get { return _amoutOfFiles; }
            set
            {
                _amoutOfFiles = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.ExtractedData" />
        /// </summary>
        public ObservableCollection<INode> ExtractedData { get; set; }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.NodeTypeFilters" />
        /// </summary>
        public ObservableCollection<INodeTypeFilterViewModel> NodeTypeFilters { get; set; }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.CategoryFilters" />
        /// </summary>
        public ObservableCollection<ICategoryFilterViewModel> CategoryFilters { get; set; }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.Information" />
        /// </summary>
        public string Information
        {
            get { return _information; }
            set
            {
                _information = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Implements <see cref="IMainWindowViewModel.PackageSize" />
        /// </summary>
        public string PackageSize
        {
            get { return _packageSize; }
            set
            {
                _packageSize = value;
                OnPropertyChanged();
            }
        }
    }
}