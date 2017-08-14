using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WeixinTookeen.Client.Repository
{
    public class MySqlHelper
    {
        //从配置文件读取连接字符串
        private static readonly string connstr = "Server=118.244.206.185;User ID=wxqf;Password=wxqf#110119;Database=wxqf;CharSet=utf8";

        //创建连接
        public static MySqlConnection CreateConnection()
        {
            MySqlConnection conn = new MySqlConnection(connstr);
            conn.Open();
            return conn;
        }

        public static int ExecuteNonQuery(MySqlConnection conn, string sql, params MySqlParameter[] parameters)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        public static int ExecuteNonQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                return ExecuteNonQuery(conn, sql, parameters);
            }
        }

        public static object ExecuteScalar(MySqlConnection conn, string sql, params MySqlParameter[] parameters)
        {
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        public static object ExecuteScalar(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                return ExecuteScalar(conn, sql, parameters);
            }
        }

        public static DataTable ExecuteQuery(MySqlConnection conn, string sql, MySqlParameter[] parameters)
        {
            DataTable table = new DataTable();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    table.Load(reader);
                }
            }
            return table;
        }

        public static DataTable ExecuteQuery(string sql, params MySqlParameter[] parameters)
        {
            using (MySqlConnection conn = CreateConnection())
            {
                return ExecuteQuery(conn, sql, parameters);
            }
        }
    }
}
