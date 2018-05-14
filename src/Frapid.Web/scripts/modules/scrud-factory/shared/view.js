function getDefaultScrudView(){
    var defaultView = "kanban";
    
    if(window.defaultScrudView){
        defaultView = window.defaultScrudView;
    };

    var queryStringView = window.getQueryStringByName("View");

    if(queryStringView){
        return queryStringView;
    };

    return defaultView;
};

function showView(target, dontRefresh) {
    if (!target) {
        target = $(".active[data-target]").attr("data-target") || "kanban";
    };

    if (target === "form-view") {
        window.scrudView.hide();
        window.scrudForm.show();
    } else {
        //var url = updateQueryString("View", target);
        //window.history.replaceState({ path: url }, '', url);
        window.scrudForm.hide();
        $("div[data-target]").hide();
        $("[data-target]").removeClass("active green");
        $('[data-target="' + target + '"]').show().addClass("active green");
        window.scrudView.show();

        if (!dontRefresh) {
            loadPageCount(loadGrid);
        };
    };
};