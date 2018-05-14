function loadMeta(callback) {
    function request() {
        var api = window.scrudFactory.formAPI;

        if (typeof (api) === "undefined") {
            api = window.scrudFactory.viewAPI;
        };

        var url = api + "/meta";
        return getAjaxRequest(url);
    };

    if (window.scrudFactory.ignoreMeta) {
        window.metaDefinition = [];
        if (typeof (callback) === "function") {
            callback();
        };

        return;
    };

    var getMetaAjax = request();

    getMetaAjax.success(function (response) {
        window.metaDefinition = response;
        if (typeof (callback) === "function") {
            callback();
        };
    });
};