namespace iPhoneMessageExplorer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxConversations = new System.Windows.Forms.ListBox();
            this.labelConversations = new System.Windows.Forms.Label();
            this.textBoxMessages = new System.Windows.Forms.TextBox();
            this.labelMessages = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSMSExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelCountData = new System.Windows.Forms.Label();
            this.labelMsgCountData = new System.Windows.Forms.Label();
            this.labelMsgCount = new System.Windows.Forms.Label();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.buttonExportMessages = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxConversations
            // 
            this.listBoxConversations.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxConversations.FormattingEnabled = true;
            this.listBoxConversations.ItemHeight = 16;
            this.listBoxConversations.Location = new System.Drawing.Point(27, 88);
            this.listBoxConversations.Name = "listBoxConversations";
            this.listBoxConversations.Size = new System.Drawing.Size(202, 276);
            this.listBoxConversations.TabIndex = 0;
            this.listBoxConversations.SelectedIndexChanged += new System.EventHandler(this.listBoxConversations_SelectedIndexChanged);
            // 
            // labelConversations
            // 
            this.labelConversations.AutoSize = true;
            this.labelConversations.Location = new System.Drawing.Point(24, 72);
            this.labelConversations.Name = "labelConversations";
            this.labelConversations.Size = new System.Drawing.Size(74, 13);
            this.labelConversations.TabIndex = 1;
            this.labelConversations.Text = "Conversations";
            // 
            // textBoxMessages
            // 
            this.textBoxMessages.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxMessages.Font = new System.Drawing.Font("Segoe UI Emoji", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMessages.Location = new System.Drawing.Point(257, 87);
            this.textBoxMessages.Multiline = true;
            this.textBoxMessages.Name = "textBoxMessages";
            this.textBoxMessages.ReadOnly = true;
            this.textBoxMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessages.Size = new System.Drawing.Size(520, 277);
            this.textBoxMessages.TabIndex = 2;
            // 
            // labelMessages
            // 
            this.labelMessages.AutoSize = true;
            this.labelMessages.Location = new System.Drawing.Point(254, 71);
            this.labelMessages.Name = "labelMessages";
            this.labelMessages.Size = new System.Drawing.Size(55, 13);
            this.labelMessages.TabIndex = 3;
            this.labelMessages.Text = "Messages";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSMSExplorerToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // aboutSMSExplorerToolStripMenuItem
            // 
            this.aboutSMSExplorerToolStripMenuItem.Name = "aboutSMSExplorerToolStripMenuItem";
            this.aboutSMSExplorerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutSMSExplorerToolStripMenuItem.Text = "About SMSExplorer";
            this.aboutSMSExplorerToolStripMenuItem.Click += new System.EventHandler(this.aboutSMSExplorerToolStripMenuItem_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(257, 32);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(232, 20);
            this.textBoxSearch.TabIndex = 5;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(495, 30);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(66, 23);
            this.buttonSearch.TabIndex = 7;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(567, 30);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(66, 23);
            this.buttonNext.TabIndex = 8;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(711, 30);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(66, 23);
            this.buttonClear.TabIndex = 9;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Location = new System.Drawing.Point(639, 30);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(66, 23);
            this.buttonPrevious.TabIndex = 10;
            this.buttonPrevious.Text = "Previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(165, 367);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(38, 13);
            this.labelCount.TabIndex = 11;
            this.labelCount.Text = "Count:";
            // 
            // labelCountData
            // 
            this.labelCountData.Location = new System.Drawing.Point(209, 367);
            this.labelCountData.Name = "labelCountData";
            this.labelCountData.Size = new System.Drawing.Size(20, 13);
            this.labelCountData.TabIndex = 12;
            this.labelCountData.Text = "<num>";
            // 
            // labelMsgCountData
            // 
            this.labelMsgCountData.Location = new System.Drawing.Point(344, 367);
            this.labelMsgCountData.Name = "labelMsgCountData";
            this.labelMsgCountData.Size = new System.Drawing.Size(41, 13);
            this.labelMsgCountData.TabIndex = 14;
            this.labelMsgCountData.Text = "<num>";
            // 
            // labelMsgCount
            // 
            this.labelMsgCount.AutoSize = true;
            this.labelMsgCount.Location = new System.Drawing.Point(254, 367);
            this.labelMsgCount.Name = "labelMsgCount";
            this.labelMsgCount.Size = new System.Drawing.Size(84, 13);
            this.labelMsgCount.TabIndex = 13;
            this.labelMsgCount.Text = "Message Count:";
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(27, 32);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(202, 21);
            this.buttonOpenFile.TabIndex = 15;
            this.buttonOpenFile.Text = "Open File";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // buttonExportMessages
            // 
            this.buttonExportMessages.Location = new System.Drawing.Point(660, 370);
            this.buttonExportMessages.Name = "buttonExportMessages";
            this.buttonExportMessages.Size = new System.Drawing.Size(117, 24);
            this.buttonExportMessages.TabIndex = 16;
            this.buttonExportMessages.Text = "Export Messages";
            this.buttonExportMessages.UseVisualStyleBackColor = true;
            this.buttonExportMessages.Click += new System.EventHandler(this.buttonExportMessages_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 404);
            this.Controls.Add(this.buttonExportMessages);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.labelMsgCountData);
            this.Controls.Add(this.labelMsgCount);
            this.Controls.Add(this.labelCountData);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.labelMessages);
            this.Controls.Add(this.textBoxMessages);
            this.Controls.Add(this.labelConversations);
            this.Controls.Add(this.listBoxConversations);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "SMS Explorer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxConversations;
        private System.Windows.Forms.Label labelConversations;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.Label labelMessages;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSMSExplorerToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelCountData;
        private System.Windows.Forms.Label labelMsgCountData;
        private System.Windows.Forms.Label labelMsgCount;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Button buttonExportMessages;
    }
}

