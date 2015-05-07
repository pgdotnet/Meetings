using Microsoft.Practices.Unity;
using RxUiSample.UI.Services;
using RxUiSample.UI.ViewModels;
using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxUiSample.UI.Controllers
{
    public class MainWindowController : IMainWindowController
    {
        private readonly IUnityContainer _container;

        private IDisposable _subscription;

        public IMainWindowViewModel ViewModel { get; set; }

        private Subject<Unit> _commandUpdateRequestedSubject = new Subject<Unit>();
        public IObservable<Unit> CommandsUpdateSuggested { get { return _commandUpdateRequestedSubject.AsObservable(); } }

        public MainWindowController(IUnityContainer container)
        {
            _container = container;
        }

        public bool CanStartListening()
        {
            return ViewModel != null && _subscription == null;
        }

        public void OnStartListening()
        {
            _subscription = _container.Resolve<ICommunicationChannel<Message>>()
                .MessageStream
                .SubscribeOn(TaskPoolScheduler.Default)
                .ObserveOnDispatcher()
                .Subscribe(message => ViewModel.ServerMessages.Add(message));

            _commandUpdateRequestedSubject.OnNext(Unit.Default);
        }

        public bool CanStopListening()
        {
            return _subscription != null;
        }

        public void OnStopListening()
        {
            _subscription.Dispose();
            _subscription = null;

            _commandUpdateRequestedSubject.OnNext(Unit.Default);
        }

        public void OnShowNotification()
        {
            _container.Resolve<IInteraction>()
                .ShowNotification("Hello world!")
                .ObserveOnDispatcher()
                .Subscribe(_ => ViewModel.OperationHistory.Add("World welcomes you, too!"));
        }

        public void OnShowConfirmation()
        {
            _container.Resolve<IInteraction>()
                .ShowConfirmation("Are you ready to rule the world?")
                .SubscribeOn(TaskPoolScheduler.Default)
                .ObserveOnDispatcher()
                .Subscribe(result => ViewModel.OperationHistory.Add(result ? "The world is yours!" : "Come back when ready..."));
        }
    }
}
