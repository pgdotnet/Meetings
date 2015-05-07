using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxUiSample
{
    public interface IUdpClientServer
    {
        IObservable<byte[]> Listen(int localPort);

        IObservable<int> Send(string remoteAddress, int port, byte[] data);
    }
}
