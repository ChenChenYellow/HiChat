using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiChat.Buisness
{
    public class User
    {
        private string username, password, picture;
        private List<string> list_of_friend, list_of_group;
        private List<Message> list_of_message;
        private List<Friend_Request> list_of_friend_request;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public List<Message> List_of_message
        {
            get
            {
                return list_of_message;
            }

            set
            {
                list_of_message = value;
            }
        }

        public List<Friend_Request> List_of_friend_request
        {
            get
            {
                return list_of_friend_request;
            }

            set
            {
                list_of_friend_request = value;
            }
        }

        public List<string> List_of_group
        {
            get
            {
                return list_of_group;
            }

            set
            {
                list_of_group = value;
            }
        }

        public List<string> List_of_friend
        {
            get
            {
                return list_of_friend;
            }

            set
            {
                list_of_friend = value;
            }
        }

        public string Picture
        {
            get
            {
                return picture;
            }

            set
            {
                picture = value;
            }
        }

        public User()
        {
        }
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
            this.picture = null;
            this.List_of_friend = new List<string>();
            this.list_of_message = new List<Message>();
            this.list_of_friend_request = new List<Friend_Request>();
            this.list_of_group = new List<string>();
        }
        public override string ToString()
        {
            string ret = "Username : " + username + "\nPassword : " + password + "\nList Of Friend : ";
            foreach (string item in List_of_friend)
            {
                ret += item.ToString();
            }
            ret += "\nList Of Message : ";
            foreach (Message item in list_of_message)
            {
                ret += item.ToString();
            }
            return ret;
        }
    }
}

