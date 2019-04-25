namespace HiChat.GUI
{
    partial class FormMenu
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMenu));
            this.lstFriend = new System.Windows.Forms.ListBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControlFriendGroup = new System.Windows.Forms.TabControl();
            this.tabPageFriend = new System.Windows.Forms.TabPage();
            this.tabPageGroup = new System.Windows.Forms.TabPage();
            this.lstGroup = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panelMessage = new System.Windows.Forms.Panel();
            this.picimage = new System.Windows.Forms.PictureBox();
            this.picFile = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.changeMyPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeMyPictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendFriendRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.tabControlFriendGroup.SuspendLayout();
            this.tabPageFriend.SuspendLayout();
            this.tabPageGroup.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picimage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFile)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstFriend
            // 
            this.lstFriend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lstFriend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFriend.FormattingEnabled = true;
            this.lstFriend.ItemHeight = 20;
            this.lstFriend.Location = new System.Drawing.Point(3, 3);
            this.lstFriend.Margin = new System.Windows.Forms.Padding(5);
            this.lstFriend.Name = "lstFriend";
            this.lstFriend.Size = new System.Drawing.Size(278, 658);
            this.lstFriend.TabIndex = 0;
            this.lstFriend.SelectedIndexChanged += new System.EventHandler(this.lstFriend_SelectedIndexChanged);
            this.lstFriend.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstFriend_MouseDown);
            // 
            // txtSend
            // 
            this.txtSend.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtSend.Location = new System.Drawing.Point(0, 368);
            this.txtSend.Margin = new System.Windows.Forms.Padding(0);
            this.txtSend.Multiline = true;
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(978, 133);
            this.txtSend.TabIndex = 1;
            // 
            // btnSend
            // 
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(898, 528);
            this.btnSend.Margin = new System.Windows.Forms.Padding(5);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 50);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControlFriendGroup);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.toolStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(5);
            this.groupBox1.Size = new System.Drawing.Size(1283, 753);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tabControlFriendGroup
            // 
            this.tabControlFriendGroup.Controls.Add(this.tabPageFriend);
            this.tabControlFriendGroup.Controls.Add(this.tabPageGroup);
            this.tabControlFriendGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControlFriendGroup.Location = new System.Drawing.Point(5, 51);
            this.tabControlFriendGroup.Name = "tabControlFriendGroup";
            this.tabControlFriendGroup.SelectedIndex = 0;
            this.tabControlFriendGroup.Size = new System.Drawing.Size(292, 697);
            this.tabControlFriendGroup.TabIndex = 12;
            this.tabControlFriendGroup.SelectedIndexChanged += new System.EventHandler(this.tabControlFriendGroup_SelectedIndexChanged);
            // 
            // tabPageFriend
            // 
            this.tabPageFriend.Controls.Add(this.lstFriend);
            this.tabPageFriend.Location = new System.Drawing.Point(4, 29);
            this.tabPageFriend.Name = "tabPageFriend";
            this.tabPageFriend.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFriend.Size = new System.Drawing.Size(284, 664);
            this.tabPageFriend.TabIndex = 0;
            this.tabPageFriend.Text = "Friend";
            this.tabPageFriend.UseVisualStyleBackColor = true;
            // 
            // tabPageGroup
            // 
            this.tabPageGroup.Controls.Add(this.lstGroup);
            this.tabPageGroup.Location = new System.Drawing.Point(4, 29);
            this.tabPageGroup.Name = "tabPageGroup";
            this.tabPageGroup.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGroup.Size = new System.Drawing.Size(284, 664);
            this.tabPageGroup.TabIndex = 1;
            this.tabPageGroup.Text = "Group";
            this.tabPageGroup.UseVisualStyleBackColor = true;
            // 
            // lstGroup
            // 
            this.lstGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lstGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstGroup.FormattingEnabled = true;
            this.lstGroup.ItemHeight = 20;
            this.lstGroup.Location = new System.Drawing.Point(3, 3);
            this.lstGroup.Name = "lstGroup";
            this.lstGroup.Size = new System.Drawing.Size(278, 658);
            this.lstGroup.TabIndex = 0;
            this.lstGroup.SelectedIndexChanged += new System.EventHandler(this.lstGroup_SelectedIndexChanged);
            this.lstGroup.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstGroup_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.picimage);
            this.groupBox2.Controls.Add(this.picFile);
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(300, 51);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox2.Size = new System.Drawing.Size(978, 697);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtSend);
            this.groupBox3.Controls.Add(this.panelMessage);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 19);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox3.Size = new System.Drawing.Size(978, 501);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // panelMessage
            // 
            this.panelMessage.AutoScroll = true;
            this.panelMessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMessage.Location = new System.Drawing.Point(0, 19);
            this.panelMessage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panelMessage.Name = "panelMessage";
            this.panelMessage.Size = new System.Drawing.Size(978, 346);
            this.panelMessage.TabIndex = 9;
            // 
            // picimage
            // 
            this.picimage.Image = global::Hichat.Properties.Resources.photoBtn;
            this.picimage.Location = new System.Drawing.Point(68, 528);
            this.picimage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.picimage.Name = "picimage";
            this.picimage.Size = new System.Drawing.Size(94, 50);
            this.picimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picimage.TabIndex = 7;
            this.picimage.TabStop = false;
            this.picimage.Click += new System.EventHandler(this.picimage_Click);
            // 
            // picFile
            // 
            this.picFile.Image = ((System.Drawing.Image)(resources.GetObject("picFile.Image")));
            this.picFile.Location = new System.Drawing.Point(4, 526);
            this.picFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.picFile.Name = "picFile";
            this.picFile.Size = new System.Drawing.Size(56, 50);
            this.picFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picFile.TabIndex = 4;
            this.picFile.TabStop = false;
            this.picFile.Click += new System.EventHandler(this.picFile_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(5, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1273, 27);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeMyPasswordToolStripMenuItem,
            this.changeMyPictureToolStripMenuItem,
            this.sendFriendRequestToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(66, 24);
            this.toolStripDropDownButton1.Text = "Action";
            // 
            // changeMyPasswordToolStripMenuItem
            // 
            this.changeMyPasswordToolStripMenuItem.Name = "changeMyPasswordToolStripMenuItem";
            this.changeMyPasswordToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.changeMyPasswordToolStripMenuItem.Text = "Change My Password";
            this.changeMyPasswordToolStripMenuItem.Click += new System.EventHandler(this.changeMyPasswordToolStripMenuItem_Click);
            // 
            // changeMyPictureToolStripMenuItem
            // 
            this.changeMyPictureToolStripMenuItem.Name = "changeMyPictureToolStripMenuItem";
            this.changeMyPictureToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.changeMyPictureToolStripMenuItem.Text = "Change My Picture";
            this.changeMyPictureToolStripMenuItem.Click += new System.EventHandler(this.changeMyPictureToolStripMenuItem_Click);
            // 
            // sendFriendRequestToolStripMenuItem
            // 
            this.sendFriendRequestToolStripMenuItem.Name = "sendFriendRequestToolStripMenuItem";
            this.sendFriendRequestToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.sendFriendRequestToolStripMenuItem.Text = "Send Friend Request";
            this.sendFriendRequestToolStripMenuItem.Click += new System.EventHandler(this.sendFriendRequestToolStripMenuItem_Click);
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.logOutToolStripMenuItem.Text = "Log Out";
            this.logOutToolStripMenuItem.Click += new System.EventHandler(this.logOutToolStripMenuItem_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1283, 753);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(650, 500);
            this.Name = "FormMenu";
            this.Text = "Hichat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMenu_FormClosing);
            this.Load += new System.EventHandler(this.FormMenu_Load);
            this.SizeChanged += new System.EventHandler(this.FormMenu_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControlFriendGroup.ResumeLayout(false);
            this.tabPageFriend.ResumeLayout(false);
            this.tabPageGroup.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picimage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFile)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstFriend;
        private System.Windows.Forms.TextBox txtSend;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picFile;
        private System.Windows.Forms.Panel panelMessage;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem changeMyPasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeMyPictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendFriendRequestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.PictureBox picimage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabControl tabControlFriendGroup;
        private System.Windows.Forms.TabPage tabPageFriend;
        private System.Windows.Forms.TabPage tabPageGroup;
        private System.Windows.Forms.ListBox lstGroup;
    }
}

