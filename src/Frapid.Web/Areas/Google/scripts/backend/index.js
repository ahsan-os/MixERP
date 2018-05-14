$("#EnabledCheckbox").prop("checked", $("#EnabledCheckbox").attr("data-checked") === "true");

$("#SaveButton").off("click").on("click", function () {
    function request(model) {
        const url = "/dashboard/google";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "PUT", data);
    };

    function getModel() {
        return window.serializeForm($(".google.configuration.segment"));
    };

    const model = getModel();
    const ajax = request(model);

    ajax.success(function () {
        window.displaySuccess();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});
