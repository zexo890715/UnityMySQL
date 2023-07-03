using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;

public class MysqlConnect : MonoBehaviour
{
    public static MySqlConnection myConnection;
    static string host = "127.0.0.1";
    static string user = "root";
    static string pwd = "5D7a2v5i8D@5";
    static string db = "unitydb";
    static string port = "3306";
    
    public static void Open()
    {
        string connectionString = string.Format("server={0};port={4};database={1};uid={2};pwd={3};", host, db, user, pwd, port);
        try
        {
            myConnection = new MySqlConnection(connectionString);
            myConnection.Open();
            Debug.Log("Success");
        }
        catch(System.Exception e)
        {
            throw new System.Exception(""+e.Message.ToString());
        }
    }

    public static void Close()
    {
        if (myConnection != null)
        {
            myConnection.Close();
            myConnection.Dispose();
            myConnection = null;
        }
    }
}
