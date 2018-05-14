function makeRatable() {
    $('.ui.rating').rating();

    function request(kanbanDetail) {
        var url = "/api/forms/config/kanban-detail/add-or-edit";

        var form = [];
        form.push(null);
        form.push(kanbanDetail);
        form.push(null);

        var data = JSON.stringify(form);

        return getAjaxRequest(url, "POST", data);
    };

    $('.ui.rating i').dblclick(function () {
        var el = $(this);
        var card = el.parent().parent().parent();
        var kanbanDetailId = window.parseInt(card.attr("data-kanban-detail-id") || null);

        if (kanbanDetailId) {
            var kanbanDetail = new Object();
            kanbanDetail.KanbanDetailId = kanbanDetailId;
            kanbanDetail.KanbanId = window.parseInt(card.closest(".segment").attr("id").replace("kanban", "") || null);
            kanbanDetail.ResourceId = card.attr("data-key");
            kanbanDetail.Rating = el.parent().find("i.active").length;

            request(kanbanDetail);
        };
    });
};
