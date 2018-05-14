function appendError(message) {
    var item = $("<li/>");
    item.html(message);
    
    if (window.scrudFactory.tabs.length > 1) {
        $("#ScrudFormErrorModal .error-list").append(item);
    };
};

function resetError() {
    $("#ScrudFormErrorModal .error-list").html("");
};


function validate() {
    resetError();

    var result = validator.validate($(".form.factory"), function (errorFields) {
        $.each(errorFields, function(i, el) {
            var label = $(el).closest(".field").find("label");
            var message = label.html() + " : " + window.translate("ThisFieldIsRequired");
            appendError(message);
        });
    }, false);

    if (!result) {
        if (window.scrudFactory.tabs.length > 1) {
            $("#ScrudFormErrorModal").modal("show");
        };

        return false;
    };

    return true;
};

function initializeValidators() {
    validator.initialize($(".form.factory"));
};