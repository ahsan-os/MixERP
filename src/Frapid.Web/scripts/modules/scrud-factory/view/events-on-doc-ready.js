function prepareScrudView() {
    $("#ExportDropDown").dropdown();
    window.scrudForm = $(".form.factory");
    window.scrudView = $(".view.factory");

    var defaultView = window.getDefaultScrudView();
    if ((defaultView || "") === "form-view") {
        scrudView.hide();
    };


    loadMeta(loadFilterNames);
    loadFilterConditions();

    if (window.scrudFactory.title) {
        $(".scrud.title").html(window.scrudFactory.title);

        var titleSuffix = getQueryStringByName("TitleSuffix");
        if(titleSuffix){
            $(".scrud.title").append(" / " + titleSuffix);
        };
    };
    if (window.scrudFactory.description) {
        $("#scrud-factory-description").html(window.scrudFactory.description).show();
    } else {
        $("#scrud-factory-description").remove();
    };

    if (typeof (window.scrudFactory.back) === "object") {
        var title = window.scrudFactory.back.Title;
        var url = window.scrudFactory.back.Url;

        var anchor = $("<a/>");
        anchor.addClass("ui basic button");
        anchor.attr("title", window.translate("Back"));
        anchor.attr("href", url);
        anchor.text(title);

        $("#AddNewButton").before(anchor);
    };

    initializeViews();
    initializeCustomButtons();

    var view = getQueryStringByName("View");

    if (view) {
        showTarget(view);
    };

    $(".kanban.segments").css("width", (($(".segment").length - 1) * 300) + "px").show();
    refreshKanbans(true);
};
