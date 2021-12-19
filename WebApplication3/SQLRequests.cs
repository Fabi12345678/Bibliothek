using System.Collections.Generic;
using System.Data.OleDb;

namespace WebApplication3
{
    public class SQLRequests
    {
        //OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\thowe\Source\Repos\Bibliothek\WebApplication3\App_Data\Database.accdb;Persist Security Info=True");
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\fabia\Source\Repos\Fabi12345678\Bibliothek\WebApplication3\App_Data\Database.accdb;Persist Security Info=True");
        public string LoadUsers()
        {
            string options = "";
            string cmd = "SELECT nutzername, nummer FROM nutzer";
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                options += "<option value='"+ reader[1].ToString() + "'>" + reader[0].ToString() + "</option>";
            }

            reader.Close();
            connection.Close();

            

            return options;
        }
        public List<string> LoadUsers2()
        {
            List<string> userList = new List<string>();
            userList.Clear();
            string cmd = "SELECT nutzername, nummer FROM nutzer";
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                userList.Add(reader[1].ToString() + ";" + reader[0].ToString());
            }

            reader.Close();
            connection.Close();
            return userList;
        }

        public string LoadBooks()
        { 
            string value = "";
            string cmd = "SELECT nummer,autor,titel,rueckgabedatum,anzahl_verl,ausgeliehen_von FROM buch";
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                value += "<tr><td>" + reader[0].ToString() + "</td><td>" + reader[1].ToString() + "</td><td>" + reader[2].ToString() + "</td><td>" + reader[3].ToString() + "</td><td>" + reader[4].ToString() + "</td><td>" + reader[5].ToString() + "</td></tr>";
            }

            reader.Close();
            connection.Close();

            return value;
        }

        public string LoadUserInfo(string id)
        {
            string value = "";
            string cmd = "SELECT n.nummer, n.nutzername, n.gebuehrenstand, a.nummer, a.zuletzt_gueltig, a.gesperrt FROM nutzer as n LEFT JOIN ausweis as a ON n.nummer = a.nutzer_nummer WHERE n.nummer = " + id;
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                value += "<table><tr><td>" + reader[0].ToString() + "</td><td>" + reader[1].ToString() + "</td><td>" + reader[2].ToString() + "</td><td>" + reader[3].ToString() + "</td><td>" + reader[4].ToString() + "</td><td>" + reader[5].ToString() + "</td></tr></table>";
            }

            reader.Close();
            connection.Close();

            return value;
        }
    }
}