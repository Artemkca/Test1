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

        public Event() { }

        public Event(Server server)
        {
            this.server = server;
        }
    }

    public class ChatJoinEvent : Event
    {
        public string name;

        public ChatJoinEvent(Server server, string name) : base(server)
        {
            this.name = name;
        }
    }

    public class ChatMessageEvent : Event
    {
        public string name;
        public string text;

        public ChatMessageEvent(Server server, string name, string text) : base(server)
        {
            this.name = name;
            this.text = text;
        }
    }
}
