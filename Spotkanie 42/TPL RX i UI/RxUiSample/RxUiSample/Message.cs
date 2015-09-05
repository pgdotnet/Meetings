using System;

namespace RxUiSample
{
	[Serializable]
	public class Message
	{
		public Message(long id, string content)
		{
			Id = id;
			Content = content;
			Length = content.Length;
		}

		public long Id { get; private set; }

		public string Content { get; private set; }

		public int Length { get; private set; }

		public override string ToString()
		{
			return String.Format("{0}: ({1}/{2}) {3}", Id, Content.Length, Length, Content);
		}
	}
}