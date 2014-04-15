using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HtmlAgilityPack;
using Leo.DB;
using Leo.Reg;

namespace Leo.GUI
{
    public partial class winMain : Form
    {
        private string m_Current_Select_ID = "0";

        public winMain()
        {
            InitializeComponent();
        }

        private void winMain_Load(object sender, EventArgs e)
        {

            // 初始化树，其中Text属性为Name, Tag为未读数量， Name为ID
            List<Nodes> list = Nodes.Select();
            TreeNode root = new TreeNode("国务院国资委");
            treeView1.Nodes.Add(root);
            treeView1.SelectedNode = root;
            foreach (Nodes n in list)
            {
                TreeNode leaf = new TreeNode();
                int count = n.UnreadCount();

                leaf.Name = n.ID.ToString();            // Name 记录 ID
                leaf.Tag = count;                       // Tag 记录 未读数
                
                // 如果有未读的消息，则粗体显示
                if (count > 0)
                {
                    leaf.Text = string.Format("{0}({1})", n.Name, leaf.Tag);
                    leaf.NodeFont = new Font("宋体", 9, FontStyle.Bold);
                }
                else
                {
                    leaf.Text = n.Name;
                    leaf.NodeFont = new Font("宋体", 9, FontStyle.Regular);
                }
                treeView1.SelectedNode.Nodes.Add(leaf);
            }
            treeView1.ExpandAll();

            dataGridView1.AutoGenerateColumns = false;
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            TreeNode node = treeView1.GetNodeAt(p);
            if (node != null && node.Tag != null)
            {
                // 如果是右键的话
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    node.ContextMenuStrip = treeMenu;
                    treeView1.SelectedNode = node;
                    treeMenu.Show();
                }

                if (m_Current_Select_ID != node.Name.ToString())     // 如果显示的不是当前的结点的内容，就刷新
                {
                    dataGridView1.RowCount = 0;
                    List<Contents> lst = Contents.Select("node_id =" + node.Name.ToString());
                    //dataGridView1.DataSource = lst;
                    foreach (Contents c in lst)
                    {
                        dataGridView1.RowCount += 1;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = c.ID;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = c.Title;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = c.CDate;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = c.IsRead;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[4].Value = c.NodeID;
                        dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[5].Value = c.URL;
                        if (c.IsRead == "N")
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.Font = new Font("宋体", 9, FontStyle.Bold);
                        }
                        else
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.Font = new Font("宋体", 9, FontStyle.Regular);
                        }
                    }
                    m_Current_Select_ID = node.Name.ToString();      // 记录当前结点的ID

                    ShowContent();
                }
            }
        }

        // 树右键菜单的更新
        private void menuTreeUpdate_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;

            string id = treeView1.SelectedNode.Tag.ToString();
            Contents c = new Contents();
            Sasac web = new Sasac();

            web.FindedLink += c.SaveContents;
            c.SavedLink += AddTreeNodeTipGridLine;

            web.UpdatePages(int.Parse(treeView1.SelectedNode.Name));
            MessageBox.Show("更新完成");
        }


        // 当发现新内容时，更新界面
        private void AddTreeNodeTipGridLine(object sender, Leo.DB.Contents.ContentEventArgs e)
        {
            if (e.parent_id <= 0)
                return;
            Nodes parent = Nodes.Select("id = " + e.parent_id)[0];
            if (parent == null)
                return;

            // 先为树增加未读的提示
            foreach (TreeNode nodes in treeView1.Nodes)
            {
                foreach (TreeNode node in nodes.Nodes)
                {
                    if (node.Name.ToString() == e.parent_id.ToString())
                    {
                        // 未读数量
                        int unread = 0;
                        if (!string.IsNullOrEmpty(node.Tag.ToString()))
                            unread = int.Parse(node.Tag.ToString()) + 1;
                        node.Tag = unread;                       // Tag 记录 未读数
                        if (unread > 0)
                        {
                            node.Text = string.Format("{0}({1})", parent.Name, node.Tag);
                            node.NodeFont = new Font("宋体", 9, FontStyle.Bold);
                        }
                        else
                        {
                            node.Text = parent.Name;
                            node.NodeFont = new Font("宋体", 9, FontStyle.Regular);
                        }
                    }
                }
            }
            treeView1.Refresh();
            

            // 列表增加
            if (e.parent_id.ToString() == m_Current_Select_ID)
            {
                dataGridView1.RowCount += 1;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[0].Value = e.id;              // 这时还不知道ID，所以用0代替
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[1].Value = e.title;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[2].Value = e.date;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[3].Value = "N";
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[4].Value = e.parent_id;
                dataGridView1.Rows[dataGridView1.RowCount - 1].Cells[5].Value = e.url;
                dataGridView1.Rows[dataGridView1.RowCount - 1].DefaultCellStyle.Font = new Font("宋体", 9, FontStyle.Bold);
                dataGridView1.Refresh();
            }
        }

        // 更新全部节点
        private void menuTreeUpdateAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show(treeView1.SelectedNode.Tag.ToString());
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            ShowContent();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ShowContent();
        }

        // 显示内容
        private void ShowContent()
        {
            if (dataGridView1.CurrentRow == null)
                return;
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            string title = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string date = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string url = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            int parent_id = int.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString());

            // 看看有没有下载
            string file_path = Contents.GetFilePath(id, parent_id);
            if (string.IsNullOrEmpty(file_path))
                file_path = Contents.SavePage(id, url, parent_id, title, date);
            webBrowser1.Navigate(String.Format(@"{0}\{1}", Application.StartupPath, file_path));

            Contents c = Contents.Select("Id = " + id)[0];
            c.ReadLink += GridReadLink;
            c.ReadLink += TreeReadLink;
            c.SetRead();
        }

        
        // 当发现新内容时，更新界面
        private void GridReadLink(object sender, Leo.DB.Contents.ContentEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString() == e.id.ToString())
                {
                    row.DefaultCellStyle.Font = new Font("宋体", 9, FontStyle.Regular);
                    dataGridView1.Refresh();
                    return;
                }
            }
        }

        private void TreeReadLink(object sender, Leo.DB.Contents.ContentEventArgs e)
        {
            Nodes parent = Nodes.Select("id = " + e.parent_id)[0];
            if (parent == null)
                return;

            foreach (TreeNode nodes in treeView1.Nodes)
            {
                foreach (TreeNode node in nodes.Nodes)
                {
                    if (node.Name == e.parent_id.ToString())
                    {
                        int count = int.Parse(node.Tag.ToString())-1;
                        node.Tag = count;                       // Tag 记录 未读数
                        if (count > 0)
                        {
                            node.Text = string.Format("{0}({1})", parent.Name, node.Tag);
                            node.NodeFont = new Font("宋体", 9, FontStyle.Bold);
                        }
                        else
                        {
                            node.Text = parent.Name;
                            node.NodeFont = new Font("宋体", 9, FontStyle.Regular);
                        }
                        treeView1.Refresh();
                        return;
                    }
                }
            }
        }
    }
}
