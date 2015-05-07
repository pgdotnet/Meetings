using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RxUiSample.UI.Utilities
{
    public class CommandProxy : ICommandProxy
    {
        private readonly Dictionary<string, DelegateCommandBase> _commands = new Dictionary<string, DelegateCommandBase>();

        public ICommand this[string commandName]
        {
            get { return _commands.ContainsKey(commandName) ? _commands[commandName] : null; }
        }

        public ICommand this[Expression<Func<ICommand>> command]
        {
            get
            {
                return this[PropertySupport.ExtractPropertyName(command)];
            }
        }

        public ICommand GetOrCreate<T>(string commandName, Action<T> execute, Func<T, bool> canExecute = null)
        {
            var command = this[commandName] as DelegateCommandBase;
            if (command != null)
                return command;

            command = (canExecute != null ? new DelegateCommand<T>(execute, canExecute) : new DelegateCommand<T>(execute));
            _commands.Add(commandName, command);
            return command;
        }

        public ICommand GetOrCreate<T>(Expression<Func<ICommand>> command, Action<T> execute, Func<T, bool> canExecute = null)
        {
            return GetOrCreate<T>(PropertySupport.ExtractPropertyName(command), execute, canExecute);
        }

        public ICommand GetOrCreate(string commandName, Action execute, Func<bool> canExecute = null)
        {
            var command = this[commandName] as DelegateCommandBase;
            if (command != null)
                return command;

            command = (canExecute != null ? new DelegateCommand(execute, canExecute) : new DelegateCommand(execute));
            _commands.Add(commandName, command);
            return command;
        }

        public ICommand GetOrCreate(Expression<Func<ICommand>> command, Action execute, Func<bool> canExecute = null)
        {
            return GetOrCreate(PropertySupport.ExtractPropertyName(command), execute, canExecute);
        }

        public void RaiseCanExecuteChanged()
        {
            foreach (var command in _commands.Values)
            {
                command.RaiseCanExecuteChanged();
            }
        }
    }
}
