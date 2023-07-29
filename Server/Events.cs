using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Server
{
    public class Event 
    {
        public Server server;
        public string eventName;

        public Event(Server server, string eventName)
        {
            this.server = server;
            this.eventName = eventName;
        }
    }

    public class ChatJoinEvent : Event
    {
        public string name;

        public ChatJoinEvent(Server server, string name) : base(server, "chatJoin")
        {
            this.name = name;
        }
    }

    public class SendMessageEvent : Event
    {
        public string name;
        public string text;

        public SendMessageEvent(Server server, string name, string text) : base(server, "sendMessage")
        {
            this.name = name;
            this.text = text;
        }
    }
}
