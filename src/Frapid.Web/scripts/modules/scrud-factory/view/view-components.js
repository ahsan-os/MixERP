function showTarget(target) {
    $("div[data-target]").hide();
    var targetEl = $('div[data-target="' + (target || "").toLowerCase() + '"]');
    targetEl.removeClass("hidden").show();

    $('a[data-target]').removeClass("active green");
    $('a[data-target="' + target + '"]').addClass("active green");
};

function initializeViews() {
    if (window.scrudFactory.removeKanban || checkIfProcedure()) {
        $('[data-target="kanban"]').remove();
        showTarget("grid");
    };

    if (window.scrudFactory.removeFilter || checkIfProcedure()) {
        $('[data-target="filter"]').remove();
        $("#FilterButton").remove();
        showTarget("kanban");
    };

    if (window.scrudFactory.removeImport || checkIfProcedure()) {
        $('[data-target="import"]').remove();
        showTarget("kanban");
    };
};
