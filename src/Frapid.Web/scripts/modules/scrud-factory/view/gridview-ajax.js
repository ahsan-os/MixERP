function loadGrid() {
    function filteredRequest(pageNumber, filters) {
        var url = window.scrudFactory.viewAPI + "/get-where/" + pageNumber;
        var data = JSON.stringify(filters);
        return getAjaxRequest(url, "POST", data);
    };

    function request(pageNumber, filterName, filters) {
        if (!filters && getFilterQueryStringCount()) {
            filters = getQuerystringFilters();

            return filteredRequest(pageNumber, filters);
        };

        var url = window.scrudFactory.viewAPI + "/page/" + pageNumber;

        if (filterName) {
            url = window.scrudFactory.viewAPI + "/get-filtered/" + pageNumber + "/" + filterName;
        };

        if (!filters || !filters.length) {
            return getAjaxRequest(url);
        };

        url = window.scrudFactory.viewAPI + "/get-where/" + pageNumber;
        var data = JSON.stringify(filters);
        return getAjaxRequest(url, "POST", data);
    };

    var pageNumber = getPageNumber();
    var filterName = getFilterName();

    if (checkIfProcedure()) {
        $(".view.dimmer").removeClass("active");
        $("#Pager").remove();
        loadAnnotation();
        return;
    };

    $(".current.page.anchor").text(pageNumber);
    var filters = getSelectedFilter();

    var ajax = request(pageNumber, filterName, filters);

    ajax.success(function (response) {
        onViewSuccess(response);
    });

    ajax.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
};