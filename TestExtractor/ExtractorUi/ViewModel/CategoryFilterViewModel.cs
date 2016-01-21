using TestExtractor.ExtractorUi.Interfaces;

namespace TestExtractor.ExtractorUi.ViewModel
{
    internal sealed class CategoryFilterViewModel : ViewModel, ICategoryFilterViewModel
    {
        private string _category;
        private bool _enabled;

        public CategoryFilterViewModel(string category)
        {
            Category = category;
            Enabled = true;
        }

        public string Category
        {
            get
            {
                return _category;
            }
            private set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }
    }
}