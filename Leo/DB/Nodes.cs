using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SQLite;
using Leo.DB;
using System.Data;

namespace Leo.DB
{
    public class Nodes : IDB<Nodes>
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 结点名称
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 结点地址
        /// </summary>
        public string URL { get; set; }


        public bool Insert()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBParams.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    string sql = "Insert Into  Nodes(name, url) values(@Name, @URL)";
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@URL", URL);
                    command.ExecuteScalar();

                    command.CommandText = "select distinct last_insert_rowid() from Nodes";
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
            using (SQLiteConnection connection = new SQLiteConnection(DBParams.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    string sql = "Update Nodes set name = @Name, url = @URL where ID = @ID";
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@ID", ID);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@URL", URL);
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
                    string sql = "Delete Nodes where ID = @ID";
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@ID", ID);
                    command.ExecuteScalar();
                    return true;
                }
            }
        }

        public int UnreadCount()
        {
            if (ID <= 0)
                return 0;

            using (SQLiteConnection connection = new SQLiteConnection(DBParams.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = string.Format("select count(*) from Contents where node_id = {0} and isread = 'N'", ID);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return int.Parse(data.Rows[0][0].ToString());
                }
            }
        }

        public static List<Nodes> Select(string where = "")
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBParams.ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    string sql;
                    if (!string.IsNullOrEmpty(where))
                        sql = string.Format("Select * From Nodes Where {0}", where);
                    else
                        sql = "Select * From Nodes";
                    command.CommandText = sql;

                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable data = new DataTable();
                    adapter.Fill(data);

                    List<Nodes> list = new List<Nodes>();
                    foreach (DataRow row in data.Rows)
                    {
                        Nodes node = new Nodes();
                        node.ID = int.Parse(row["ID"].ToString());
                        node.Name = row["Name"].ToString();
                        node.URL = row["url"].ToString();
                        list.Add(node);
                    }
                    return list;
                }
            }
        }
    }
}
