using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hichat
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#f5f5f5");
            btnGo.BackColor = ColorTranslator.FromHtml("#1aad19");
            lblUser.ForeColor = ColorTranslator.FromHtml("#878787");
            lblPassword.ForeColor = ColorTranslator.FromHtml("#878787");
            lblprofile.ForeColor = ColorTranslator.FromHtml("#878787");
            btnGo.Click += btnGo_Click;
            btnFindPic.Click += btnFindPic_Click;
            picboxProfile.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            if (picboxProfile.Image == null)
            {
                MessageBox.Show("Pick a Image");
            }
            else
            {
                string imgArr = HiChat.HiChat.SerializeImage(picboxProfile.Image, HiChat.HiChat.GetImFormat(picboxProfile.Image));
                if (HiChat.HiChat.Register(txtUserName.Text, txtPassword.Text))
                {
                    HiChat.HiChat.SetProfileImage(txtUserName.Text, imgArr);
                    MessageBox.Show("Register Succesfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Register Unsuccesfully.");
                    this.Close();
                }
            }
        }

        private void btnFindPic_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                picboxProfile.Image = new Bitmap(fd.FileName);
                // image file path  
                picboxProfile.Text = fd.FileName;
            }
        }
    }
}
