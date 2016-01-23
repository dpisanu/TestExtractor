using TestExtractor.ExtractorUi.Interfaces;
using TestExtractor.ExtractorUi.ViewModel;

namespace TestExtractor.ExtractorUi
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