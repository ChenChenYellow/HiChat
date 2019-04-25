using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiChat.Buisness
{
    public class Friend_Request
    {
        private string sender, receiver;
        private DateTime request_date;
        public Friend_Request(string sender, string receiver, DateTime request_date)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.request_date = request_date;
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

        public DateTime Request_date
        {
            get
            {
                return request_date;
            }

            set
            {
                request_date = value;
            }
        }
    }
}
