using System;
using System.Drawing;
using System.Windows.Forms;
using static HiChat.Buisness.HiChat;

namespace HiChat.GUI
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#f5f5f5");
            btnLogin.BackColor = ColorTranslator.FromHtml("#1aad19");
            lblUser.ForeColor = ColorTranslator.FromHtml("#878787");
            lblPassword.ForeColor = ColorTranslator.FromHtml("#878787");
            if (!TestConnection())
            {
                MessageBox.Show("NO Connection");
            }
            pictureBox1.Location = new Point((this.Width - pictureBox1.Width) / 2, pictureBox1.Location.Y);
            lblUser.Location = new Point((this.Width - lblUser.Width) / 2, lblUser.Location.Y);
            lblPassword.Location = new Point((this.Width - lblPassword.Width) / 2, lblPassword.Location.Y);
            label1.Location = new Point((this.Width - label1.Width) / 2, label1.Location.Y);
            txtUserName.Location = new Point((this.Width - txtUserName.Width) / 2, txtUserName.Location.Y);
            txtPassword.Location = new Point((this.Width - txtPassword.Width) / 2, txtPassword.Location.Y);
            btnLogin.Location = new Point((this.Width - btnLogin.Width) / 2, btnLogin.Location.Y);
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;
            if (Login(username, password))
            {
                txtPassword.Clear();
                txtUserName.Clear();
                FormMenu myform = new FormMenu(username);
                myform.FormClosed += Myform_FormClosed;
                myform.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Login Failed", "Login Failed");
            }
        }

        private void Myform_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((FormMenu)sender).Dispose();
            this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Register myreg = new Register();
            myreg.FormClosed += (object s, FormClosedEventArgs ev) =>
            {
                this.Show();
            };
            myreg.Show();
            this.Hide();
        }
    }
}
