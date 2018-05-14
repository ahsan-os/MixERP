$("#CreateDuplicateButton").click(function () {
    var id = $("[data-primarykey]").val();

    if (id) {
        $(".form.factory").hide();
        $("[data-primarykey]").val("").trigger("change");
        window.displayMessage(window.translate("ItemDuplicated"), "success");
        $(".form.factory").show();
        window.editing = false;
    };
});

$("#ReturnToViewButton").click(function() {
    $(".form.factory").hide();
    $(".view.factory").fadeIn();
});