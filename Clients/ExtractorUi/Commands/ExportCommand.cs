using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using TestExtractor.Client.ExtractorUi.ViewModel;

namespace TestExtractor.Client.ExtractorUi.Commands
{
    /// <summary>
    ///     Command Class that handles the Exporting the List to files
    ///     Implements Interface : <see cref="ICommand" />
    /// </summary>
    internal class ExportCommand : ICommand
    {
        private readonly SaveFileDialog _fileDialog;
        private readonly MainWindowViewModel _mainWindowViewModel;

        /// <summary>
        ///     Created a new instance of <see cref="ExportCommand" />
        /// </summary>
        /// <param name="mainWindowViewModel"><see cref="MainWindowViewModel"/> to work on</param>
        internal ExportCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _fileDialog = new SaveFileDialog();
        }

        /// <summary>
        ///     Implements <see cref="ICommand.CanExecute" />
        /// </summary>
        public bool CanExecute(object parameter)
        {
            if (_mainWindowViewModel == null || _fileDialog == null)
            {
                return false;
            }

            return _mainWindowViewModel.ExtractedData.Any();
        }

        /// <summary>
        ///     Implements <see cref="ICommand.Execute" />
        /// </summary>
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

        /// <summary>
        ///     Implements <see cref="ICommand.CanExecuteChanged" />
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        ///     Unbox an object to a number
        /// </summary>
        /// <param name="parameter">Object parameter</param>
        /// <param name="number">Number as an out parameter</param>
        /// <returns><c>true</c> if unboxing to number worked. <c>false</c> if it fails</returns>
        private static bool ParseNumber(object parameter, out int number)
        {
            number = 0;
            bool parseWorked;
            try
            {
                parseWorked = int.TryParse(Convert.ToString(parameter, CultureInfo.InvariantCulture), out number);
            }
            catch
            {
                parseWorked = false;
            }
            return parseWorked;
        }
    }
}