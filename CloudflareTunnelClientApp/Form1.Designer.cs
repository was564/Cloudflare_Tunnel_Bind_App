using System.Data;
using System.Windows.Forms;

namespace CloudflareTunnelBindApp
{
    using TranslationKey = LanguageManager.TranslationKey;
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.executeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdOutput = new System.Windows.Forms.TextBox();
            this.cmdPage = new System.Windows.Forms.TabPage();
            this.tunnelListPage = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.protocolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hostnameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.localBindUrlDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.portDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tunnelInfoTable = new CloudflareTunnelBindApp.TunnelInfoTable();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.koreanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            LanguageManager.Initialize(this, koreanToolStripMenuItem, englishToolStripMenuItem);

            this.executeMenu.SuspendLayout();
            this.cmdPage.SuspendLayout();
            this.tunnelListPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunnelInfoTable)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(46, 25);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(98, 41);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = LanguageManager.Translate(TranslationKey.Start);
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.startItem_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(198, 25);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(98, 41);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = LanguageManager.Translate(TranslationKey.Stop);
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.stopItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.executeMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // executeMenu
            // 
            this.executeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startItem,
            this.stopItem,
            this.exitItem});
            this.executeMenu.Name = "executeMenu";
            this.executeMenu.Size = new System.Drawing.Size(99, 70);
            // 
            // startItem
            // 
            this.startItem.Name = "startItem";
            this.startItem.Size = new System.Drawing.Size(98, 22);
            this.startItem.Text = LanguageManager.Translate(TranslationKey.Start);
            this.startItem.Click += new System.EventHandler(this.startItem_Click);
            // 
            // stopItem
            // 
            this.stopItem.Name = "stopItem";
            this.stopItem.Size = new System.Drawing.Size(98, 22);
            this.stopItem.Text = LanguageManager.Translate(TranslationKey.Stop);
            this.stopItem.Click += new System.EventHandler(this.stopItem_Click);
            // 
            // exitItem
            // 
            this.exitItem.Name = "exitItem";
            this.exitItem.Size = new System.Drawing.Size(98, 22);
            this.exitItem.Text = LanguageManager.Translate(TranslationKey.Exit);
            this.exitItem.Click += new System.EventHandler(this.exitItem_Click);
            // 
            // cmdOutput
            // 
            this.cmdOutput.BackColor = System.Drawing.Color.White;
            this.cmdOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOutput.Location = new System.Drawing.Point(3, 3);
            this.cmdOutput.Multiline = true;
            this.cmdOutput.Name = "cmdOutput";
            this.cmdOutput.ReadOnly = true;
            this.cmdOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.cmdOutput.Size = new System.Drawing.Size(517, 327);
            this.cmdOutput.TabIndex = 0;
            // 
            // cmdPage
            // 
            this.cmdPage.Controls.Add(this.cmdOutput);
            this.cmdPage.Location = new System.Drawing.Point(4, 22);
            this.cmdPage.Name = "cmdPage";
            this.cmdPage.Padding = new System.Windows.Forms.Padding(3);
            this.cmdPage.Size = new System.Drawing.Size(523, 333);
            this.cmdPage.TabIndex = 0;
            this.cmdPage.Text = LanguageManager.Translate(TranslationKey.CmdOutput);
            this.cmdPage.UseVisualStyleBackColor = true;
            // 
            // tunnelListPage
            // 
            this.tunnelListPage.Controls.Add(this.dataGridView1);
            this.tunnelListPage.Location = new System.Drawing.Point(4, 22);
            this.tunnelListPage.Name = "tunnelListPage";
            this.tunnelListPage.Padding = new System.Windows.Forms.Padding(3);
            this.tunnelListPage.Size = new System.Drawing.Size(523, 333);
            this.tunnelListPage.TabIndex = 1;
            this.tunnelListPage.Text = LanguageManager.Translate(TranslationKey.TunnelList);
            this.tunnelListPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.protocolDataGridViewTextBoxColumn,
            this.hostnameDataGridViewTextBoxColumn,
            this.localBindUrlDataGridViewTextBoxColumn,
            this.portDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tunnelInfoTable;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(517, 327);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            // 
            // protocolDataGridViewTextBoxColumn
            // 
            this.protocolDataGridViewTextBoxColumn.DataPropertyName = "Protocol";
            this.protocolDataGridViewTextBoxColumn.HeaderText = "Protocol";
            this.protocolDataGridViewTextBoxColumn.Name = "protocolDataGridViewTextBoxColumn";
            // 
            // hostnameDataGridViewTextBoxColumn
            // 
            this.hostnameDataGridViewTextBoxColumn.DataPropertyName = "Hostname";
            this.hostnameDataGridViewTextBoxColumn.HeaderText = "Hostname";
            this.hostnameDataGridViewTextBoxColumn.Name = "hostnameDataGridViewTextBoxColumn";
            this.hostnameDataGridViewTextBoxColumn.Width = 150;
            // 
            // localBindUrlDataGridViewTextBoxColumn
            // 
            this.localBindUrlDataGridViewTextBoxColumn.DataPropertyName = "Local Bind Url";
            this.localBindUrlDataGridViewTextBoxColumn.HeaderText = "Local Bind Url";
            this.localBindUrlDataGridViewTextBoxColumn.Name = "localBindUrlDataGridViewTextBoxColumn";
            this.localBindUrlDataGridViewTextBoxColumn.Width = 150;
            // 
            // portDataGridViewTextBoxColumn
            // 
            this.portDataGridViewTextBoxColumn.DataPropertyName = "Port";
            this.portDataGridViewTextBoxColumn.HeaderText = "Port";
            this.portDataGridViewTextBoxColumn.Name = "portDataGridViewTextBoxColumn";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.cmdPage);
            this.tabControl1.Controls.Add(this.tunnelListPage);
            this.tabControl1.Location = new System.Drawing.Point(46, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(531, 359);
            this.tabControl1.TabIndex = 3;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.languageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(628, 24);
            this.menuStrip1.TabIndex = 4;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.fileToolStripMenuItem.Text = LanguageManager.Translate(TranslationKey.File);
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShowShortcutKeys = false;
            this.startToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.startToolStripMenuItem.Text = LanguageManager.Translate(TranslationKey.Start);
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stopToolStripMenuItem.Text = LanguageManager.Translate(TranslationKey.Stop);
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = LanguageManager.Translate(TranslationKey.Exit);
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitItem_Click);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.koreanToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.languageToolStripMenuItem.Text = LanguageManager.Translate(TranslationKey.Language);
            // 
            // koreanToolStripMenuItem
            // 
            this.koreanToolStripMenuItem.Name = "koreanToolStripMenuItem";
            this.koreanToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.koreanToolStripMenuItem.Text = LanguageManager.Translate(TranslationKey.Korean);
            this.koreanToolStripMenuItem.Click += new System.EventHandler(this.koreanToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.englishToolStripMenuItem.Text = LanguageManager.Translate(TranslationKey.English);
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 491);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Cloudflare Tunnel Bind Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.executeMenu.ResumeLayout(false);
            this.cmdPage.ResumeLayout(false);
            this.cmdPage.PerformLayout();
            this.tunnelListPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tunnelInfoTable)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip executeMenu;
        private System.Windows.Forms.ToolStripMenuItem startItem;
        private System.Windows.Forms.ToolStripMenuItem stopItem;
        private System.Windows.Forms.ToolStripMenuItem exitItem;
        private System.Windows.Forms.TextBox cmdOutput;
        private TabPage cmdPage;
        private TabPage tunnelListPage;
        private TabControl tabControl1;
        private TunnelInfoTable tunnelInfoTable;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn protocolDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn hostnameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn localBindUrlDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn portDataGridViewTextBoxColumn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem startToolStripMenuItem;
        private ToolStripMenuItem stopToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripMenuItem koreanToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;
    }
}

