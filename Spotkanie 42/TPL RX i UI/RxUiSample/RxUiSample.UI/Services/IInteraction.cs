using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RxUiSample.UI.Services
{
    public interface IInteraction
    {
        IObservable<Unit> ShowNotification(string message, string title = null);

        IObservable<bool> ShowConfirmation(string message, string title = null);
    }
}
