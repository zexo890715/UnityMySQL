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
            string connectionString = "DSN=LAN";
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

    public static bool userCheck(string name)
    {   
        mysqlOpen();
        string query = $"SELECT name FROM user WHERE name = '{name}'";
        using (OdbcCommand command = new OdbcCommand(query, connection))
        {
            using (OdbcDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool pwdCheck(string name, string pwd)
    {   
        string password = null;
        mysqlOpen();
        string query = $"SELECT password FROM user WHERE name = '{name}'";
        using (OdbcCommand command = new OdbcCommand(query, connection))
        {
            using (OdbcDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();
                    password = reader.GetString(0);
                    if(password == pwd)
                        return true;
                }
            }
        }
        return false;
    }

    public static string idGenerate()
    {   
        string id = "";
        for (int i = 0; i < 8; i++)
        {
            int randomDigit = Random.Range(0, 10);
            id += randomDigit.ToString();
        }
        return id;
    }

    public static string idCheck()
    {   
        string id = idGenerate();
        bool idExists = false;
        do
        {
            mysqlOpen();
            string query = $"SELECT id FROM user WHERE id = '{id}'";
            using (OdbcCommand command = new OdbcCommand(query, connection))
            {
                using (OdbcDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        id = idGenerate();
                        idExists = true;
                    }
                    else
                    {
                        idExists = false;
                        return id;
                    }
                }
            }
        }
        while(idExists);
        return null;
    }
    
    public static void userWrite(string name, string password)
    {   
        string id = idCheck();
        mysqlOpen();
        string query = "INSERT INTO user (id, name, password) VALUES (?, ?, ?)";
        using (OdbcCommand command = new OdbcCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@password", password);
            command.ExecuteNonQuery();
        }
    }
}
