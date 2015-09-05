using System;

namespace RxUiSample
{
	public interface ICommunicationChannel<T>
	{
		IObservable<T> MessageStream { get; }

		IObservable<int> SendMessage(T message);
	}
}