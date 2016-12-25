using System;
using System.Windows.Input;

namespace SysinternalsToolsDownloader.Utilities
{
    /// <summary>
    ///     The below implementation is a common one that invokes delegates for the Execute and CanExecute methods.
    ///     If you are using Prism, the framework for building composite WPF and Silverlight applications from the Microsoft
    ///     Patterns and Practices Team,
    ///     there is a similar Microsoft.Practices.Prism.Commands.DelegateCommand class available.
    /// </summary>
    /// <see
    ///     ref="https://social.technet.microsoft.com/wiki/contents/articles/18199.event-handling-in-an-mvvm-wpf-application.aspx" />
    /// <typeparam name="T">Type</typeparam>
    public class DelegateCommand<T> : ICommand where T : class
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;

        public DelegateCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public DelegateCommand(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute((T) parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T) parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}