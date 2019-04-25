using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HiChat;

namespace Hichat.GUI
{
    public partial class FormAddMember : Form
    {
        private List<string> listFriend;
        public Group group;
        public bool Result;
        public CheckedListBox Sheet;

        public FormAddMember(Group group, List<string> listFriend)
        {
            InitializeComponent();
            this.listFriend = listFriend;
            this.group = group;
            this.Text = "Add Friend to Group " + group.Alias;
        }

        private void FormAddMember_Load(object sender, EventArgs e)
        {
            groupBox1.Height = groupBox1.Parent.Height - checkedListBox1.Height;
            buttonCancel.Width = groupBox1.Width / 2;
            buttonConfirm.Width = groupBox1.Width / 2;
            ((ListBox)checkedListBox1).DataSource = listFriend;
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            Sheet = this.checkedListBox1;
            Result = true;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Result = false;
            this.Close();
        }
    }
}
