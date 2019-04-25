using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using Microsoft.VisualBasic;
using HiChat;
using Hichat.GUI;
namespace Hichat
{
    public partial class FormMenu : Form
    {
        private string username;
        private List<string> friendList;
        private List<Group> groupList;
        private List<HiChat.Message> messages;
        private Timer timer1;
        private int count;
        private Image myImage;
        private static FormViewPicture formViewPicture;
        private static FormAddMember formAddMember;
        private static readonly string profileImagePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + @"/App_data/Profile_Image/";
        private static readonly string defaultProfileImagePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + @"/Pic/default_image.jpg";
        private event EventHandler DeleteFriend_Click;
        private event EventHandler CreateGroup_Click, QuitGroup_Click, AddGroupMember_Click;

        private static ToolTip toolTip = new ToolTip();

        public FormMenu(string username)
        {
            this.username = username;
            InitializeComponent();
        }
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 3000; // in miliseconds
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            HiChat.HiChat.DownloadMessage(username);
            if (tabControlFriendGroup.SelectedIndex == 0 && lstFriend.SelectedIndex > -1)
            {
                updateGrid();
            }
            else if (tabControlFriendGroup.SelectedIndex == 1 && lstGroup.SelectedIndex > -1)
            {
                updateGroupGrid();
            }

            List<Friend_Request> frientRequests = HiChat.HiChat.GetFriend_Request(username);
            HiChat.HiChat.DeleteFriendRequest(username);
            foreach (Friend_Request fr in frientRequests)
            {
                if (MessageBox.Show(fr.Sender + " wants to be friend with you, Accept?", "Friend Request", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    HiChat.HiChat.AddFriend(username, fr.Sender);
                    friendList = HiChat.HiChat.GetFriend(username);
                    lstFriend.DataSource = friendList;
                }
            }
        }
        private void FormMenu_Load(object sender, EventArgs e)
        {
            HiChat.HiChat.CreateOrOpenUserDataBase(username);
            string myContent = HiChat.HiChat.GetProgileImage(username);
            if (!string.IsNullOrEmpty(myContent))
            {
                HiChat.HiChat.SaveImage(myContent, profileImagePath + username + ".jpeg");
                myImage = HiChat.HiChat.CopyImage(profileImagePath + username + ".jpeg");
            }
            else
            {
                myImage = HiChat.HiChat.CopyImage(defaultProfileImagePath);
            }
            friendList = HiChat.HiChat.GetFriend(username);
            foreach (string item in friendList)
            {
                string content = HiChat.HiChat.GetProgileImage(item);
                if (content != null)
                {
                    HiChat.HiChat.SaveImage(content, profileImagePath + item + ".jpeg");
                }
            }
            lstFriend.DataSource = friendList;

            groupList = HiChat.HiChat.GetGroupByID(HiChat.HiChat.GetGroupID(username));
            lstGroup.DataSource = groupList;

            InitTimer();

            formViewPicture = new FormViewPicture();
            this.Size = this.MinimumSize;
            this.Text = "HiChat-" + username;
            panelMessage.HorizontalScroll.Maximum = 0;
            panelMessage.HorizontalScroll.Visible = false;
            DeleteFriend_Click += FormMenu_DeleteFriend_Click;
            CreateGroup_Click += FormMenu_CreateGroup_Click;
            QuitGroup_Click += FormMenu_QuitGroup_Click;
            AddGroupMember_Click += FormMenu_AddGroupMember_Click;
            groupBox2.Visible = false;
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 200;
            toolTip.ShowAlways = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tabControlFriendGroup.SelectedIndex == 0)
            {
                HiChat.HiChat.AddMessageToLocal(HiChat.HiChat.SendMessage(username, lstFriend.SelectedItem.ToString(), txtSend.Text.ToString()));
                txtSend.Clear();

            }
            else
            {
                HiChat.HiChat.AddMessageToLocal(HiChat.HiChat.SendMessageToGroup(username, ((Group)lstGroup.SelectedItem).Id.ToString(), txtSend.Text.ToString()));
                txtSend.Clear();
            }
        }

