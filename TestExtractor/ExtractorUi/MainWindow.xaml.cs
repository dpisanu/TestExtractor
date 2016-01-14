using System;
using System.Collections.Generic;
using System.Linq;
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

        public MainWindow ()
        {
            InitializeComponent();
            _extractor = ExtractFactory.Extractor<TestExtractor.Extractors.NUnit.Extractor.NUnit>();
            _fileDialog = new OpenFileDialog { Multiselect = true };
            _files = new List<string>();
        }

        private void LoadFilesButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            _fileDialog.ShowDialog();
            
            _files.AddRange(_fileDialog.FileNames);
            AmountOfFileLabel.Content = _files.Count();
        }

        private void ExtractButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ResultRichTextBox.Document.Blocks.Clear();
            ResultRichTextBox.AppendText("Starting Extraction Process");
            ResultRichTextBox.AppendText(Environment.NewLine);
            
            if (SuitsToggle.IsChecked == true)
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
            foreach (var node in extractedNodes.Item1)
            {
                var msg = string.Format("Extracted Suite '{0}' containing '{1}' Stubs", node.NodeName.FullName, node.StubCount);
                ResultRichTextBox.AppendText(msg);
                ResultRichTextBox.AppendText(Environment.NewLine);
            }
        }

        private void ExtractedStubs(Tuple<IList<IStubNode>, TimeSpan> extractedNodes)
        {
            LogTime("Extraction took {0} ms", extractedNodes.Item2);
            foreach (var node in extractedNodes.Item1)
            {
                var msg = string.Format("Extracted Stub '{0}' from Suite '{1}' ", node.NodeName.FullName, node.ParentFullName);
                ResultRichTextBox.AppendText(msg);
                ResultRichTextBox.AppendText(Environment.NewLine);
            }
        }

        private void LogTime(string message, TimeSpan timeSpan)
        {
            var msg = string.Format(message, timeSpan.Milliseconds);
            ResultRichTextBox.AppendText(msg);
            ResultRichTextBox.AppendText(Environment.NewLine);
        }
    }
}