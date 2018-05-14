function prepEntities(entities) {
    var items = [];

    $.each(entities, function (i, v) {
        var entity = new Object();

        $.each(v, function (propertyName, val) {
            var type = window.Enumerable.From(metaDefinition.Columns).Where(function (x) { return x.PropertyName === propertyName }).ToArray()[0].DbDataType;
                        
            if (isNullOrWhiteSpace(val)) {
                val = null;
            } else {
                if (wholeNumbers.indexOf(type) > -1) {
                    val = window.parseInt2(val);
                } else if (decimalNumber.indexOf(type) > -1) {
                    val = window.parseFloat2(val);
                } else if (dateTypes.indexOf(type) > -1) {
                    //
                } else if (booleans.indexOf(type) > -1) {
                    val = (["true", "t", "yes", "y", "1"].indexOf((val || "").toString().toLowerCase()) > -1);
                };
            };


            entity[propertyName] = val;
        });

        items.push(entity);
    });

    return items;
};

$("#file").change(function () {
    function request(entities) {
        var url = window.scrudFactory.formAPI + "/bulk-import";
        var form = [];

        form.push(window.metaDefinition);
        form.push(entities);

        var data = JSON.stringify(form);
        return getAjaxRequest(url, "POST", data);
    };

    var el = $(this);
    $(".big.error").removeClass("vpad8").html("");
    var file = this.files[0];

    if (!file) {
        return;
    };

    var supportedFileTypes = ["text/csv", "application/csv", "text/comma-separated-values", "application/vnd.ms-excel"];

    if (supportedFileTypes.indexOf(file.type) === -1) {
        $(".big.error").addClass("vpad8").html(stringFormat(window.translate("UploadInvalidTryAgain"), file.type));
        return;
    };

    $("#ProgressBar").show();
    var progress = $(".progress");

    showProgress(progress, 25, window.translate("ProcessingYourCSVFile"));

    var reader = new FileReader();

    reader.onload = function () {
        var result = reader.result;
        showProgress(progress, 50, window.translate("SuccessfullyProcessedYourFile"));
        var entities = Papa.parse(result, { "header": true, skipEmptyLines: true }).data;
        entities = prepEntities(entities);

        showProgress(progress, 50, window.translate("RequestingImport"));

        el.closest(".segment").find(".button").addClass("loading").prop("disabled", true);

        var ajax = request(entities);

        ajax.success(function () {
            var message = stringFormat(window.translate("ImportedNItems"), entities.length);
            showProgress(progress, 100, message);
            el.closest(".segment").find(".button").removeClass("loading").prop("disabled", false);

            setTimeout(function(){
                window.location = window.location;
            }, 2000);
        });

        ajax.fail(function (xhr) {
            $(".big.error").addClass("vpad8").html(getAjaxErrorMessage(xhr));
            el.closest(".segment").find(".button").removeClass("loading").prop("disabled", false);
            showProgress(progress, 0, window.translate("RollingBackChanges"));
            $("#ProgressBar").fadeOut(2000);
        });
    };

    reader.readAsText(file);
});

function showProgress(el, percentage, message) {
    el.attr("data-percent", percentage.toString());
    el.find(".bar").css("width", percentage.toString() + "%");
    el.find(".label").text(percentage.toString() + "% - " + message);
};

