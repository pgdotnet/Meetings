using Castle.DynamicProxy;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AopWithDp.Interceptors
{
    public class UndoInterceptor: IInterceptor
    {
        private DelegateCommand _undoCommand;

        private class UndoFrame
        {
            public string Property { get; set; }
            public object Value { get; set; }
        }

        private readonly Stack<UndoFrame> _undoStack = new Stack<UndoFrame>();

        private object _proxy;

        /// <summary>
        /// to avoid snapshoting changes while performing undo
        /// </summary>
        private bool _undoInProgress = false; 

        public UndoInterceptor()
        {
            _undoCommand = new DelegateCommand(Undo, () => _undoStack.Count > 0);
        }

        public void Intercept(IInvocation invocation)
        {
            _proxy = invocation.Proxy; // to invoke undo on proxy target

            var name = invocation.Method.Name;
            if (name == "get_UndoCommand")
            {
                invocation.ReturnValue = _undoCommand;
                return;
            }

            if (name.StartsWith("set_"))
            {
                var propName = name.Substring(4);

                Snapshot(propName);

                invocation.Proceed();

                return;
            }

            invocation.Proceed();
        }

        private void Snapshot(string property)
        {
            if (_undoInProgress)
                return;

            var value = _proxy.GetType()
                .GetProperty(property, BindingFlags.Instance | BindingFlags.Public)
                .GetValue(_proxy);

            var frame = new UndoFrame
            {
                Property = property,
                Value = value
            };

            _undoStack.Push(frame);

            // to notify that stack is no longer empty
            _undoCommand.RaiseCanExecuteChanged();       
        }

        private void Undo()
        {
            if (_proxy == null)
                return; // should never happen

            if (_undoStack.Count == 0)
                return;

            _undoInProgress = true;

            var frame = _undoStack.Pop();

            _proxy.GetType()
                .GetProperty(frame.Property, BindingFlags.Instance | BindingFlags.Public)
                .SetValue(_proxy, frame.Value);

            // to notify that stack may be empty
            _undoCommand.RaiseCanExecuteChanged();            

            _undoInProgress = false;
        }

    }
}
