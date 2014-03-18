using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace Leo.DB
{
    public class DBParams
    {
        public static string DBName = "sites.db";

        public static string ConnectionString = "Data Source=sites.db";

        public static void CreateDB()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"
CREATE TABLE Nodes(
    id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
    name varchar(255),
    url varchar(255));
CREATE TABLE Contents(
    id integer NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
    title varchar(255),
    cdate varchar(255),
    node_id integer,
    isdown varchar(1),
    isread varchar(1),
    url varchar(255));
";
                    command.ExecuteNonQuery();

                    //command.CommandText = "DROP TABLE Demo";
                    //command.ExecuteNonQuery();
                }
            }
        }
    }
}
