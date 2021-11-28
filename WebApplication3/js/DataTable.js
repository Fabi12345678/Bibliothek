class BookListDataTable {
    /**
     * Konstruktor der Klasse
     * */
    constructor() {
        this.initDataTable();
    }

    /**
     * Initialisiert DataTables auf der BücherTabelle
     * */
    initDataTable() {
        $('#table-book-list').DataTable({
            "language": {
                "lengthMenu": "",
                "zeroRecords": "Leider keine Einträge gefunden...",
                "search": "Suche nach:",
                "infoEmpty": "",
                "infoFiltered": "",
                "info": "",
                "paginate": {
                    "first": "erste",
                    "last": "letzte",
                    "next": "vor",
                    "previous": "zurück"
                }
            },
            "lengthMenu": [8],
            "columnDefs": [
                {
                    "searchPanes": {
                        "show": true
                    },
                    "targets": [1, 2, 3, 4, 5],
                }
            ],
            "searchPanes": {
                "layout": 'columns-5',
                "viewTotal": true,
                "emptyMessage": 'nicht angegeben',
            },
            "order": [0, 'asc'],
            "dom": 'Plfrtip',
            "drawCallback": () => {
                this.cleanUpTable();
            },
            "initComplete": () => {
                this.cleanUpTable();
            }
        });
    }

    cleanUpTable() {
        $('div.dtsp-titleRow').remove();
    }
}