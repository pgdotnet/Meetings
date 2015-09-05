using System;
using System.IO;
using System.Reactive.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace RxUiSample
{
	public class UdpCommunicationChannel<T> : ICommunicationChannel<T>
	{
		private readonly IUdpClientServer _udpClientServer;
		private readonly IChannelConfig _channelConfig;

		public UdpCommunicationChannel(IUdpClientServer udpClientServer, IChannelConfig channelConfig)
		{
			if (!typeof(T).IsSerializable)
				throw new ArgumentException("Given type must be serializable!", "T");

			_udpClientServer = udpClientServer;
			_channelConfig = channelConfig;
		}

		public IObservable<T> MessageStream
		{
			get { return _udpClientServer.Listen(_channelConfig.Port).Select(bytes => Deserialize(bytes)); }
		}

		public IObservable<int> SendMessage(T message)
		{
			return _udpClientServer.Send(_channelConfig.Address, _channelConfig.Port, Serialize(message));
		}

		private byte[] Serialize(T data)
		{
			object isNull = data;
			if (isNull == null || data.Equals(default(T)))
				return new byte[0];

			using (MemoryStream stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, data);
				return stream.ToArray();
			}
		}

		private T Deserialize(byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0)
				return default(T);

			using (MemoryStream stream = new MemoryStream(bytes))
			{
				BinaryFormatter formatter = new BinaryFormatter();
				return (T)formatter.Deserialize(stream);
			}
		}
	}
}