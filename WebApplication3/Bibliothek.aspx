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
                <div class="tab is-active" data-tab="user-container">Nutzer</div>
                <div class="tab" data-tab="book-list-container">Bücherliste</div>
            </div>
            <div class="bib-tab user-container col-12">
                <div style="float:right; width:100%;" class="col-12">
                    <asp:DropDownList ID="UserSelect" runat="server" CssClass="my-3 min-width15">
                    </asp:DropDownList>
                    <asp:Button ID="UserSearchButton" runat="server" Text="Suchen" OnClick="UserSearchButton_Click"/>
                    <div class="user-data-container row"style=" margin-top:20px;">
                        <div class="col-12">
                            <div class="card text-white bg-secondary mb-3 col-3" style="max-width: 18rem;">
                                <div class="card-header">Personendaten<a class="info-name"></a></div>
                                <div class="card-body">
                                    <%= CallLoadUserInfos() %>
                                </div>
                            </div>
                            <div class="col-9">
                                <table id="user-book-list" class="table table-striped table-bordered table-hover">
                                    <thead class="thead-dark">
                                        <tr>
                                            <th>Nummer</th>
                                            <th>Autor</th>
                                            <th>Titel</th>
                                            <th>Rückgabedatum</th>
                                            <th>Anzahl Verlängerungen</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <%= CallLoadUserBooks() %>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="col-12">
                            <asp:DropDownList ID="AvailableBooks" runat="server"></asp:DropDownList>
                            <asp:Button ID="AusleihButton" runat="server" Text="Ausleihen" OnClick="AusleihButton_Click" />
                            <asp:Button ID="Reservieren" runat="server" Text="Reservieren" OnClick="Reservieren_Click" />
                        </div>
                        <div class="col-12">
                            <asp:DropDownList ID="BorrowedBooks" runat="server"></asp:DropDownList>
                            <asp:Button ID="VerlängernButton" runat="server" Text="Verlängern" OnClick="VerlängernButton_Click" />
                            <asp:Button ID="Zurückgeben" runat="server" Text="Zurückgeben" OnClick="Zurückgeben_Click" />
                        </div>
                    </div>
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
            <div class="col-12 my-3 text-danger">
                <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
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
