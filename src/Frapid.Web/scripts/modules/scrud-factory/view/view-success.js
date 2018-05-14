function onViewSuccess(response) {
    window.scrudjson = response;
    var el = $("#scrudjson");
    createGridView(el, window.scrudjson);
    loadQueryStringFilters();
    localizeHeaders(el);
    gridFilterActions();
    loadColumns();
    loadActions();
    loadButtons();
    triggerViewReadyEvent();
    $(".view.dimmer").removeClass("active");
    createCards();
    $(".view.factory").show();
    $(document).trigger("viewsuccess");
};

function loadQueryStringFilters() {
    getQuerystringFilters();
};

function clearQuery() {
    var url = document.location.pathname;
    window.history.replaceState({ path: url }, '', url);
    loadPageCount(loadGrid);
};

function gridFilterActions() {
    $("input.grid.filter").change(function() {
        window.gridFilterActionData = "";
        var url = document.location.pathname + document.location.search;

        var els = $("input.grid.filter");
        $.each(els, function() {
            var el = $(this);
            var val = el.val();
            var member = el.attr("data-member");

            if (val) {
                url = updateQueryString(member, val, url);
            } else {
                url = removeURLParameter(url, member);
            };
        });

        window.history.replaceState({ path: url }, '', url);
        loadPageCount(loadGrid);
    });
};

function removeURLParameter(url, parameter) {
    var urlparts = url.split('?');
    if (urlparts.length >= 2) {

        var prefix = encodeURIComponent(parameter) + '=';
        var pars = urlparts[1].split(/[&;]/g);

        //reverse iteration as may be destructive
        for (var i = pars.length; i-- > 0;) {
            //idiom for string.startsWith
            if (pars[i].lastIndexOf(prefix, 0) !== -1) {
                pars.splice(i, 1);
            }
        }

        url = urlparts[0] + '?' + pars.join('&');
        return url;
    } else {
        return url;
    }
}