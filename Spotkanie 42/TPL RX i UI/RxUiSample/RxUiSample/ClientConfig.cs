namespace RxUiSample
{
	public struct ClientConfig : IChannelConfig
	{
		public string Address { get; set; }

		public int Port { get; set; }
	}
}