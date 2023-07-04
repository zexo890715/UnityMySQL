using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;

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
            Debug.Log("Login");
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
            Debug.Log("Logout");
        }
    }

    public static void mysqlRead()
    {   
        if (connection != null)
        {
            string query = "SELECT * FROM user";
            using (OdbcCommand command = new OdbcCommand(query, connection))
            {
                using (OdbcDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string id = reader.GetString(0);
                        string name = reader.GetString(1);
                        Debug.Log($" ID: {id}, Name: {name}");
                    }
                }
            }
        }
    }
}
