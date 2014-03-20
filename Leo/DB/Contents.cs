using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Data.SQLite;
using Leo.DB;
using System.Data;

namespace Leo.DB
{
    public class Contents : IDB<Contents>
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string CDate { get; set; }

        /// <summary>
        /// 所属结点
        /// </summary>
        public int NodeID { get; set; }

        /// <summary>
        /// 是否已读，Y为已读，N为未读
        /// </summary>
        public string IsRead { get; set; }

        /// <summary>
        /// 是否已经下载，Y为下载，N为未下载
        /// </summary>
        public string IsDown { get; set; }

        public string URL { get; set; }


        private void filter()
        {
            if (string.IsNullOrEmpty(IsDown) || IsDown.ToUpper() != "Y")
                IsDown = "N";
            if (string.IsNullOrEmpty(IsRead) || IsRead.ToUpper() != "Y")
                IsRead = "N";
        }

        public bool Insert()
        {
            filter();

            using (SQLiteConnection connection = new SQLiteConnection(DBParams.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    const string sql = "Insert Into  Contents(Title, cdate, node_id, isread, isdown, url) values(@title, @cdate, @node_id, @isread, @isdown, @url)";
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@title", Title);
                    command.Parameters.AddWithValue("@cdate", CDate);
                    command.Parameters.AddWithValue("@node_id", NodeID);
                    command.Parameters.AddWithValue("@isread", IsRead);
                    command.Parameters.AddWithValue("@isdown", IsDown);
                    command.Parameters.AddWithValue("@url", URL);
                    command.ExecuteScalar();

                    command.CommandText = "select distinct last_insert_rowid() from Contents";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    ID = int.Parse(data.Rows[0][0].ToString());
                    return true;
                }
            }
        }

        public bool Update()
        {
            filter();

            using (SQLiteConnection connection = new SQLiteConnection(DBParams.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    const string sql = "Update Contents Set Title = @title, cdate = @cdate, node_id = @nodeid, isread = @isread, isdown = @isdown";
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@title", Title);
                    command.Parameters.AddWithValue("@cdate", CDate);
                    command.Parameters.AddWithValue("@node_id", NodeID);
                    command.Parameters.AddWithValue("@isread", IsRead);
                    command.Parameters.AddWithValue("@isdown", IsDown);
                    command.Parameters.AddWithValue("@url", URL);
                    command.ExecuteScalar();
                    return true;
                }
            }
        }

        public bool Delete()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBParams.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    const string sql = "Delete Contents where ID = @ID";
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteScalar();
                    return true;
                }
            }
        }

        public static List<Contents> Select(string where = "")
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBParams.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    string sql;
                    if (!string.IsNullOrEmpty(where))
                        sql = string.Format("Select * From Contents Where {0}", where);
                    else
                        sql = "Select * From Contents";
                    command.CommandText = sql;

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    //ID = int.Parse(data.Rows[0][0].ToString());

                    List<Contents> list = new List<Contents>();
                    foreach (DataRow row in data.Rows)
                    {
                        Contents ct = new Contents();
                        ct.ID = int.Parse(row["ID"].ToString());
                        ct.Title = row["Title"].ToString();
                        ct.CDate = row["CDate"].ToString();
                        ct.NodeID = int.Parse(row["Node_ID"].ToString());
                        ct.IsRead = row["IsRead"].ToString();
                        ct.IsDown = row["IsDown"].ToString();
                        ct.URL = row["url"].ToString();
                        list.Add(ct);
                    }
                    return list;
                }
            }
        }

        /// <summary>
        /// 保存分析出来的联接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SaveContents(object sender, Leo.Reg.Sasac.LinkEventArgs e)
        {

            // 先判断是否已经存在
            if (Contents.Select(string.Format("url = '{0}'", e.url)).Count == 0)
            {
                // 不存在就要保存到数据库里面。
                Contents content = new Contents();
                content.Title = e.title;
                content.URL = e.url;
                content.CDate = e.date;
                content.NodeID = e.parent_id;
                content.Insert();
            }
        }


        /// <summary>
        /// 保存页面的内容
        /// </summary>
        public static string SavePage(int id, string page_url, int parent_id, string title, string date)
        {
            // 先判断目录下存在不存在
            
            // 一定会有pages目录
            if (!System.IO.Directory.Exists("pages"))
                System.IO.Directory.CreateDirectory("pages");

            // 应该保存的目录名称
            string dir_name = string.Format("{0:0000}", parent_id);
            if (!System.IO.Directory.Exists("pages/"+dir_name))
                System.IO.Directory.CreateDirectory("pages/" + dir_name);

            // 取得保存的内容
            string template = @"
<HTML><HEAD><link href='../style.css'rel='stylesheet'type='text/css'></HEAD><BODY text=#000000 vLink=#990000 aLink=#990000 link=#990000 leftMargin=0 topMargin=0 marginheight=0 marginwidth=0 Bgcolor=#E7F4FE class=Body style='word-break:break-all'><table border='0'height='98'cellpadding='0'cellspacing='0'width=100%id='main'><tr><td width='100%'align='left'valign='top'><br><br></TD></TR><TR vAlign=center align=left><TD height='295'valign='top'align='left'><TABLE cellSpacing=0 borderColorDark=#999999 cellPadding=0 align=center borderColorLight=#ffffff border='0'style='line-height:150%;'WIDTH='90%'><TBODY><TR vAlign=top align=left><TD id='thetd'class='thetd'><div align='left'style='width: 100%; height: 132'><p align='left'><center><font class='article_title'>{0}</font></center><br><hr size='1'noshade color='#FF9900'><span id='content'><!--BookContent Start-->{1}<br/><br/>&nbsp;&nbsp;<!--BookContent End--><br/><br/></span></p></div></TD></TR></TBODY></TABLE></TD></TR></TABLE><br></BODY></HTML>
";

            string content = string.Format(template,
                                            title,
                                            Leo.Reg.Sasac.GetLinkContent(page_url));
            string file_name = string.Format("{0:00000000}",id);
            string path = string.Format("./pages/{0}/{1}.html", dir_name, file_name);
            System.IO.File.WriteAllText(path, content, Encoding.UTF8);
            return path;
        }

        public static string GetFile(int id, int parent_id)
        {
            string dir_name = string.Format("{0:0000}", parent_id);
            string file_name = string.Format("{0:00000000}", id);
            string path = string.Format("./pages/{0}/{1}.html", dir_name, file_name);

            if (!System.IO.File.Exists(path))
                return "";
            else
                return path;
        }
    }
}
