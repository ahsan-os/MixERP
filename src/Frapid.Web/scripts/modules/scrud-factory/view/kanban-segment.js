var kanbanTemplate = '<div id="kanban{KanbanId}" class="ui segment">\
                    <span class="ui teal left large label" data-kanban-id="{KanbanId}" title="{Description}">{KanbanName}</span>\
                    <div class="ui right floated tiny basic icon buttons">\
                        <a title="{AddNewCheckListLocalized}" class="ui basic icon button" href="javascript:void(0);" onclick="addKanban();"><i class="add icon"></i></a>\
                        <a title="{EditThisCheckListLocalized}" class="ui basic icon button" href="javascript:void(0);" onclick="editKanban(this);"><i class="edit icon"></i></a>\
                        <a title="{DeleteThisCheckListLocalized}" class="ui basic icon button" href="javascript:void(0);" onclick="deleteKanban(this);"><i class="delete icon"></i></a>\
                    </div>\
                    <div class="ui clearing padded divider"></div> \
                    <div class="kanban holder"></div> \
                </div>';

function createKanbanSegment(kanban) {
    var local = kanbanTemplate;
    local = local.replace(/{KanbanId}/g, kanban.KanbanId);
    local = local.replace(/{KanbanName}/g, kanban.KanbanName);
    local = local.replace(/{Description}/g, kanban.Description);
    local = local.replace("{AddNewCheckListLocalized}", window.translate("AddNewChecklist"));
    local = local.replace("{EditThisCheckListLocalized}", window.translate("EditThisChecklist"));
    local = local.replace("{DeleteThisCheckListLocalized}", window.translate("DeleteThisChecklist"));

    var el = $(local);

    $("#kanban").append(el);
};

function initializeKanbanUI(){
    var totalKanbans = $("#kanban .ui.segment").length;

    if(totalKanbans === 1){
        $("#kanban").css("width", "100%");
        $("#kanban0").attr("style", "max-width:100%!important;width:100%!important;");
        $("<style id='emptykanbanStyle' type='text/css'>.kanban.holder .card{ display: inline-flex; margin: 1em;}</style>").appendTo("head");
        return;
    };

    $("#emptykanbanStyle").remove();
};

function createKanbans(kanbans) {
    $("#kanban").html("");

    var kanban = new Object();
    kanban.KanbanId = "0";
    kanban.KanbanName = window.i18n.Untitled;
    createKanbanSegment(kanban);

    $.each(kanbans, function (i, kanban) {
        createKanbanSegment(kanban);
    });

    createCards();
    initializeKanbanUI();
};

function getKanbans() {
    var url = "/api/forms/config/kanbans/get-where/1";

    var filters = [];
    filters.push(getAjaxColumnFilter("WHERE", "ObjectName", "string", FilterConditions.IsEqualTo, window.scrudFactory.viewTableName));
    filters.push(getAjaxColumnFilter("WHERE", "UserId", "int", FilterConditions.IsEqualTo, window.parseInt(window.userId)));

    var data = JSON.stringify(filters);

    return getAjaxRequest(url, "POST", data, false);
};

function isKanban() {
    var target = window.getQueryStringByName("View");
    return (target || "").toLowerCase() === "kanban";
};

function refreshKanbans(dontRefresh) {
    var ajax = getKanbans();

    ajax.success(function (response) {
        window.kanbans = response;

        //if (!window.kanbans.length) {
        //    if (!isKanban()) {
        //        var defaultView = window.getQueryStringByName("View") || "kanban";
        //        showView(defaultView, dontRefresh);
        //    };
        //};

        var defaultView = window.getDefaultScrudView();
        showView(defaultView, dontRefresh);

        createKanbans(response);
    });
};
