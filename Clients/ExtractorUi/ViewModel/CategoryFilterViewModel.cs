using TestExtractor.Client.ExtractorUi.Interfaces;

namespace TestExtractor.Client.ExtractorUi.ViewModel
{
    /// <summary>
    ///     Concrete implementation of a Category Filter View Model
    ///     Inherrits Class : <see cref="ICategoryFilterViewModel" />
    ///     Implements Interface : <see cref="ViewModel" />
    /// </summary>
    internal sealed class CategoryFilterViewModel : Client.ExtractorUi.ViewModel.ViewModel, ICategoryFilterViewModel
    {
        private string _category;
        private bool _enabled;

        /// <summary>
        ///     Created a new instance of <see cref="CategoryFilterViewModel" />
        /// </summary>
        /// <param name="category">Category to represent</param>
        public CategoryFilterViewModel(string category)
        {
            Category = category;
            Enabled = true;
        }

        /// <summary>
        ///     Implements <see cref="ICategoryFilterViewModel.Category" />
        /// </summary>
        public string Category
        {
            get { return _category; }
            private set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Implements <see cref="ICategoryFilterViewModel.Enabled" />
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                OnPropertyChanged();
            }
        }
    }
}