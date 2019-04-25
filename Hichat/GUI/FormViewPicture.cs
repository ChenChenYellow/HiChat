using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hichat.GUI
{
    public partial class FormViewPicture : Form
    {
        private Image image;
        private event EventHandler SaveImage_Click;
        public FormViewPicture()
        {
            InitializeComponent();
        }
        public void SetImage(Image i)
        {
            this.image = i;
            pictureBox1.Image = image;
        }
        private void FormViewPicture_Load(object sender, EventArgs e)
        {
            SaveImage_Click += FormViewPicture_SaveImage_Click;
        }

        private void FormViewPicture_SaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "*.jpg|*.jpg";
            s.FileName = "Image.jpg";
            if (s.ShowDialog() == DialogResult.OK)
            {
                image.Save(s.FileName);
            }
        }

        private void FormViewPicture_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add(new MenuItem("Save Image As", SaveImage_Click));
                cm.Show(((PictureBox)sender), e.Location);
            }
        }


            
    }
}
