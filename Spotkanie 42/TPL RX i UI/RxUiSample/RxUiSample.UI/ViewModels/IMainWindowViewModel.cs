using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RxUiSample.UI.ViewModels
{
    public interface IMainWindowViewModel
    {
        ICommand StartListeningCommand { get; }

        ICommand StopListeningCommand { get; }

        ICommand ShowNotificationCommand { get; }

        ICommand ShowConfirmationCommand { get; }

        ObservableCollection<string> OperationHistory { get; }

        ObservableCollection<Message> ServerMessages { get; }

        long MessageCount { get; }
    }
}
