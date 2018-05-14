function loadEdit(id) {
    function request(primaryKeyValue) {
        var url = window.scrudFactory.formAPI + "/" + primaryKeyValue;
        return getAjaxRequest(url);
    };

	$(document).trigger("dataLoading");
    var ajax = request(id);

    ajax.success(function (response) {
        window.editData = response;
        window.editing = true;
        $("#scrud").html("");
        $("#scrud-tab-menus").html("");
        $("#scrud-tab-members").html("");
        createForm();
    });
};