        private void lstFriend_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSend.Text = "";
            groupBox2.Visible = true;
            if (lstFriend.SelectedIndex > -1)
            {
                initGrid();
            }
        }
        private void lstGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSend.Text = "";
            groupBox2.Visible = true;
            if (lstGroup.SelectedIndex > -1)
            {
                initGroupGrid();
            }
        }

        private void initGroupGrid()
        {
            panelMessage.Controls.Clear();
            messages = HiChat.HiChat.GetMessageOfGroup(((Group)lstGroup.SelectedItem).Id);
            messages.Sort((x, y) => x.Message_date.CompareTo(y.Message_date));
            count = 0;
            loadGroupControls(messages);
        }
        private void loadGroupControls(List<HiChat.Message> mes)
        {
            int margin = 10;
            foreach (HiChat.Message m in mes)
            {
                Image friend_Image = HiChat.HiChat.CopyImage(defaultProfileImagePath);
                if (File.Exists(profileImagePath + m.Sender.ToString() + ".jpeg"))
                {
                    friend_Image = HiChat.HiChat.CopyImage(profileImagePath + m.Sender.ToString() + ".jpeg");
                }

                PictureBox p = new PictureBox();
                p.Height = 40;
                p.Width = 40;
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                Control content = new Control();
                if (m.Type.Equals("Text"))
                {
                    RichTextBox txt = new RichTextBox();
                    txt.Text = m.Content;
                    Size size = TextRenderer.MeasureText(txt.Text, this.Font);
                    txt.MaximumSize = new Size(300, 3000);
                    txt.Width = Math.Min(size.Width, 300);
                    try
                    {
                        txt.Height = Math.Max(((size.Width / txt.Width) + 1) * this.Font.Height, size.Height);
                    }
                    catch (Exception)
                    {
                        txt.Height = this.Font.Height;
                    }
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;
                    content = txt;
                }
                else
                {
                    string ext = Path.GetExtension(m.Type);
                    switch (ext)
                    {
                        case ".jpg":
                        case ".png":
                        case ".jpeg":
                            PictureBox picture = new PictureBox();
                            picture.Image = HiChat.HiChat.CopyImage(m.Content);
                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                            picture.MinimumSize = new Size(50, 50);
                            picture.MaximumSize = new Size(300, 300);
                            picture.DoubleClick += Picture_DoubleClick;
                            content = picture;
                            break;
                        default:
                            Button button = new Button();
                            button.FlatStyle = FlatStyle.Flat;
                            button.Width = 300;
                            button.Text = " A " + ext + " File HiChat Cannot Open, \nClick to Save To somewhere else";
                            button.Height = 4 * this.Font.Height;
                            button.Click += (object sender, EventArgs e) =>
                            {
                                SaveFileDialog sf = new SaveFileDialog();
                                sf.FileName = m.Type;
                                sf.Filter = "*.*|*.*";
                                if (sf.ShowDialog() == DialogResult.OK)
                                {
                                    if (File.Exists(sf.FileName))
                                    {
                                        if (MessageBox.Show("File Exist, Overwrite?", "File Exist", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            File.Copy(HiChat.HiChat.appDataImagePath + m.Type, sf.FileName, true);
                                        }
                                    }
                                    else
                                    {
                                        File.Copy(HiChat.HiChat.appDataImagePath + m.Type, sf.FileName, true);
                                    }
                                }
                            };
                            content = button;
                            break;
                    }
                }
                if (m.Sender.Equals(username))
                {
                    p.Image = myImage;
                    toolTip.SetToolTip(p, username);
                    panelMessage.SizeChanged += (object sender, EventArgs e) =>
                    {
                        p.Location = new Point(panelMessage.Width - p.Width - margin - margin, p.Location.Y);
                        content.Location = new Point(p.Location.X - content.Width - margin, content.Location.Y);
                    };
                    p.Location = new Point(panelMessage.Width - p.Width - margin - margin, count - panelMessage.VerticalScroll.Value);
                    content.Location = new Point(p.Location.X - content.Width - margin, count - panelMessage.VerticalScroll.Value);
                }
                else
                {
                    p.Image = friend_Image;
                    toolTip.SetToolTip(p, m.Sender);
                    p.Location = new Point(margin + margin, count - panelMessage.VerticalScroll.Value);
                    content.Location = new Point(p.Location.X + p.Width + margin, count - panelMessage.VerticalScroll.Value);
                }
                if (content.Height > p.Height)
                {
                    count += content.Height + margin;
                }
                else
                {
                    count += p.Height + margin;
                }
                panelMessage.Controls.Add(p);
                panelMessage.Controls.Add(content);
            }
        }
        private void updateGroupGrid()
        {
            List<HiChat.Message> newMessages = HiChat.HiChat.GetMessageOfGroup(((Group)lstGroup.SelectedItem).Id);
            int oldMessages = messages.Count;
            newMessages.Sort((x, y) => x.Message_date.CompareTo(y.Message_date));

            if (newMessages.Count > 0 && newMessages.Count > messages.Count)
            {
                newMessages.RemoveRange(0, oldMessages);
                loadGroupControls(newMessages);
                messages.AddRange(newMessages);
            }
        }

        private void initGrid()
        {
            panelMessage.Controls.Clear();
            messages = HiChat.HiChat.GetMessageOfUser(lstFriend.SelectedItem.ToString());
            messages.Sort((x, y) => x.Message_date.CompareTo(y.Message_date));
            count = 0;
            loadControls(messages);
        }
        private void loadControls(List<HiChat.Message> mes)
        {
            int margin = 10;
            Image friend_Image = HiChat.HiChat.CopyImage(defaultProfileImagePath);
            if (File.Exists(profileImagePath + lstFriend.SelectedItem.ToString() + ".jpeg"))
            {
                friend_Image = HiChat.HiChat.CopyImage(profileImagePath + lstFriend.SelectedItem.ToString() + ".jpeg");
            }
            foreach (HiChat.Message m in mes)
            {
                PictureBox p = new PictureBox();
                p.Height = 40;
                p.Width = 40;
                p.SizeMode = PictureBoxSizeMode.StretchImage;

                Control content = new Control();
                if (m.Type.Equals("Text"))
                {
                    RichTextBox txt = new RichTextBox();
                    txt.Text = m.Content;

                    Size size = TextRenderer.MeasureText(txt.Text, this.Font);
                    txt.MaximumSize = new Size(300, 3000);
                    txt.Width = Math.Min(size.Width, 300);
                    try
                    {
                        txt.Height = Math.Max(((size.Width / txt.Width) + 1) * this.Font.Height, size.Height);

                    }
                    catch (Exception)
                    {
                        txt.Height = this.Font.Height;
                    }
                    txt.ReadOnly = true;
                    txt.BorderStyle = BorderStyle.None;

                    content = txt;
                }
                else
                {
                    string ext = Path.GetExtension(m.Type);
                    switch (ext)
                    {
                        case ".jpg":
                        case ".png":
                        case ".jpeg":
                            PictureBox picture = new PictureBox();
                            picture.Image = HiChat.HiChat.CopyImage(m.Content);
                            picture.SizeMode = PictureBoxSizeMode.Zoom;
                            picture.MinimumSize = new Size(50, 50);
                            picture.MaximumSize = new Size(300, 300);
                            picture.DoubleClick += Picture_DoubleClick;
                            content = picture;
                            break;
                        default:
                            Button button = new Button();
                            button.FlatStyle = FlatStyle.Flat;
                            button.Width = 300;
                            button.Text = " A " + ext + " File HiChat Cannot Open, \nClick to Save To somewhere else";
                            button.Height = 4 * this.Font.Height;
                            button.Click += (object sender, EventArgs e) =>
                            {
                                SaveFileDialog sf = new SaveFileDialog();
                                sf.FileName = m.Type;
                                sf.Filter = "*.*|*.*";
                                if (sf.ShowDialog() == DialogResult.OK)
                                {
                                    if (File.Exists(sf.FileName))
                                    {
                                        if (MessageBox.Show("File Exist, Overwrite?", "File Exist", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            File.Copy(HiChat.HiChat.appDataImagePath + m.Type, sf.FileName, true);
                                        }
                                    }
                                    else
                                    {
                                        File.Copy(HiChat.HiChat.appDataImagePath + m.Type, sf.FileName, true);
                                    }
                                }
                            };
                            content = button;
                            break;
                    }
                }
                if (m.Sender.Equals(username))
                {
                    p.Image = myImage;/*
                    p.MouseHover += (sender, e) =>
                    {
                        ToolTip toolTip = new ToolTip();

                    };*/
                    toolTip.SetToolTip(p, username);
                    panelMessage.SizeChanged += (object sender, EventArgs e) =>
                    {
                        p.Location = new Point(panelMessage.Width - p.Width - margin - margin, p.Location.Y);
                        content.Location = new Point(p.Location.X - content.Width - margin, content.Location.Y);
                    };
                    p.Location = new Point(panelMessage.Width - p.Width - margin - margin, count - panelMessage.VerticalScroll.Value);
                    content.Location = new Point(p.Location.X - content.Width - margin, count - panelMessage.VerticalScroll.Value);
                }
                else
                {
                    p.Image = friend_Image;
                    toolTip.SetToolTip(p, m.Sender);
                    p.Location = new Point(margin + margin, count - panelMessage.VerticalScroll.Value);
                    content.Location = new Point(p.Location.X + p.Width + margin, count - panelMessage.VerticalScroll.Value);
                }
                if (content.Height > p.Height)
                {
                    count += content.Height + margin;
                }
                else
                {
                    count += p.Height + margin;
                }
                panelMessage.Controls.Add(p);
                panelMessage.Controls.Add(content);
            }
        }

        private void updateGrid()
        {
            List<HiChat.Message> newMessages = HiChat.HiChat.GetMessageOfUser(lstFriend.SelectedItem.ToString());
            int oldMessages = messages.Count;
            newMessages.Sort((x, y) => x.Message_date.CompareTo(y.Message_date));

            if (newMessages.Count > 0 && newMessages.Count > messages.Count)
            {
                newMessages.RemoveRange(0, oldMessages);
                loadControls(newMessages);
                messages.AddRange(newMessages);
            }
        }

        private void Picture_DoubleClick(object sender, EventArgs e)
        {
            formViewPicture.SetImage(((PictureBox)sender).Image);
            formViewPicture.Show();
        }

        private void changeMyPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to change password?", "Password", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string passw = Interaction.InputBox("What is your desired new password?", "New Password", "Default text");
                HiChat.HiChat.ResetPassword(username, Interaction.InputBox("What is your old password to verify?", "Old Password?", "Default text"), passw);
            }
        }
        private void changeMyPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                Image i = HiChat.HiChat.CopyImage(fd.FileName);
                HiChat.HiChat.SetProfileImage(username, HiChat.HiChat.SerializeImage(i, HiChat.HiChat.GetImFormat(i)));
                MessageBox.Show("Changed Picture", "Changed Picture");
            }

        }
        private void sendFriendRequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string friend_name = Interaction.InputBox("Please type your friend username", "Send Friend Request", "");
            if (!friend_name.Equals(username))
            {
                foreach (string item in lstFriend.Items)
                {
                    if (item.Equals(friend_name))
                    {
                        MessageBox.Show("Your already have this friend");
                        return;
                    }
                }
                if (HiChat.HiChat.UserExist(friend_name))
                {
                    HiChat.HiChat.SendFriendRequest(username, friend_name);
                    MessageBox.Show("Friend Request Sent");
                    return;
                }
                MessageBox.Show("Username not exist");
                return;
            }
            MessageBox.Show("You are already your friend");

        }
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to log out?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void picFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                switch (Path.GetExtension(fd.FileName))
                {
                    case ".jpg":
                        MessageBox.Show("Not accepting .jpg image file");
                        break;
                    case ".png":
                        MessageBox.Show("Not accepting .png image file");
                        break;
                    case ".jpeg":
                        MessageBox.Show("Not accepting .jpeg image file");
                        break;
                    default:
                        string fileArr = HiChat.HiChat.SerializeFile(fd.FileName);
                        string fileName = username + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + Path.GetExtension(fd.SafeFileName);
                        if (tabControlFriendGroup.SelectedIndex == 0)
                        {
                            HiChat.HiChat.AddMessageToLocal(HiChat.HiChat.SendMessage(username, lstFriend.SelectedItem.ToString(), fileArr, fileName));
                        }
                        else if (tabControlFriendGroup.SelectedIndex == 1)
                        {
                            HiChat.HiChat.AddMessageToLocal(HiChat.HiChat.SendMessageToGroup(username, ((Group)lstGroup.SelectedItem).Id.ToString(), fileArr, fileName));
                        }
                        HiChat.HiChat.BackUpFileToFolder(fd.FileName, fileName);
                        MessageBox.Show("File Sent", "File Sent");
                        break;
                }
            }
        }
        private void picimage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)| *.jpg; *.jpeg; *.gif; *.bmp";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string fileName = username + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + Path.GetExtension(fd.SafeFileName);
                string imgArr = HiChat.HiChat.SerializeImage(new Bitmap(fd.FileName), HiChat.HiChat.GetImFormat(new Bitmap(fd.FileName)));
                if (tabControlFriendGroup.SelectedIndex == 0)
                {
                    HiChat.HiChat.AddMessageToLocal(HiChat.HiChat.SendMessage(username, lstFriend.SelectedItem.ToString(), imgArr, fileName));
                }
                else
                {
                    HiChat.HiChat.AddMessageToLocal(HiChat.HiChat.SendMessageToGroup(username, ((Group)lstGroup.SelectedItem).Id, imgArr, fileName));
                }
                HiChat.HiChat.BackUpFileToFolder(fd.FileName, fileName);
                MessageBox.Show("Image Sent", "Image Sent");
            }
        }

        private void FormMenu_CreateGroup_Click(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            ContextMenu parentMenu = (ContextMenu)item.Parent;
            int indx = Convert.ToInt32(parentMenu.Tag);
            string friendName = lstFriend.Items[indx].ToString();
            string alias = Interaction.InputBox("Please type a group alias", "Create a group", "");
            HiChat.HiChat.RegisterGroup(username, friendName, alias);
        }
        private void FormMenu_DeleteFriend_Click(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            ContextMenu parentMenu = (ContextMenu)item.Parent;
            int indx = Convert.ToInt32(parentMenu.Tag);
            string friendName = lstFriend.Items[indx].ToString();
            if (MessageBox.Show("Are You sure to Delete Your Friend " + friendName, "Delete a Friend", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (MessageBox.Show("You are About to Delete You Friend " + friendName + "\nARE you sure to continue? ", "Warning! Delete Friend", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    HiChat.HiChat.DeleteFriend(username, friendName);
                    friendList = HiChat.HiChat.GetFriend(username);
                    lstFriend.DataSource = friendList;
                }
            }
        }
        private void FormMenu_AddGroupMember_Click(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            ContextMenu parentMenu = (ContextMenu)item.Parent;
            int indx = Convert.ToInt32(parentMenu.Tag);
            Group group = ((Group)lstGroup.Items[indx]);
            string groupID = group.Id;
            formAddMember = new FormAddMember(group, friendList);
            formAddMember.Show();
            formAddMember.FormClosed += FormAddMember_FormClosed;
        }

        private void FormAddMember_FormClosed(object sender, FormClosedEventArgs e)
        {
            FormAddMember me = (FormAddMember)sender;
            if (me.Result)
            {
                foreach (string item in me.Sheet.CheckedItems)
                {
                    HiChat.HiChat.AddGroup(item, me.group.Id);
                }
                MessageBox.Show("Add Group Success");
            }
        }

        private void FormMenu_QuitGroup_Click(object sender, EventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            ContextMenu parentMenu = (ContextMenu)item.Parent;
            int indx = Convert.ToInt32(parentMenu.Tag);
            Group group = ((Group)lstGroup.Items[indx]);
            string groupID = group.Id;
            if (MessageBox.Show("Are you sure to quit the group " + group.Alias, "Quit Group", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                HiChat.HiChat.QuitGroup(username, groupID);
                MessageBox.Show("Group Quit Success", "Group Quit");
                groupList = HiChat.HiChat.GetGroupByID(HiChat.HiChat.GetGroupID(username));
                lstGroup.DataSource = groupList;
            }
        }
        private void FormMenu_SizeChanged(object sender, EventArgs e)
        {
            tabControlFriendGroup.Width = this.Width / 5;
            groupBox2.Width = this.Width - tabControlFriendGroup.Width - 50;
            groupBox3.Height = groupBox2.Height - 50;
            txtSend.Height = groupBox3.Height / 4;
            panelMessage.Height = groupBox3.Height - txtSend.Height - 50;
            btnSend.Height = picimage.Height = picFile.Height = this.Font.Height * 2;
            btnSend.Location = new Point(groupBox2.Width - btnSend.Width, groupBox2.Height - btnSend.Height);
            picFile.Location = new Point(0, groupBox2.Height - picFile.Height);
            picimage.Location = new Point(picFile.Width + 10, groupBox2.Height - picimage.Height);
        }
        private void FormMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            timer1.Dispose();
        }

        private void lstFriend_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int indx = lstFriend.IndexFromPoint(e.Location);
                if (((ListBox)sender).SelectedIndex.Equals(indx))
                {
                    ContextMenu cm = new ContextMenu();
                    cm.MenuItems.Add(new MenuItem("Delete this friend", DeleteFriend_Click));
                    cm.MenuItems.Add(new MenuItem("Create group", CreateGroup_Click));
                    cm.Tag = indx;
                    cm.Show((ListBox)sender, e.Location);
                }
            }
        }
        private void lstGroup_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int indx = lstGroup.IndexFromPoint(e.Location);
                if (((ListBox)sender).SelectedIndex.Equals(indx))
                {
                    ContextMenu cm = new ContextMenu();
                    cm.MenuItems.Add(new MenuItem("Quit Group", QuitGroup_Click));
                    cm.MenuItems.Add(new MenuItem("Add Group Member", AddGroupMember_Click));
                    cm.Tag = indx;
                    cm.Show((ListBox)sender, e.Location);
                }
            }
        }
        private void tabControlFriendGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            messages = new List<HiChat.Message>();
            panelMessage.Controls.Clear();
            count = 0;
        }
    }
}
