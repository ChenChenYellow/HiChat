using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiChat
{
    public class Message
    {
        private string sender, receiver, content, type;
        private DateTime message_date;
        private bool groupMessage;

        public Message(string sender, string receiver, string content, DateTime message_date, string type, bool groupMessage)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.content = content;
            this.message_date = message_date;
            this.type = type;
            this.GroupMessage = groupMessage;
        }
        public Message()
        {

        }
        
        public string Sender
        {
            get
            {
                return sender;
            }

            set
            {
                sender = value;
            }
        }

        public string Receiver
        {
            get
            {
                return receiver;
            }

            set
            {
                receiver = value;
            }
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        public DateTime Message_date
        {
            get
            {
                return message_date;
            }

            set
            {
                message_date = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public bool GroupMessage
        {
            get
            {
                return groupMessage;
            }

            set
            {
                groupMessage = value;
            }
        }

        public override string ToString()
        {
            
            return "Sender : " + sender + "\nReceiver : " + receiver + "\nType : " + type + "\nMessage_date : " + message_date.ToString();
        }
    }
}
