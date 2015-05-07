using Microsoft.Practices.Prism.ViewModel;
using RxUiSample.UI.Controllers;
using RxUiSample.UI.Utilities;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reactive.Linq;
using System.Windows.Input;

namespace RxUiSample.UI.ViewModels
{
    public class MainWindowViewModel : NotificationObject, IMainWindowViewModel
    {
        private readonly IMainWindowController _controller;
        private readonly ICommandProxy _commandProxy;

        public ICommand StartListeningCommand
        {
            get { return _commandProxy.GetOrCreate(() => StartListeningCommand, _controller.OnStartListening, _controller.CanStartListening); }
        }

        public ICommand StopListeningCommand
        {
            get { return _commandProxy.GetOrCreate(() => StopListeningCommand, _controller.OnStopListening, _controller.CanStopListening); }
        }

        public ICommand ShowNotificationCommand
        {
            get { return _commandProxy.GetOrCreate(() => ShowNotificationCommand, _controller.OnShowNotification); }
        }

        public ICommand ShowConfirmationCommand
        {
            get { return _commandProxy.GetOrCreate(() => ShowConfirmationCommand, _controller.OnShowConfirmation); }
        }

        private ObservableCollection<string> _operationHistory;
        public ObservableCollection<string> OperationHistory
        {
            get { return _operationHistory ?? (_operationHistory = new ObservableCollection<string>()); }
        }

        private ObservableCollection<Message> _serverMessages;
        public ObservableCollection<Message> ServerMessages
        {
            get { return _serverMessages ?? (_serverMessages = new ObservableCollection<Message>()); }
        }

        private long _messageCount;
        public long MessageCount
        {
            get { return _messageCount; }
            set
            {
                _messageCount = value;
                RaisePropertyChanged(() => MessageCount);
            }
        }

        public MainWindowViewModel(IMainWindowController controller, ICommandProxy commandProxy)
        {
            _controller = controller;
            _commandProxy = commandProxy;

            _controller.ViewModel = this;
            _controller.CommandsUpdateSuggested.Subscribe(_ => _commandProxy.RaiseCanExecuteChanged());

            Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
                h => ServerMessages.CollectionChanged += h,
                h => ServerMessages.CollectionChanged -= h)
                .Subscribe(_ => MessageCount = ServerMessages.Count);
        }
    }
}
