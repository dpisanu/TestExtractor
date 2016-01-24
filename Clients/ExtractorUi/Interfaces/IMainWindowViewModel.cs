using System.Collections.ObjectModel;
using System.Windows.Input;
using TestExtractor.Structure;

namespace TestExtractor.Client.ExtractorUi.Interfaces
{
    /// <summary>
    ///     Interface specifying the API of the Main Window View Model
    ///     Implements Interface : <see cref="IViewModel" />
    /// </summary>
    internal interface IMainWindowViewModel : IViewModel
    {
        /// <summary>
        ///     The Add Files Command
        /// </summary>
        ICommand AddFilesCommand { get; }

        /// <summary>
        ///     The Extract Command
        /// </summary>
        ICommand ExtractCommand { get; }

        /// <summary>
        ///     The Export Command
        /// </summary>
        ICommand ExportCommand { get; }

        /// <summary>
        ///     The Filter Command
        /// </summary>
        ICommand FilterCommand { get; }

        /// <summary>
        ///     Extract Tests
        /// </summary>
        bool ExtractTests { get; set; }

        /// <summary>
        ///     Extract Suits
        /// </summary>
        bool ExtractSuits { get; set; }

        /// <summary>
        ///     Amount of Files
        /// </summary>
        int AmoutOfFiles { get; }

        /// <summary>
        ///     The Extracted Data from the Test Assemblies
        /// </summary>
        ObservableCollection<INode> ExtractedData { get; }

        /// <summary>
        ///     Available Node Type Filters
        /// </summary>
        ObservableCollection<INodeTypeFilterViewModel> NodeTypeFilters { get; }

        /// <summary>
        ///     Available Category Filters
        /// </summary>
        ObservableCollection<ICategoryFilterViewModel> CategoryFilters { get; }

        /// <summary>
        ///     Information String for current state
        /// </summary>
        string Information { get; set; }

        /// <summary>
        ///     Package Size
        /// </summary>
        string PackageSize { get; set; }
    }
}