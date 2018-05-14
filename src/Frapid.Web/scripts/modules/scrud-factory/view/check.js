function checkIfProcedure() {
    if ((typeof (window.scrudFactory.isProcedure) === "undefined")) {
        return window.scrudFactory.viewAPI.indexOf("/procedures") !== -1;
    };

    return window.scrudFactory.isProcedure;
};

function hasVerification() {
    if (!window.scrudFactory.hasVerification) {
        return false;
    };

    if (!window.scrudFactory.formAPI) {
        return false;
    };

    var candidates = ["verification_status_id", "verification_reason"];

    var columns = Enumerable.From(metaDefinition.Columns).Select(function (x) { return x.ColumnName.toString() }).ToArray();
    var result = Enumerable.From(candidates).Except(columns).ToArray();
    return result.length === 0;
};

function isCardView() {
    var kanban = $('div[data-target="kanban"]');
    return kanban.is(":visible");
};
