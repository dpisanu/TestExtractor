using TestExtractor.Client.ExtractorUi.Interfaces;
using TestExtractor.Client.ExtractorUi.ViewModel;

namespace TestExtractor.Client.ExtractorUi
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IMainWindowViewModel _mainWindowViewModel;

        public MainWindow()
        {
            _mainWindowViewModel = new MainWindowViewModel();
            DataContext = _mainWindowViewModel;

            InitializeComponent();
        }
    }
}