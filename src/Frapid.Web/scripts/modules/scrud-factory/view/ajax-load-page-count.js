function loadPageCount(callback) {
    function filteredRequest(filters) {
        var url = window.scrudFactory.viewAPI + "/count-where";
        var data = JSON.stringify(filters);
        return getAjaxRequest(url, "POST", data);
    };

    function request(filterName, filters) {
        if (!filters && getFilterQueryStringCount()) {
            filters = getQuerystringFilters();

            return filteredRequest(filters);
        };

        var url = window.scrudFactory.viewAPI + "/count";

        if (filterName) {
            url = window.scrudFactory.viewAPI + "/count-filtered/" + filterName;
        };

        if (!filters || !filters.length) {
            return getAjaxRequest(url);
        };

        url = window.scrudFactory.viewAPI + "/count-where";
        var data = JSON.stringify(filters);

        return getAjaxRequest(url, "POST", data);
    };

    $(".view.dimmer").addClass("active");

    if (checkIfProcedure()) {
        if (typeof callback === "function") {
            callback();
        };

        return;
    };

    var filterName = getFilterName();
    var filters = getSelectedFilter();

    var ajax = request(filterName, filters);

    ajax.success(function (response) {
        var pages = (Math.ceil(window.parseInt(response) / 10) || 1);

        $(".total.pages.anchor").text(pages);

        if (typeof callback === "function") {
            callback();
        };
    });

    ajax.fail(function (xhr) {
        logAjaxErrorMessage(xhr);
    });
};
