using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Input;
using ExtractorUi.Commands;
using ExtractorUi.Interfaces;
using TestExtractor.Structure;

namespace ExtractorUi.ViewModel
{
    internal sealed class MainWindowViewModel : ViewModel, IMainWindowViewModel
    {
        private bool _extractTests;
        private bool _extractSuits;

        public MainWindowViewModel()
        {
            AddFilesCommand = new RelayCommand(AddFilesCommandExecute, AddFilesCommandCanExecute);
            ExtractCommand = new RelayCommand(ExtractCommandCommandExecute, ExtractCommandCommandCanExecute);
        }
        
        public ICommand AddFilesCommand { get; private set; }
        public ICommand ExtractCommand { get; private set; }

        public bool ExtractTests
        {
            get
            {
                return _extractTests;
            }
            set
            {
                _extractTests = value;
                OnPropertyChanged();
            }
        }

        public bool ExtractSuits
        {
            get
            {
                return _extractSuits;
            }
            set
            {
                _extractSuits = value;
                OnPropertyChanged();
            }
        }

        public IList<INode> ExtractedData { get; set; }
        public FlowDocument ResultRichtTextBoxFlowDocument { get; set; }

        private bool AddFilesCommandCanExecute(object o)
        {
            throw new NotImplementedException();
        }

        private void AddFilesCommandExecute(object o)
        {
            throw new NotImplementedException();
        }

        private bool ExtractCommandCommandCanExecute(object o)
        {
            throw new NotImplementedException();
        }

        private void ExtractCommandCommandExecute(object o)
        {
            throw new NotImplementedException();
        }
    }
}
