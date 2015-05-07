using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxUiSample
{
    [Serializable]
    public class Message
    {
        public Message(long id, string content)
        {
            Id = id;
            Content = content;
        }

        public long Id { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Id, Content);
        }
    }
}
