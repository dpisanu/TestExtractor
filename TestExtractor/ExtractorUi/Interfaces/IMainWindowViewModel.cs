using System.Collections;
using System.Collections.Generic;
using System.Windows.Documents;
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

        IList<INode> ExtractedData { get; }

        FlowDocument ResultRichtTextBoxFlowDocument { get; set; }
    }
}