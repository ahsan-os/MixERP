function createForm() {
    function getSchema() {
        return window.scrudFactory.formTableName.split(".")[0];
    };

    function customFieldRequest() {
        var url = window.scrudFactory.formAPI + "/custom-fields";
        var queryString = getQueryStringByName(window.scrudFactory.queryStringKey || "");

        if (queryString) {
            url += "/" + queryString;
        };

        return getAjaxRequest(url);
    };

    function request() {
        var url = window.scrudFactory.formAPI + "/meta";
        return getAjaxRequest(url);
    };

    var ajax = request();

    ajax.success(function (response) {
        window.metaInfo = response;

        createLayout(response.Columns, scrud);

        if (getSchema()) {
            var cfAjax = customFieldRequest();
            cfAjax.success(function (reply) {
                window.metaCustomFields = reply;
                createCustomFields(window.metaCustomFields);
            });
        };
    });
};
