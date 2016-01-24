using System.ComponentModel;
using System.Runtime.CompilerServices;
using TestExtractor.Client.ExtractorUi.Interfaces;

namespace TestExtractor.Client.ExtractorUi.ViewModel
{
    /// <summary>
    ///     Abstract Implementation of a ViewModel Base
    ///     Implements Interface : <see cref="IViewModel" />
    /// </summary>
    public abstract class ViewModel : IViewModel
    {
        /// <summary>
        ///     Implements <see cref="INotifyPropertyChanged.PropertyChanged" />
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     On Property Changed function to be called when a Property Value changes
        /// </summary>
        /// <param name="propertyName">
        ///     Name of Property.
        ///     Uses <see cref="System.Runtime.CompilerServices.CallerMemberNameAttribute" /> to find the property name
        /// </param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                return;
            }
            var handler = PropertyChanged;
            if (handler == null)
            {
                return;
            }
            handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}