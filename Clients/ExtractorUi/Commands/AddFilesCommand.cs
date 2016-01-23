using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using TestExtractor.ExtractorUi.ViewModel;

namespace TestExtractor.ExtractorUi.Commands
{
    /// <summary>
    ///     Command Class that handles the Adding of Files
    ///     Implements Interface : <see cref="ICommand" />
    /// </summary>
    internal class AddFilesCommand : ICommand
    {
        private readonly OpenFileDialog _fileDialog;
        private readonly MainWindowViewModel _mainWindowViewModel;

        /// <summary>
        ///     Created a new instance of <see cref="AddFilesCommand" />
        /// </summary>
        /// <param name="mainWindowViewModel"><see cref="MainWindowViewModel"/> to work on</param>
        internal AddFilesCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _fileDialog = new OpenFileDialog {Multiselect = true};
        }

        /// <summary>
        ///     Implements <see cref="ICommand.CanExecute" />
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _mainWindowViewModel != null && _mainWindowViewModel.Files != null && _fileDialog != null;
        }

        /// <summary>
        ///     Implements <see cref="ICommand.Execute" />
        /// </summary>
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

        /// <summary>
        ///     Implements <see cref="ICommand.CanExecuteChanged" />
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}