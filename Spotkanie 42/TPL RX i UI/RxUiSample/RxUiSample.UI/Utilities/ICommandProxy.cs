using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace RxUiSample.UI.Utilities
{
    public interface ICommandProxy
    {
        ICommand GetOrCreate(Expression<Func<ICommand>> command, Action execute, Func<bool> canExecute = null);

        ICommand GetOrCreate(string commandName, Action execute, Func<bool> canExecute = null);

        ICommand GetOrCreate<T>(Expression<Func<ICommand>> command, Action<T> execute, Func<T, bool> canExecute = null);

        ICommand GetOrCreate<T>(string commandName, Action<T> execute, Func<T, bool> canExecute = null);

        void RaiseCanExecuteChanged();

        ICommand this[Expression<Func<ICommand>> command] { get; }

        ICommand this[string commandName] { get; }
    }
}
