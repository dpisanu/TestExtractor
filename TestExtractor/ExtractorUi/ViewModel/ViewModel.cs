using System.ComponentModel;
using System.Runtime.CompilerServices;
using ExtractorUi.Interfaces;

namespace ExtractorUi.ViewModel
{
    public abstract class ViewModel : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

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