using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiChat
{
    public class Group
    {
        private List<string> members;
        private string alias;
        private string id;
        public Group()
        {

        }
        public Group(List<string> members, string id, string alias)
        {
            this.members = members;
            this.alias = alias;
            this.id = id;
        }
        public List<string> Members
        {
            get
            {
                return members;
            }

            set
            {
                members = value;
            }
        }

        public string Alias
        {
            get
            {
                return alias;
            }

            set
            {
                alias = value;
            }
        }

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public override string ToString()
        {
            return this.alias;
        }
    }
}
