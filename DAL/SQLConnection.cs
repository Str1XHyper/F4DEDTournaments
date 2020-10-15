using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class SQLConnection
    {
        private static MySqlConnection CreateConnection()
        {
            MySqlConnection cnn;
            string connetionString = $"Server=185.182.57.161;user=tijnvcd415_Tournaments;password=12345678;Database=tijnvcd415_Tournaments;Treat Tiny As Boolean=true";
            cnn = new MySqlConnection(connetionString);
            return cnn;
        }
        public static List<string[]> ExecuteSearchQuery(string query)
        {
            string[] tempStrArr;
            List<string[]> values = new List<string[]>();
            MySqlConnection cnn = CreateConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = cnn;
            cnn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            values.Clear();
            try
            {
                while (reader.Read())
                {
                    tempStrArr = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        tempStrArr[i] = reader[i].ToString();
                    }
                    values.Add(tempStrArr);
                }
            }
            catch (Exception e)
            {
                string eString = e.ToString();
            }
            cnn.Close();
            return values;
        }

        
        public static bool ExecuteNonSearchQuery(string query)
        {
            MySqlConnection cnn = CreateConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = cnn;
            cnn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            } 
            catch(Exception ex)
            {
                string test = ex.ToString();
                return false;
            }
            cnn.Close();
            return true;
        }
    }
}
