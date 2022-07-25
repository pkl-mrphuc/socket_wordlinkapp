using System;

namespace WordLinkApp.SocketModel
{
    public class MessageData
    {
        public string Data { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int Key { get; set; }
    }
}
