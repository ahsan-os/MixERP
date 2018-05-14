$("[data-toggle-allow]").click(function() {
    const el = $(this);

    const state = el.prop("checked");
    const table = el.closest("table");

    if (!state) {
        $("[data-menu-allow]").removeAttr("checked");
        return;
    };

    table.find("[data-menu-allow]").prop("checked", true).trigger("change");
});

$("[data-menu-allow]").change(function() {
    const el = $(this);
    const target = el.parent().parent().find("[data-menu-deny]");
    target.removeAttr("checked");
});

$("[data-menu-deny]").change(function() {
    const el = $(this);
    const target = el.parent().parent().find("[data-menu-allow]");
    target.removeAttr("checked");
});

$("[data-toggle-deny]").click(function() {
    const el = $(this);

    const state = el.prop("checked");
    const table = el.closest("table");

    if (!state) {
        $("[data-menu-deny]").removeAttr("checked");
        return;
    };

    table.find("[data-menu-deny]").prop("checked", true).trigger("change");
});


$(window).keypress(function(event) {
    if (!(event.which === 115 && event.ctrlKey) && !(event.which === 19)) return true;
    save();
    event.preventDefault();
    return false;
});

function save() {
    function request(model) {
        const url = "/dashboard/authorization/menu-access/user-policy";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "PUT", data);
    };

    const confirmed = window.confirmAction();
    if (!confirmed) {
        return;
    };

    const userId = window.parseInt($("#UserSelect").val() || 0);
    const officeId = window.parseInt($("#OfficeSelect").val() || 0);

    if (!userId || !officeId) {
        return;
    };

    const allowed = $("[data-menu-allow]:checked").map(function() {
        return $(this).attr("data-menu-id");
    }).get();

    const disallowed = $("[data-menu-deny]:checked").map(function() {
        return $(this).attr("data-menu-id");
    }).get();

    const model = {
        UserId: userId,
        OfficeId: officeId,
        Allowed: allowed || [],
        Disallowed: disallowed || []
    };

    const ajax = request(model);

    ajax.success(function() {
        window.displaySuccess();
    });

    ajax.fail(function(xhr) {
        window.logAjaxErrorMessage(xhr);
    });
};

$("[data-save-button]").off("click").on("click", function() {
    save();
});

$("[data-get-menu-policy-button]").off("click").on("click", function() {
    function request(officeId, userId) {
        var url = "/dashboard/authorization/menu-access/user-policy/{officeId}/{userId}";
        url = url.replace("{officeId}", officeId);
        url = url.replace("{userId}", userId);

        return window.getAjaxRequest(url);
    };

    const userId = window.parseInt($("#UserSelect").val() || 0);
    const officeId = window.parseInt($("#OfficeSelect").val() || 0);

    if (!userId || !officeId) {
        return;
    };

    $("[data-toggle-allow]").removeAttr("checked");
    $("[data-toggle-deny]").removeAttr("checked");
    $("[data-menu-id]").removeAttr("checked");

    const ajax = request(officeId, userId);

    ajax.success(function(response) {
        $.each(response, function() {
            const menuId = this.MenuId;

            var selector = "[data-menu-id={menuId}]";

            if (this.AllowAccess) {
                selector += "[data-menu-allow]";
            } else if (this.DisallowAccess) {
                selector += "[data-menu-deny]";
            } else {
                selector = "";
            };

            if (selector) {
                selector = selector.replace("{menuId}", menuId);
                $(selector).prop("checked", true).trigger("change");
            };
        });
    });

});