namespace Leo.GUI
{
    partial class winMain
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
            this.components = new System.ComponentModel.Container();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsRead = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parent_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pageurl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.treeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuTreeUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTreeUpdateAll = new System.Windows.Forms.ToolStripMenuItem();
            this.gridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuGridRedownload = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.treeMenu.SuspendLayout();
            this.gridMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(367, 397);
            this.webBrowser1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(4, 4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(607, 558);
            this.splitContainer1.SplitterDistance = 236;
            this.splitContainer1.TabIndex = 5;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(236, 558);
            this.treeView1.TabIndex = 6;
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer2.Size = new System.Drawing.Size(367, 558);
            this.splitContainer2.SplitterDistance = 157;
            this.splitContainer2.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.title,
            this.date,
            this.IsRead,
            this.parent_id,
            this.pageurl});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(367, 157);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // title
            // 
            this.title.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.title.DataPropertyName = "Title";
            this.title.HeaderText = "标题";
            this.title.Name = "title";
            this.title.ReadOnly = true;
            // 
            // date
            // 
            this.date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.date.DataPropertyName = "CDate";
            this.date.HeaderText = "日期";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 54;
            // 
            // IsRead
            // 
            this.IsRead.DataPropertyName = "IsRead";
            this.IsRead.HeaderText = "IsRead";
            this.IsRead.Name = "IsRead";
            this.IsRead.ReadOnly = true;
            this.IsRead.Visible = false;
            // 
            // parent_id
            // 
            this.parent_id.HeaderText = "parent_id";
            this.parent_id.Name = "parent_id";
            this.parent_id.ReadOnly = true;
            this.parent_id.Visible = false;
            // 
            // pageurl
            // 
            this.pageurl.HeaderText = "pageurl";
            this.pageurl.Name = "pageurl";
            this.pageurl.ReadOnly = true;
            this.pageurl.Visible = false;
            // 
            // treeMenu
            // 
            this.treeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTreeUpdate,
            this.menuTreeUpdateAll});
            this.treeMenu.Name = "treeMenu";
            this.treeMenu.Size = new System.Drawing.Size(125, 48);
            // 
            // menuTreeUpdate
            // 
            this.menuTreeUpdate.Name = "menuTreeUpdate";
            this.menuTreeUpdate.Size = new System.Drawing.Size(124, 22);
            this.menuTreeUpdate.Text = "更新";
            this.menuTreeUpdate.Click += new System.EventHandler(this.menuTreeUpdate_Click);
            // 
            // menuTreeUpdateAll
            // 
            this.menuTreeUpdateAll.Name = "menuTreeUpdateAll";
            this.menuTreeUpdateAll.Size = new System.Drawing.Size(124, 22);
            this.menuTreeUpdateAll.Text = "全部更新";
            this.menuTreeUpdateAll.Click += new System.EventHandler(this.menuTreeUpdateAll_Click);
            // 
            // gridMenu
            // 
            this.gridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGridRedownload});
            this.gridMenu.Name = "gridMenu";
            this.gridMenu.Size = new System.Drawing.Size(125, 26);
            // 
            // menuGridRedownload
            // 
            this.menuGridRedownload.Name = "menuGridRedownload";
            this.menuGridRedownload.Size = new System.Drawing.Size(124, 22);
            this.menuGridRedownload.Text = "重新下载";
            // 
            // winMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 566);
            this.Controls.Add(this.splitContainer1);
            this.Name = "winMain";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "国务院国资委信息汇总";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.winMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.treeMenu.ResumeLayout(false);
            this.gridMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip treeMenu;
        private System.Windows.Forms.ToolStripMenuItem menuTreeUpdate;
        private System.Windows.Forms.ToolStripMenuItem menuTreeUpdateAll;
        private System.Windows.Forms.ContextMenuStrip gridMenu;
        private System.Windows.Forms.ToolStripMenuItem menuGridRedownload;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsRead;
        private System.Windows.Forms.DataGridViewTextBoxColumn parent_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn pageurl;
    }
}

