function makeSortable() {
    $(function () {
        function deleteRequest(kanbanDetailId) {
            var url = "/api/forms/config/kanban-details/delete/" + kanbanDetailId;

            return getAjaxRequest(url, "DELETE");
        };

        function request(kanbanDetail) {
            var url = "/api/forms/config/kanban-details/add-or-edit";

            var form = [];
            form.push(null);
            form.push(kanbanDetail);
            form.push(null);

            var data = JSON.stringify(form);

            return getAjaxRequest(url, "POST", data);
        };

        $(".kanban.holder").sortable({
            connectWith: ".kanban.holder",
            placeholder: "kanban-placeholder ui-corner-all",
            receive: function (event, ui) {
                var card = $(ui.item[0]);

                var kanbanDetail = new Object();
                kanbanDetail.KanbanDetailId = window.parseInt(card.attr("data-kanban-detail-id") || null);
                kanbanDetail.KanbanId = window.parseInt(card.parent().parent().attr("id").replace("kanban", "") || 0);
                kanbanDetail.Rating = card.find(".rating .active.icon").length;
                kanbanDetail.ResourceId = card.attr("data-key");

                if (kanbanDetail.KanbanId) {
                    var ajax = request(kanbanDetail);

                    ajax.success(function (msg) {
                        card.attr("data-kanban-detail-id", msg);
                    });
                } else {
                    if (kanbanDetail.KanbanDetailId) {
                        var deleteAjax = deleteRequest(kanbanDetail.KanbanDetailId);

                        deleteAjax.success(function () {
                            card.removeAttr("data-kanban-detail-id");
                        });
                    };
                };

            }
        }).disableSelection();
    });
};