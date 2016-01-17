using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ExtractorUi.Commands
{
    /// <summary>
    /// A Relaying Command construct as described in the Microsoft MVVM Documentation 
    /// </summary>
    /// <seealso cref="https://msdn.microsoft.com/en-us/magazine/dn237302.aspx"/>
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecutePredicate;
        private readonly Action<object> _executeAction;
        private EventHandler _canExecuteChangedEventHandler;

        public RelayCommand (Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {  throw new ArgumentNullException("Execute Action is not allowed to be NULL");}

            if (canExecute == null)
            { throw new ArgumentNullException("CanExecute Predicate is not allowed to be NULL"); }

            _executeAction = execute;
            _canExecutePredicate = canExecute;
        }
        
        /// <summary>
        /// An event that is fired when CanExecute changes
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                _canExecuteChangedEventHandler += value;
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecuteChangedEventHandler != null)
                {
                    _canExecuteChangedEventHandler -= value;
                }
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute (object parameter)
        {
            if (_canExecutePredicate == null)
            {
                return false;
            }

            return _canExecutePredicate == null || _canExecutePredicate(parameter);
        }

        public void Execute (object parameter)
        {
            _executeAction(parameter);
        }
    }
}