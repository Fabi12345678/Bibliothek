using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class Bibliothek : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Abfrage ob Testdaten in DB sind wenn nicht Testdaten befüllen
        }

        protected string CallLoadUsers()
        {
            SQLRequests sqlRequests = new SQLRequests();
            string options = sqlRequests.LoadUsers();
            return options;
        }
    }
}