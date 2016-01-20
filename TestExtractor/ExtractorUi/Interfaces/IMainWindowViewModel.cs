using System.Collections.ObjectModel;
using System.Windows.Input;
using TestExtractor.Structure;

namespace ExtractorUi.Interfaces
{
    internal interface IMainWindowViewModel : IViewModel
    {
        ICommand AddFilesCommand { get; }

        ICommand ExtractCommand { get; }

        bool ExtractTests { get; set; }

        bool ExtractSuits { get; set; }

        string AmoutOfFiles { get; }

        ObservableCollection<INode> ExtractedData { get; }

        ObservableCollection<INodeTypeFilterViewModel> NodeTypeFilters { get; }

        ObservableCollection<ICategoryFilterViewModel> CategoryFilters { get; }

        string Information { get; set; }
    }
}