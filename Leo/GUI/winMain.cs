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
        private string node_id = "0";

        public winMain()
        {
            InitializeComponent();
        }

        private void winMain_Load(object sender, EventArgs e)
        {
            List<Nodes> list = Nodes.Select();
            TreeNode root = new TreeNode("国务院国资委");
            treeView1.Nodes.Add(root);
            treeView1.SelectedNode = root;
            foreach (Nodes n in list)
            {
                TreeNode leaf = new TreeNode();
                leaf.Tag = n.ID;
                int count = n.UnreadCount();
                if (count > 0)
                {
                    leaf.Text = string.Format("{0}({1})", n.Name, n.UnreadCount());
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
                if (node_id != node.Tag.ToString())     // 如果显示的不是当前的结点的内容，就刷新
                {
                    List<Contents> lst = Contents.Select("node_id =" + node.Tag.ToString());
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
                    node_id = node.Tag.ToString();      // 记录当前结点的ID
                }
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    node.ContextMenuStrip = treeMenu;
                    treeView1.SelectedNode = node;
                    treeMenu.Show();
                }
            }
        }

        // 更新当前节点的内容
        private void menuTreeUpdate_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;

            string id = treeView1.SelectedNode.Tag.ToString();
            Contents c = new Contents();
            Sasac web = new Sasac();

            web.FindedLink += c.SaveContents;
            web.FindedLink += AddTreeNodeTipGridLine;

            web.UpdatePages();
        }

        private void AddTreeNodeTipGridLine(object sender, Leo.Reg.Sasac.LinkEventArgs e)
        {
            // 先为树增加未读的提示
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Tag.ToString() == e.parent_id.ToString())
                {

                }
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

        private void ShowContent()
        {
            if (dataGridView1.CurrentRow == null)
                return;
            int id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            string title = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string date = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string url = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            int parent_id = int.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString());

            string file_path = Contents.GetFile(id, parent_id);
            if (string.IsNullOrEmpty(file_path))
                file_path = Contents.SavePage(id, url, parent_id, title, date);
            //webBrowser1.Url = new Uri(file_path);
            //webBrowser1.Navigate(Application.StartupPath + @"\Test.html");
            webBrowser1.Navigate(Application.StartupPath + @"\" + file_path);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ShowContent();
        }
    }
}
