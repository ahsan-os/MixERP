function getPageNumber() {
    var page = window.parseInt(getQueryStringByName("Page") || 0);
    var totalPage = window.parseInt($(".total.pages.anchor:first").text() || 1);

    if (!page) {
        return 1;
    };

    if (page > totalPage) {
        page = totalPage;
    };

    return page;
};

function setPageNumber(pageNumber) {
    var url = updateQueryString("Page", pageNumber);
    window.history.replaceState({ path: url }, '', url);
};

function showNextPage() {
    var totalPage = window.parseInt($(".total.pages.anchor:first").text() || 1);
    var nextPage = getPageNumber() + 1;

    if (nextPage > totalPage) {
        nextPage = totalPage;
    };

    setPageNumber(nextPage);

    loadPageCount(loadGrid);
};


function showPreviousPage() {
    var previousPage = getPageNumber() - 1;

    if (previousPage < 1) {
        previousPage = 1;
    };

    setPageNumber(previousPage);

    loadPageCount(loadGrid);
};