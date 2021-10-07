<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bibliothek.aspx.cs" Inherits="WebApplication3.Bibliothek" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center><h1>Bibliothek</h1></center>
            <div style="float:right; width:100%">
                <select style="min-width:15%; float:right; margin-right:50px">
                    <%=CallLoadUsers() %>
                </select>
            </div>
        </div>
    </form>
</body>

</html>
