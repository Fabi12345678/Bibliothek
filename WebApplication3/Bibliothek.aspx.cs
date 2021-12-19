using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Bibliothek : System.Web.UI.Page
    {
        //OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\thowe\Source\Repos\Bibliothek\WebApplication3\App_Data\Database.accdb;Persist Security Info=True");
        OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\fabia\Source\Repos\Fabi12345678\Bibliothek\WebApplication3\App_Data\Database.accdb;Persist Security Info=True");
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //CreateBookExamples();
            if (!IsPostBack)
            {
                CallLoadUsers();
                List<string> availableBooks = new List<string>();
                availableBooks.Clear();
                SQLRequests sqlRequests = new SQLRequests();
                availableBooks = sqlRequests.LoadAvailableBooks();
                AvailableBooks.Items.Clear();
                int index = 0;
                foreach (string user in availableBooks)
                {
                    AvailableBooks.Items.Insert(index, new ListItem(user.Split(';')[0], user.Split(';')[1]));
                    index++;
                }
            }
            
            

        }
        protected string CallLoadUserInfos()
        {
            SQLRequests sqlRequest = new SQLRequests();
            if (IsPostBack)
                return sqlRequest.LoadUserInfo(Request.Form["UserSelect"]);
            else
                return "";
        }
        
        protected void CallLoadUsers()
        {
            List<string> userList = new List<string>();
            SQLRequests sqlRequests = new SQLRequests();
            userList = sqlRequests.LoadUsers2();
            int index = 0;
            foreach (string user in userList)
            {
                //1=id
                UserSelect.Items.Insert(index, new ListItem(user.Split(';')[1], user.Split(';')[0]));
                index++;
            }
        }

        protected string CallLoadBooks()
        {
            SQLRequests sqlRequest = new SQLRequests();

            return sqlRequest.LoadBooks();
        }

        /**
         * Erstellt alle Testeinträge in die Büche Tabelle.
         * Falls es zu Fehler kommt, den "DROP TABLE" cmd nicht ausführen.
         */
        private void CreateBookExamples()
        {
            connection.Open();
            string cmdDrop = "drop table buch;";
            OleDbCommand selectCmdDrop = new OleDbCommand(cmdDrop, connection);
            selectCmdDrop.ExecuteNonQuery();

            string cmd = "create table buch (nummer text,autor text,titel text,ausgeliehen_von int,rueckgabedatum date,anzahl_verl int); ";
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            selectCmd.ExecuteNonQuery();

            string[,] books = new string[19, 3] {
                {"111-111", "J.K. Rowling", "Harry Potter und der Stein der Weisen"},
                {"112-142", "J.K. Rowling", "Harry Potter und die Kammer des Schreckens"},
                {"113-183", "J.K. Rowling", "Harry Potter und der Gefangene von Askaban"},
                {"114-111", "J.K. Rowling", "Harry Potter und der Feuerkelch"},
                {"115-134", "J.K. Rowling", "Harry Potter und der Orden des Phoenix"},
                {"116-432", "J.K. Rowling", "Harry Potter und der Halbblutprinz"},
                {"117-345", "J.K. Rowling", "Harry Potter und die Heiligtümer des Todes"},
                {"118-131", "Jesus oder so", "Die Bibel"},
                {"119-111", "Antoinse de Saint-Exupery", "Der kleine Prinz"},
                {"120-231", "Anne Frank", "Tagebuch"},
                {"121-231", "F. Scott Fitzgerald", "Der große Gatsby"},
                {"122-461", "George Orwell", "1984"},
                {"123-131", "Waris Dirie", "Wüstenblume"},
                {"124-424", "Ernest Hemingway", "Der alte Mann und das Meer"},
                {"125-843", "Erich Maria Remarque", "Im Westen nichts Neuess"},
                {"126-123", "Frank Schätzing", "Der Schwarm"},
                {"127-345", "Margaret Atwood", "Der Report der Magd"},
                {"128-375", "Lewis Carroll", "Alice im Wunderland"},
                {"129-241", "Patricia Highsmith", "Zwei Fremde im Zug"},
            };

            for (int i = 0; i < 19; i++)
            {
                string cmdInsert = "insert into buch values ('" + books[i, 0] + "','" + books[i, 1] + "','" + books[i, 2] + "',null,null,null);";
                OleDbCommand selectCmdInsert = new OleDbCommand(cmdInsert, connection);
                selectCmdInsert.ExecuteNonQuery();
            }

            connection.Close();
        }

        protected void UserSearchButton_Click(object sender, EventArgs e)
        {
            BorrowedBooks.Items.Clear();
            string cmd = "SELECT b.titel, b.anzahl_verl, b.nummer FROM buch as b WHERE b.ausgeliehen_von LIKE '" + UserSelect.SelectedValue + "'";
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();
            int index = 0;
            while (reader.Read())
            {
                BorrowedBooks.Items.Insert(index, new ListItem(reader[0].ToString() + " - " + reader[1].ToString() , reader[2].ToString()));
                index++;
            }
            reader.Close();
            connection.Close();
        }

        protected void AusleihButton_Click(object sender, EventArgs e)
        {
            string userID = UserSelect.SelectedValue;
            string bookID = AvailableBooks.SelectedValue;
            SQLRequests sqlRequest = new SQLRequests();
            string errorMessage = sqlRequest.BorrowBook(userID, bookID);
            ErrorLabel.Text = errorMessage;
            List<string> availableBooks = new List<string>();
            availableBooks.Clear();
            SQLRequests sqlRequests = new SQLRequests();
            availableBooks = sqlRequests.LoadAvailableBooks();
            AvailableBooks.Items.Clear();
            int index = 0;
            foreach (string user in availableBooks)
            {
                AvailableBooks.Items.Insert(index, new ListItem(user.Split(';')[0], user.Split(';')[1]));
                index++;
            }

            BorrowedBooks.Items.Clear();
            string cmd = "SELECT b.titel, b.anzahl_verl, b.nummer FROM buch as b WHERE b.ausgeliehen_von LIKE '" + UserSelect.SelectedValue + "'";
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();
            index = 0;
            while (reader.Read())
            {
                BorrowedBooks.Items.Insert(index, new ListItem(reader[0].ToString() + " - " + reader[1].ToString(), reader[2].ToString()));
                index++;
            }
            reader.Close();
            connection.Close();
        }

        protected void VerlängernButton_Click(object sender, EventArgs e)
        {
            SQLRequests sqlRequests = new SQLRequests();
            string errorMessage = sqlRequests.ExpandBook(BorrowedBooks.SelectedValue);
            ErrorLabel.Text = errorMessage;
            BorrowedBooks.Items.Clear();
            string cmd = "SELECT b.titel, b.anzahl_verl, b.nummer FROM buch as b WHERE b.ausgeliehen_von LIKE '" + UserSelect.SelectedValue + "'";
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();
            int index = 0;
            while (reader.Read())
            {
                BorrowedBooks.Items.Insert(index, new ListItem(reader[0].ToString() + " - " + reader[1].ToString(), reader[2].ToString()));
                index++;
            }
            reader.Close();
            connection.Close();
        }

        protected void Zurückgeben_Click(object sender, EventArgs e)
        {
            SQLRequests sqlRequests = new SQLRequests();
            string errorMessage = sqlRequests.ReturnBook(BorrowedBooks.SelectedValue,UserSelect.SelectedValue);
            ErrorLabel.Text = errorMessage;
            BorrowedBooks.Items.Clear();
            string cmd = "SELECT b.titel, b.anzahl_verl, b.nummer FROM buch as b WHERE b.ausgeliehen_von LIKE '" + UserSelect.SelectedValue + "'";
            connection.Open();
            OleDbCommand selectCmd = new OleDbCommand(cmd, connection);
            OleDbDataReader reader = selectCmd.ExecuteReader();
            int index = 0;
            while (reader.Read())
            {
                BorrowedBooks.Items.Insert(index, new ListItem(reader[0].ToString() + " - " + reader[1].ToString(), reader[2].ToString()));
                index++;
            }
            reader.Close();
            connection.Close();

            List<string> availableBooks = new List<string>();
            availableBooks.Clear();
            availableBooks = sqlRequests.LoadAvailableBooks();
            AvailableBooks.Items.Clear();
            index = 0;
            foreach (string user in availableBooks)
            {
                AvailableBooks.Items.Insert(index, new ListItem(user.Split(';')[0], user.Split(';')[1]));
                index++;
            }
        }

        protected void Reservieren_Click(object sender, EventArgs e)
        {
            SQLRequests sqlRequests = new SQLRequests();
            string errorMessage = sqlRequests.ReserveBook(AvailableBooks.SelectedValue, UserSelect.SelectedValue);
            ErrorLabel.Text = errorMessage;
        }
    }
}