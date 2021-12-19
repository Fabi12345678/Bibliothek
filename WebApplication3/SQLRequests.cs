using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;

namespace WebApplication3
{
    public class SQLRequests
    {
        //OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\thowe\Source\Repos\Bibliothek\WebApplication3\App_Data\Database.accdb;Persist Security Info=True");
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\fabia\Source\Repos\Fabi12345678\Bibliothek\WebApplication3\App_Data\Database.accdb;Persist Security Info=True");
        
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
            string cmd = "SELECT n.nummer, n.nutzername, n.gebuehrenstand, a.nummer, a.zuletzt_gueltig, a.gesperrt FROM nutzer as n INNER JOIN ausweis as a ON n.nummer = a.nutzer_nummer WHERE n.nummer = " + id;
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                value += "Nutzernummer: " + reader[0].ToString() + "<br>Nutzername: " + reader[1].ToString() + "<br>Gebuehrenstand in €: " + reader[2].ToString() + "<br>Ausweisnummer: " + reader[3].ToString() + "<br>Ausweis zuletzt Gültig: " + reader[4].ToString() + "<br>Ausweis gesperrt: " + reader[5].ToString();
            }

            reader.Close();
            connection.Close();

            return value;
        }

        public List<string> LoadAvailableBooks()
        {
            List<string> availableBooks = new List<string>();
            availableBooks.Clear();
            string cmd = "SELECT nummer,titel FROM buch WHERE buch.ausgeliehen_von is NULL";
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();

            while (reader.Read())
            {
                availableBooks.Add(reader[1].ToString() + ";" + reader[0].ToString());
            }

            reader.Close();
            connection.Close();

            return availableBooks;
        }

        public string BorrowBook(string userID, string bookID)
        {
            connection.Open();
            string select = "SELECT reserviert_von FROM buch WHERE nummer like '" + bookID + "'";
            OleDbCommand selectCMD = new OleDbCommand(select,connection);
            OleDbDataReader reader = selectCMD.ExecuteReader();

            while (reader.Read())
            {
                if(reader[0].ToString() == "")
                {
                    DateTime date = DateTime.Now.AddDays(21);
                    string cmd = "UPDATE buch SET ausgeliehen_von =" + userID + ", anzahl_verl = 0, rueckgabedatum = '" + date + "' WHERE nummer like '" + bookID + "'";
                    OleDbCommand insertCMD = new OleDbCommand(cmd, connection);
                    insertCMD.ExecuteNonQuery();
                    connection.Close();
                    reader.Close();
                    return "";
                }
                else
                {
                    if(reader[0].ToString() == userID)
                    {
                        DateTime date = DateTime.Now.AddDays(21);
                        string cmd = "UPDATE buch SET ausgeliehen_von =" + userID + ", anzahl_verl = 0, rueckgabedatum = '" + date + "' WHERE nummer like '" + bookID + "'";
                        OleDbCommand insertCMD = new OleDbCommand(cmd, connection);
                        insertCMD.ExecuteNonQuery();
                        connection.Close();
                        reader.Close();
                        return "";
                    }
                    else
                    {
                        reader.Close();
                        return "Das Buch wurde bereits von einem anderen Benutzer reserviert und kann daher nicht ausgeliehen werden!";
                    }
                }
            }
            connection.Close();
            return "";
            
        }
        
        public string ExpandBook(string bookID)
        {
            string selectcommand = "SELECT anzahl_verl, rueckgabedatum FROM buch WHERE nummer like '" + bookID + "'";
            connection.Open();
            OleDbCommand selectCMD = new OleDbCommand(selectcommand, connection);
            OleDbDataReader reader = selectCMD.ExecuteReader();
            while (reader.Read())
            {
                if (Int32.Parse(reader[0].ToString()) < 3)
                {
                    DateTime date = DateTime.Parse(reader[1].ToString()).AddDays(7);
                    string updatecommand = "UPDATE buch SET anzahl_verl =" + (Int32.Parse(reader[0].ToString()) + 1) + ", rueckgabedatum = '" + date + "' WHERE nummer like '" + bookID + "'";
                    OleDbCommand updateCMD = new OleDbCommand(updatecommand, connection);
                    updateCMD.ExecuteNonQuery();
                    reader.Close();
                    connection.Close();
                    return "";
                }
                else
                {
                    reader.Close();
                    connection.Close();
                    return "Das Buch wurde bereits 3 mal verlängert und kann daher nicht mehr verlängert werden!";

                }
            }
            reader.Close();
            connection.Close();
            return "";
        }

        public string ReturnBook(string bookID, string userID)
        {
            connection.Open();
            string select = "SELECT rueckgabedatum FROM buch WHERE nummer like '" + bookID + "'";
            OleDbCommand selectCMD = new OleDbCommand(select, connection);
            OleDbDataReader reader = selectCMD.ExecuteReader();
            float stand = 0;
            float newStand = 0;
            string gesperrt = "";

            while (reader.Read()) 
            {
                DateTime date = DateTime.Parse(reader[0].ToString());
                if(date < DateTime.Now)
                {
                    select = "SELECT gebuehrenstand FROM nutzer WHERE nummer like '" + userID + "'";
                    selectCMD = new OleDbCommand(select, connection);
                    OleDbDataReader reader2 = selectCMD.ExecuteReader();
                    while (reader2.Read())
                    {
                        stand = float.Parse(reader2[0].ToString());
                        double diffDays = (DateTime.Now - date).TotalDays;
                        diffDays = diffDays / 7;
                        newStand = stand + Int32.Parse(diffDays.ToString()) * 10;
                        
                        if (newStand >= 50)
                        {
                            string updateStr = "UPDATE ausweis SET gesperrt = true WHERE nutzer_nummer like '" + userID + "'"; ;
                            OleDbCommand updateCommand = new OleDbCommand(updateStr,connection);
                            updateCommand.ExecuteNonQuery();
                        }
                           
                        OleDbCommand insertCMD = new OleDbCommand("UPDATE nutzer SET gebuehrenstand = '" + newStand + "' WHERE nummer like '" + userID + "'", connection);
                        insertCMD.ExecuteNonQuery();
                    }
                    reader2.Close();
                }
            }
            reader.Close();
            string updatecommand = "UPDATE buch SET anzahl_verl = NULL, ausgeliehen_von = NULL, rueckgabedatum = NULL WHERE nummer like '" + bookID + "'";
            OleDbCommand updateCMD = new OleDbCommand(updatecommand, connection);
            updateCMD.ExecuteNonQuery();
            connection.Close();
            string gesperrtText;
            if (gesperrt == "true")
                gesperrtText = "Der Benutzer ist gesperrt";
            else
                gesperrtText = "Der Benutzer ist nicht gesperrt";
            return "Es sind " + (newStand - newStand) + "€ an Gebühren angefallen und somit lautet der aktuelle Gebührenstand: " + newStand + "€. " + gesperrtText;
        }

        public string ReserveBook(string bookID, string userID)
        {
            string select = "SELECT ausgeliehen_von FROM buch WHERE nummer like '" + bookID + "'";
            connection.Open();
            OleDbCommand selectCMD = new OleDbCommand(select, connection);
            OleDbDataReader reader = selectCMD.ExecuteReader();
            while (reader.Read())
            {
                string ausgeliehenVon = reader[0].ToString();
                if(ausgeliehenVon == "")
                {
                    reader.Close();
                    connection.Close();
                    return "Kein anderer Benutzer hat das Buch ausgeliehen, daher kann das Buch nicht reserviert werden.";
                }
                else
                {
                    if (ausgeliehenVon == userID)
                    {
                        reader.Close();
                        connection.Close();
                        return "Das Buch darf nicht reserviert werden, da es vom gleichem Benutzer schon ausgeliehen wurde.";
                        //return darfst ned reservieren weil du hast schon ausgeliehen
                    }
                    else
                    {
                        OleDbCommand updateCMD = new OleDbCommand("UPDATE buch SET reserviert_von = '" + userID + "' WHERE nummer like '" + bookID + "'", connection);
                        updateCMD.ExecuteNonQuery();
                    }
                }
            }
            reader.Close();
            connection.Close();
            return "";
        }
    }
}