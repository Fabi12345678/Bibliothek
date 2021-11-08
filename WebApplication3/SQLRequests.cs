using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication3
{
    public class SQLRequests
    {
        private SqlConnection sqlConnection = new SqlConnection(@"Data Source=.\SQLEXPRESS;
                          AttachDbFilename=C:\Users\user\Source\Repos\Fabi12345678\Bibliothek\WebApplication3\App_Data\Database.mdf;
                          Integrated Security=True;
                          Connect Timeout=30;
                          User Instance=True");
        
        public string LoadUsers()
        {
            string options = "";
            string cmd = "";
            SqlCommand selectCommand = new SqlCommand(cmd, sqlConnection);
            sqlConnection.Open();
            // SQL shit
            selectCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return options; 
        }
    }
}