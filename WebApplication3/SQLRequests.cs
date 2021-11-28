using System.Data.OleDb;

namespace WebApplication3
{
    public class SQLRequests
    {
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\thowe\Source\Repos\Bibliothek\WebApplication3\App_Data\Database.accdb;Persist Security Info=True");

        public string LoadUsers()
        {
            string options = "";
            string cmd = "SELECT nutzername, nummer FROM nutzer";
            connection.Open();
            // SQL shit
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

        public string LoadBooks()
        { 
            string value = "";
            string cmd = "SELECT nummer,autor,titel,ausgeliehen_von,rueckgabedatum,anzahl_verl FROM buch";
            connection.Open();
            // SQL shit
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                value += "<tr><td>" + reader[0].ToString() + "</td><td>" + reader[1].ToString() + "</td><td>" + reader[2].ToString() + "</td><td>" + reader[3].ToString() + "</td><td>" + reader[4].ToString() + "</td><td>" + reader[5].ToString() + "</td></tr>";

                //<option value='" + reader[1].ToString() + "'>" + reader[0].ToString() + "</option>
            }

            reader.Close();
            connection.Close();

            return value;
        }
    }
}