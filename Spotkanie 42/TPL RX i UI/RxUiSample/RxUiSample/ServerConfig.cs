﻿namespace RxUiSample
{
	public struct ServerConfig : IChannelConfig
	{
		public string Address { get; set; }

		public int Port { get; set; }
	}
}