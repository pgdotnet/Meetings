using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RxUiSample
{
    public class UdpCommunicationChannel<T> : ICommunicationChannel<T>
    {
        private readonly IUdpClientServer _udpClientServer;
        private readonly ServerConfig _serverConfig;
        
        public UdpCommunicationChannel(IUdpClientServer udpClientServer, ServerConfig serverConfig)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("Given type must be serializable!", "T");

            _udpClientServer = udpClientServer;
            _serverConfig = serverConfig;
        }

        public IObservable<T> MessageStream
        {
            get { return _udpClientServer.Listen(_serverConfig.Port).Select(bytes => Deserialize(bytes)); }
        }

        public IObservable<int> SendMessage(T message)
        {
            return _udpClientServer.Send(_serverConfig.Address, _serverConfig.Port, Serialize(message));
        }

        private byte[] Serialize(T data)
        {
            object isNull = data;
            if (isNull == null || data.Equals(default(T)))
                return new byte[0];

            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
            return stream.ToArray();
        }

        private T Deserialize(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return default(T);

            MemoryStream stream = new MemoryStream(bytes);
            BinaryFormatter formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(stream);
        }
    }
}
