$(document).ready(function () {
    var queryString = getQueryStringByName(window.scrudFactory.queryStringKey || "");

    if (queryString) {
        loadEdit(queryString);
    } else {
        createForm();
    };
});
