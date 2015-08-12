namespace WindowsApplication2
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenButton = new System.Windows.Forms.Button();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.FileContentTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RenameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OutputButton = new System.Windows.Forms.Button();
            this.CoverOldFileCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoOutputCheckBox = new System.Windows.Forms.CheckBox();
            this.ttafixCheckBox = new System.Windows.Forms.CheckBox();
            this.fileLineCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SubDirTextBox = new System.Windows.Forms.TextBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.DelRegButton = new System.Windows.Forms.Button();
            this.AddRegButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.paremeterTextBox = new System.Windows.Forms.TextBox();
            this.PlayerPathTextBox = new System.Windows.Forms.TextBox();
            this.AutoDealOtherCueCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoSend2PlayerCheckBox = new System.Windows.Forms.CheckBox();
            this.AutoExitCheckBox = new System.Windows.Forms.CheckBox();
            this.OpenPlayerDialog = new System.Windows.Forms.OpenFileDialog();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenButton
            // 
            this.OpenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenButton.Location = new System.Drawing.Point(265, 13);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(53, 20);
            this.OpenButton.TabIndex = 0;
            this.OpenButton.Text = "打开..";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilePathTextBox.Location = new System.Drawing.Point(9, 14);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.ReadOnly = true;
            this.FilePathTextBox.Size = new System.Drawing.Size(250, 21);
            this.FilePathTextBox.TabIndex = 1;
            // 
            // FileContentTextBox
            // 
            this.FileContentTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FileContentTextBox.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileContentTextBox.Location = new System.Drawing.Point(11, 68);
            this.FileContentTextBox.Multiline = true;
            this.FileContentTextBox.Name = "FileContentTextBox";
            this.FileContentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FileContentTextBox.Size = new System.Drawing.Size(305, 170);
            this.FileContentTextBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "编码类型";
            // 
            // RenameTextBox
            // 
            this.RenameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RenameTextBox.Location = new System.Drawing.Point(93, 244);
            this.RenameTextBox.Name = "RenameTextBox";
            this.RenameTextBox.Size = new System.Drawing.Size(133, 21);
            this.RenameTextBox.TabIndex = 5;
            this.RenameTextBox.Text = "%filename%(UTF-8)";
            this.RenameTextBox.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "另存为文件名";
            // 
            // OutputButton
            // 
            this.OutputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputButton.Enabled = false;
            this.OutputButton.Location = new System.Drawing.Point(265, 39);
            this.OutputButton.Name = "OutputButton";
            this.OutputButton.Size = new System.Drawing.Size(53, 20);
            this.OutputButton.TabIndex = 7;
            this.OutputButton.Text = "保存";
            this.OutputButton.UseVisualStyleBackColor = true;
            this.OutputButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // CoverOldFileCheckBox
            // 
            this.CoverOldFileCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CoverOldFileCheckBox.AutoSize = true;
            this.CoverOldFileCheckBox.Location = new System.Drawing.Point(232, 246);
            this.CoverOldFileCheckBox.Name = "CoverOldFileCheckBox";
            this.CoverOldFileCheckBox.Size = new System.Drawing.Size(84, 16);
            this.CoverOldFileCheckBox.TabIndex = 8;
            this.CoverOldFileCheckBox.Text = "覆盖原文件";
            this.CoverOldFileCheckBox.UseVisualStyleBackColor = true;
            this.CoverOldFileCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // AutoOutputCheckBox
            // 
            this.AutoOutputCheckBox.AutoSize = true;
            this.AutoOutputCheckBox.Location = new System.Drawing.Point(112, 41);
            this.AutoOutputCheckBox.Name = "AutoOutputCheckBox";
            this.AutoOutputCheckBox.Size = new System.Drawing.Size(72, 16);
            this.AutoOutputCheckBox.TabIndex = 9;
            this.AutoOutputCheckBox.Text = "自动保存";
            this.AutoOutputCheckBox.UseVisualStyleBackColor = true;
            this.AutoOutputCheckBox.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // ttafixCheckBox
            // 
            this.ttafixCheckBox.AutoSize = true;
            this.ttafixCheckBox.Checked = true;
            this.ttafixCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ttafixCheckBox.Location = new System.Drawing.Point(6, 20);
            this.ttafixCheckBox.Name = "ttafixCheckBox";
            this.ttafixCheckBox.Size = new System.Drawing.Size(240, 16);
            this.ttafixCheckBox.TabIndex = 10;
            this.ttafixCheckBox.Text = "将FILE行\"The True Audio\"替换为\"WAVE\"";
            this.ttafixCheckBox.UseVisualStyleBackColor = true;
            // 
            // fileLineCheckBox
            // 
            this.fileLineCheckBox.AutoSize = true;
            this.fileLineCheckBox.Checked = true;
            this.fileLineCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fileLineCheckBox.Location = new System.Drawing.Point(6, 36);
            this.fileLineCheckBox.Name = "fileLineCheckBox";
            this.fileLineCheckBox.Size = new System.Drawing.Size(228, 16);
            this.fileLineCheckBox.TabIndex = 11;
            this.fileLineCheckBox.Text = "同目录查找音乐文件替换FILE行文件名";
            this.fileLineCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.SubDirTextBox);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.DelRegButton);
            this.groupBox1.Controls.Add(this.AddRegButton);
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.paremeterTextBox);
            this.groupBox1.Controls.Add(this.PlayerPathTextBox);
            this.groupBox1.Controls.Add(this.AutoDealOtherCueCheckBox);
            this.groupBox1.Controls.Add(this.AutoSend2PlayerCheckBox);
            this.groupBox1.Controls.Add(this.fileLineCheckBox);
            this.groupBox1.Controls.Add(this.ttafixCheckBox);
            this.groupBox1.Location = new System.Drawing.Point(9, 268);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 200);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "专用选项(仅对.cue文件有效)";
            // 
            // SubDirTextBox
            // 
            this.SubDirTextBox.Enabled = false;
            this.SubDirTextBox.Location = new System.Drawing.Point(118, 144);
            this.SubDirTextBox.Name = "SubDirTextBox";
            this.SubDirTextBox.Size = new System.Drawing.Size(180, 21);
            this.SubDirTextBox.TabIndex = 21;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Enabled = false;
            this.radioButton3.Location = new System.Drawing.Point(6, 145);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(101, 16);
            this.radioButton3.TabIndex = 20;
            this.radioButton3.Text = "移动到子目录:";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Enabled = false;
            this.radioButton2.Location = new System.Drawing.Point(96, 123);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(95, 16);
            this.radioButton2.TabIndex = 20;
            this.radioButton2.Text = "删除到回收站";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Enabled = false;
            this.radioButton1.Location = new System.Drawing.Point(6, 123);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "隐藏";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // DelRegButton
            // 
            this.DelRegButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DelRegButton.Location = new System.Drawing.Point(163, 172);
            this.DelRegButton.Name = "DelRegButton";
            this.DelRegButton.Size = new System.Drawing.Size(137, 22);
            this.DelRegButton.TabIndex = 19;
            this.DelRegButton.Text = "从右键菜单卸载";
            this.DelRegButton.UseVisualStyleBackColor = true;
            this.DelRegButton.Click += new System.EventHandler(this.button5_Click);
            // 
            // AddRegButton
            // 
            this.AddRegButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AddRegButton.Location = new System.Drawing.Point(8, 172);
            this.AddRegButton.Name = "AddRegButton";
            this.AddRegButton.Size = new System.Drawing.Size(137, 22);
            this.AddRegButton.TabIndex = 18;
            this.AddRegButton.Text = "注册到右键菜单";
            this.AddRegButton.UseVisualStyleBackColor = true;
            this.AddRegButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(240, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(58, 22);
            this.button3.TabIndex = 17;
            this.button3.Text = "浏览..";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "程序路径";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "参数";
            // 
            // paremeterTextBox
            // 
            this.paremeterTextBox.Enabled = false;
            this.paremeterTextBox.Location = new System.Drawing.Point(187, 50);
            this.paremeterTextBox.Name = "paremeterTextBox";
            this.paremeterTextBox.Size = new System.Drawing.Size(111, 21);
            this.paremeterTextBox.TabIndex = 14;
            // 
            // PlayerPathTextBox
            // 
            this.PlayerPathTextBox.Enabled = false;
            this.PlayerPathTextBox.Location = new System.Drawing.Point(65, 74);
            this.PlayerPathTextBox.Name = "PlayerPathTextBox";
            this.PlayerPathTextBox.Size = new System.Drawing.Size(169, 21);
            this.PlayerPathTextBox.TabIndex = 13;
            // 
            // AutoDealOtherCueCheckBox
            // 
            this.AutoDealOtherCueCheckBox.AutoSize = true;
            this.AutoDealOtherCueCheckBox.Location = new System.Drawing.Point(6, 101);
            this.AutoDealOtherCueCheckBox.Name = "AutoDealOtherCueCheckBox";
            this.AutoDealOtherCueCheckBox.Size = new System.Drawing.Size(96, 16);
            this.AutoDealOtherCueCheckBox.TabIndex = 12;
            this.AutoDealOtherCueCheckBox.Text = "处理其他cue:";
            this.AutoDealOtherCueCheckBox.UseVisualStyleBackColor = true;
            this.AutoDealOtherCueCheckBox.CheckedChanged += new System.EventHandler(this.AutoDealOtherCueCheckBox_CheckedChanged);
            // 
            // AutoSend2PlayerCheckBox
            // 
            this.AutoSend2PlayerCheckBox.AutoSize = true;
            this.AutoSend2PlayerCheckBox.Location = new System.Drawing.Point(6, 52);
            this.AutoSend2PlayerCheckBox.Name = "AutoSend2PlayerCheckBox";
            this.AutoSend2PlayerCheckBox.Size = new System.Drawing.Size(120, 16);
            this.AutoSend2PlayerCheckBox.TabIndex = 12;
            this.AutoSend2PlayerCheckBox.Text = "自动发送到播放器";
            this.AutoSend2PlayerCheckBox.UseVisualStyleBackColor = true;
            this.AutoSend2PlayerCheckBox.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // AutoExitCheckBox
            // 
            this.AutoExitCheckBox.AutoSize = true;
            this.AutoExitCheckBox.Enabled = false;
            this.AutoExitCheckBox.Location = new System.Drawing.Point(187, 41);
            this.AutoExitCheckBox.Name = "AutoExitCheckBox";
            this.AutoExitCheckBox.Size = new System.Drawing.Size(72, 16);
            this.AutoExitCheckBox.TabIndex = 14;
            this.AutoExitCheckBox.Text = "自动退出";
            this.AutoExitCheckBox.UseVisualStyleBackColor = true;
            // 
            // OpenPlayerDialog
            // 
            this.OpenPlayerDialog.FileName = "openFileDialog2";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "932",
            "936",
            "950"});
            this.comboBox1.Location = new System.Drawing.Point(63, 39);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(43, 20);
            this.comboBox1.TabIndex = 15;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::FixCue.Properties.Resources.未命名;
            this.pictureBox1.Location = new System.Drawing.Point(761, 385);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 105);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 474);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.OutputButton);
            this.Controls.Add(this.AutoExitCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.AutoOutputCheckBox);
            this.Controls.Add(this.CoverOldFileCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RenameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FileContentTextBox);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.OpenButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(342, 512);
            this.Name = "Form1";
            this.Text = "FixCue";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter_1);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.TextBox FileContentTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox RenameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OutputButton;
        private System.Windows.Forms.CheckBox CoverOldFileCheckBox;
        private System.Windows.Forms.CheckBox AutoOutputCheckBox;
        private System.Windows.Forms.CheckBox ttafixCheckBox;
        private System.Windows.Forms.CheckBox fileLineCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox AutoExitCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox paremeterTextBox;
        private System.Windows.Forms.TextBox PlayerPathTextBox;
        private System.Windows.Forms.CheckBox AutoSend2PlayerCheckBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.OpenFileDialog OpenPlayerDialog;
        private System.Windows.Forms.Button DelRegButton;
        private System.Windows.Forms.Button AddRegButton;
        private System.Windows.Forms.CheckBox AutoDealOtherCueCheckBox;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox SubDirTextBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

