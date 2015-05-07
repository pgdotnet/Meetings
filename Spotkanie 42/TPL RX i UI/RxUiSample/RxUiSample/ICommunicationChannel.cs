using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxUiSample
{
    public interface ICommunicationChannel<T>
    {
        IObservable<T> MessageStream { get; }

        IObservable<int> SendMessage(T message);
    }
}
