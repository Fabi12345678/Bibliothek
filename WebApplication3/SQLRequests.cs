using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
            //string cmd = "SELECT name FROM nutzer";
            //SqlCommand selectCommand = new SqlCommand(cmd, sqlConnection);
            //sqlConnection.Open();
            //// SQL shit
            //SqlDataReader reader = selectCommand.ExecuteReader();
            //while (reader.Read())
            //{
            //    Debug.Print(reader[0].ToString());
            //}
            //sqlConnection.Close();

            string queryString =
            "SELECT name FROM nutzer";

            using (SqlConnection connection =
                       new SqlConnection(@"Data Source=.\SQLEXPRESS;
                          AttachDbFilename=C:\Users\user\Source\Repos\Fabi12345678\Bibliothek\WebApplication3\App_Data\Database.mdf;
                          Integrated Security=True;
                          Connect Timeout=30;
                          User Instance=True"))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    ReadSingleRow((IDataRecord)reader);
                }

                // Call Close when done reading.
                reader.Close();
            }

            return options;
        }

        private static void ReadSingleRow(IDataRecord dataRecord)
        {
            Console.WriteLine(String.Format("{0}, {1}", dataRecord[0], dataRecord[1]));
        }
    }
}