using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using UnityEngine;

public class MysqlControl : MonoBehaviour
{
    public static OdbcConnection connection;
    
    public static void mysqlOpen()
    {
        try
        {
            string connectionString = "DSN=Unity_MySQL";
            connection = new OdbcConnection(connectionString);
            connection.Open();
        }
        catch(System.Exception e)
        {
            throw new System.Exception(""+e.Message.ToString());
        }
    }

    public static void mysqlClose()
    {
        if (connection != null)
        {
            connection.Close();
            connection.Dispose();
            connection = null;
            //Debug.Log("Logout");
        }
    }

    public static void mysqlRead()
    {   
        mysqlOpen();
        string query = "SELECT * FROM user";
        using (OdbcCommand command = new OdbcCommand(query, connection))
        {
            using (OdbcDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string id = reader.GetString(0);
                    string name = reader.GetString(1);
                    Debug.Log($"ID: {id}, Name: {name}");
                }
            }
        }
    }
    
    public static void mysqlWrite(string id, string name)
    {   
        mysqlOpen();
        string query = "INSERT INTO user (id, name) VALUES (?, ?)";
        using (OdbcCommand command = new OdbcCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.ExecuteNonQuery();
        }
    }

    public static bool userRead(string name, string pwd)
    {   
        string password = null;
        mysqlOpen();
        string query = $"SELECT password FROM user WHERE name = '{name}'";
        //Debug.Log(query);
        using (OdbcCommand command = new OdbcCommand(query, connection))
        {
            using (OdbcDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    password = reader.GetString(0);
                }
            }
        }
        if(password == pwd)
            return true;
        else
            return false;
    }
}
