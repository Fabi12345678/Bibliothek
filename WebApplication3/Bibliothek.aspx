<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bibliothek.aspx.cs" Inherits="WebApplication3.Bibliothek" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Bibliothek-App</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <link rel="stylesheet" href="CSS/StyleSheet.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="js/JavaScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center><h1>Bibliothek</h1></center>
            <div style="float:right; width:100%;">
                <select style="min-width:15%; float:right; margin-right:50px">
                    <%=CallLoadUsers() %>
                </select>
            </div>
            <div class="user-container">
                <div style="width:100%; text-align: center;">
                    <asp:TextBox ID="tbxSearchBox" runat="server"></asp:TextBox><asp:Button ID="SearchButton" runat="server" Text="Suchen"/>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
