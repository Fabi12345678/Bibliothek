class Admin {
    /**
     * Konstruktor der Klasse
     */
    constructor() {
        this.init();
    }

    init() {
        this.tabVisibility('book-list-container');
        this.bindEvents();
    }

    bindEvents() {
        $('div.tab').off('click').on('click', (e) => {
            $('div.tab').removeClass('is-active');
            $(e.target).addClass('is-active');
            this.tabVisibility($(e.target).attr('data-tab'));
        });
    }

    tabVisibility(activeTab) {
        $('div.bib-tab').each(function (_, e) {
            $(e).hide();

            if ($(e).hasClass(activeTab)) {
                $(e).show();
            }
        });
    }
}