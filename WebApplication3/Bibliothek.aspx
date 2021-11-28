<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Bibliothek.aspx.cs" Inherits="WebApplication3.Bibliothek" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Bibliothek-App</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <link rel="stylesheet" href="CSS/StyleSheet.css"/>
    <link rel="stylesheet" href="//cdn.datatables.net/1.11.3/css/jquery.dataTables.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/searchpanes/1.4.0/css/searchPanes.dataTables.min.css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.3/css/select.dataTables.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="js/Admin.js"></script>
    <script src="js/DataTable.js"></script>
    <script src="//cdn.datatables.net/1.11.3/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/searchpanes/1.4.0/js/dataTables.searchPanes.min.js"></script>
    <script src="https://cdn.datatables.net/select/1.3.3/js/dataTables.select.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="header">
                Bibliothek
            </div>
            <div class="center">
                <div class="tab is-active" data-tab="book-list-container">Bücherliste</div>
                <div class="tab" data-tab="user-container">Nutzer</div>
            </div>
            <div class="bib-tab user-container">
                <div style="float:right; width:100%;">
                    <select style="min-width:15%; float:right; margin-right:50px">
                        <%= CallLoadUsers() %>
                    </select>
                </div>
            </div>

            <div class="user-container">
                <div style="width:100%; text-align: center;">
                    <!-- <asp:TextBox ID="tbxSearchBox" runat="server"></asp:TextBox>
                    <asp:Button ID="SearchButton" runat="server" Text="Buch Suchen"/> -->
                </div>
            </div>

            <div class="bib-tab book-list-container col-12 mt-5">
                <table id="table-book-list" class="table table-striped table-bordered table-hover">
                    <thead class="thead-dark">
                        <tr>
                            <th>Nummer</th>
                            <th>Autor</th>
                            <th>Titel</th>
                            <th>Rückgabedatum</th>
                            <th>Anzahl Verlängerungen</th>
                            <th>Ausgeliehen von</th>
                        </tr>
                    </thead>
                    <tbody>
                        <%= CallLoadBooks() %>
                    </tbody>
                </table>
            </div>

        </div>
    </form>
</body>
</html>
<script>
    $(() => {
        let dataTable = new BookListDataTable();
        let admin = new Admin();
    })
</script>
