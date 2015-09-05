using System;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Microsoft.Practices.Unity;
using RxUiSample.UI.Services;
using RxUiSample.UI.ViewModels;

namespace RxUiSample.UI.Controllers
{
	public class MainWindowController : IMainWindowController
	{
		private readonly IUnityContainer _container;

		private IDisposable _subscription;

		public IMainWindowViewModel ViewModel { get; set; }

		private readonly Subject<Unit> _commandUpdateRequestedSubject = new Subject<Unit>();

		public IObservable<Unit> CommandsUpdateSuggested
		{
			get { return _commandUpdateRequestedSubject.AsObservable(); }
		}

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
			_subscription = _container.Resolve<ICommunicationChannel<Message>>("server")
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
			_container.Resolve<ICommunicationChannel<Message>>("client").SendMessage(new Message(DateTime.Now.Ticks, "Hello World form Client!")).Subscribe();

			_container.Resolve<IInteraction>()
				.ShowNotification("Hello world!")
				.ObserveOnDispatcher()
				.Subscribe(_ => ViewModel.OperationHistory.Add("World welcomes you, too!"));
		}

		public void OnShowConfirmation()
		{
			_container.Resolve<ICommunicationChannel<Message>>("client").SendMessage(new Message(DateTime.Now.Ticks, "Asking if you want to rule the world, phhh...")).Subscribe();

			_container.Resolve<IInteraction>()
				.ShowConfirmation("Are you ready to rule the world?")
				.SubscribeOn(TaskPoolScheduler.Default)
				.ObserveOnDispatcher()
				.Subscribe(result => ViewModel.OperationHistory.Add(result ? "The world is yours!" : "Come back when ready..."));
		}
	}
}
