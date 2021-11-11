using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class SQLRequests
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\fabia\source\repos\Fabi12345678\Bibliothek\WebApplication3\App_Data\Database.accdb;Persist Security Info=True");

        

        public string LoadUsers()
        {
            string options = "";
            string cmd = "SELECT nutzername FROM nutzer";
            connection.Open();
            // SQL shit
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();
            while (reader.Read())
            {
                Debug.Print(reader[0].ToString());
            }
            reader.Close();
            connection.Close();

            

            return options;
        }
    }
}