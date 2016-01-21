using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using TestExtractor.ExtractorUi.ViewModel;

namespace TestExtractor.ExtractorUi.Commands
{
    internal class AddFilesCommand : ICommand
    {
        private readonly OpenFileDialog _fileDialog;
        private readonly MainWindowViewModel _mainWindowViewModel;

        internal AddFilesCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _fileDialog = new OpenFileDialog {Multiselect = true};
        }

        public bool CanExecute(object parameter)
        {
            return _mainWindowViewModel != null && _mainWindowViewModel.Files != null && _fileDialog != null;
        }

        public void Execute(object parameter)
        {
            _fileDialog.ShowDialog();
            if (!_fileDialog.FileNames.Any())
            {
                return;
            }

            var files = new List<string>();
            files.AddRange(_mainWindowViewModel.Files);
            files.AddRange(_fileDialog.FileNames);

            _mainWindowViewModel.Files.Clear();
            _mainWindowViewModel.Files.AddRange(files.Distinct());
            _mainWindowViewModel.AmoutOfFiles = _mainWindowViewModel.Files.Count();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}