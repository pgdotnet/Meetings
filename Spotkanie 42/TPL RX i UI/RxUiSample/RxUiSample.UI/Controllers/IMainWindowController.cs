using RxUiSample.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace RxUiSample.UI.Controllers
{
    public interface IMainWindowController
    {
        IMainWindowViewModel ViewModel { get; set; }

        IObservable<Unit> CommandsUpdateSuggested { get; }

        bool CanStartListening();

        bool CanStopListening();

        void OnShowConfirmation();

        void OnShowNotification();

        void OnStartListening();

        void OnStopListening();
    }
}
