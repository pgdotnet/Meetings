using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RxUiSample.UI.Services
{
    public class Interaction : IInteraction
    {
        public IObservable<Unit> ShowNotification(string message, string title = null)
        {
            return Observable.Start(() => { MessageBox.Show(message, title ?? "Message"); return Unit.Default; });
        }

        public IObservable<bool> ShowConfirmation(string message, string title = null)
        {
            return Observable.Start(() => { return MessageBox.Show(message, title ?? "Message", MessageBoxButton.YesNo) == MessageBoxResult.Yes; });
        }
    }
}
