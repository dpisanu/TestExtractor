using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ExtractorUi.ViewModel;
using Microsoft.Win32;
using TestExtractor.Extractor;
using TestExtractor.Extractor.Extractor;
using TestExtractor.Structure;

namespace ExtractorUi
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IExtractor _extractor;
        private readonly OpenFileDialog _fileDialog;
        private readonly List<string> _files;
        private bool extractSuits = true;
        private INodeTypeFiltersViewModel _nodeTypeFiltersViewModel;

        public MainWindow ()
        {
            InitializeComponent();
            _extractor = ExtractFactory.Extractor<TestExtractor.Extractors.NUnit.Extractor.NUnit>();
            _fileDialog = new OpenFileDialog { Multiselect = true };
            _files = new List<string>();
            _nodeTypeFiltersViewModel = new NodeTypeFiltersViewModel(); 
        }

        private void LoadFilesButtonClick(object sender, RoutedEventArgs e)
        {
            _fileDialog.ShowDialog();
            
            _files.AddRange(_fileDialog.FileNames);
            AmountOfFileLabel.Content = _files.Count();
        }

        private void ExtractButtonClick(object sender, RoutedEventArgs e)
        {
            ResultRichTextBox.Document.Blocks.Clear();
            ResultRichTextBox.AppendText("Starting Extraction Process");
            ResultRichTextBox.AppendText(Environment.NewLine);

            if (RadioButtonSuits.IsChecked == true)
            {
                ExtractedSuits(_extractor.ExtractTimed<ISuiteNode>(_files));
            }
            else
            {
                ExtractedStubs(_extractor.ExtractTimed<IStubNode>(_files));
            }
        }

        private void ExtractedSuits(Tuple<IList<ISuiteNode>, TimeSpan> extractedNodes)
        {
            LogTime("Extraction took {0} ms", extractedNodes.Item2);
            DataContext = extractedNodes.Item1;
        }

        private void ExtractedStubs(Tuple<IList<IStubNode>, TimeSpan> extractedNodes)
        {
            LogTime("Extraction took {0} ms", extractedNodes.Item2);
            DataContext = extractedNodes.Item1;
        }

        private void LogTime(string message, TimeSpan timeSpan)
        {
            var msg = string.Format(message, timeSpan.Milliseconds);
            ResultRichTextBox.AppendText(msg);
            ResultRichTextBox.AppendText(Environment.NewLine);
        }

        private void TestSuiteToggled(object sender, RoutedEventArgs e)
        {
            if (sender == RadioButtonSuits)
            {
                
            }
        }
    }
}