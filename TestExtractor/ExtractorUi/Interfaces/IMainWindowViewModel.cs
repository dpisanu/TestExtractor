using System.Collections.ObjectModel;
using System.Windows.Input;
using TestExtractor.Structure;

namespace TestExtractor.ExtractorUi.Interfaces
{
    internal interface IMainWindowViewModel : IViewModel
    {
        ICommand AddFilesCommand { get; }

        ICommand ExtractCommand { get; }

        ICommand ExportCommand { get; }

        ICommand FilterCommand { get; }

        bool ExtractTests { get; set; }

        bool ExtractSuits { get; set; }

        int AmoutOfFiles { get; }

        ObservableCollection<INode> ExtractedData { get; }

        ObservableCollection<INodeTypeFilterViewModel> NodeTypeFilters { get; }

        ObservableCollection<ICategoryFilterViewModel> CategoryFilters { get; }

        string Information { get; set; }

        string PackageSize { get; set; }
    }
}