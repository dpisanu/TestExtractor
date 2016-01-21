using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using TestExtractor.ExtractorUi.ViewModel;

namespace TestExtractor.ExtractorUi.Commands
{
    internal class ExportCommand : ICommand
    {
        private readonly SaveFileDialog _fileDialog;
        private readonly MainWindowViewModel _mainWindowViewModel;

        internal ExportCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _fileDialog = new SaveFileDialog();
        }

        public bool CanExecute(object parameter)
        {
            if (_mainWindowViewModel == null)
            {
                return false;
            }

            if (_fileDialog == null)
            {
                return false;
            }

            return _mainWindowViewModel.ExtractedData.Any();
        }

        public void Execute(object parameter)
        {
            int packageSize;
            if (!ParseNumber(parameter, out packageSize))
            {
                return;
            }

            _fileDialog.ShowDialog();

            if (string.IsNullOrEmpty(_fileDialog.SafeFileName))
            {
                return;
            }
            var directoryName = Path.GetDirectoryName(_fileDialog.FileName);
            if (string.IsNullOrEmpty(directoryName))
            {
                return;
            }

            var blocks = Split.Split.SplitByPackageSize(_mainWindowViewModel.ExtractedData, packageSize);

            var counter = 0;
            foreach (var block in blocks)
            {
                counter++;
                var fileName = directoryName + "\\" + _fileDialog.SafeFileName + counter;

                // Example #1: Write an array of strings to a file.
                // Create a string array that consists of three lines.
                var lines = block.Select(node => node.NodeName.FullName).ToList();

                // WriteAllLines creates a file, writes a collection of strings to the file,
                // and then closes the file.  You do NOT need to call Flush() or Close().
                File.WriteAllLines(fileName, lines);
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private bool ParseNumber(object parameter, out int packageSize)
        {
            packageSize = 0;
            bool parseWorked;
            try
            {
                parseWorked = int.TryParse(Convert.ToString(parameter, CultureInfo.InvariantCulture), out packageSize);
            }
            catch
            {
                parseWorked = false;
            }
            return parseWorked;
        }
    }
}