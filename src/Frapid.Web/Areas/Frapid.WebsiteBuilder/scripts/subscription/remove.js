function unsubscribe() {
    function request(model) {
        const url = "/subscription/remove";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function validate(el) {
        var isValid = window.validator.validate(el, null, true);

        const hp = el.find(".ui.confirm.email.input input").val();

        if (hp) {
            isValid = false;
        };

        return isValid;
    };

    $("#ConfirmEmailAddressInputEmail").hide();
    var form = $(".subscription.form");

    const isValid = validate(form);

    if (!isValid) {
        return;
    };

    form.addClass("loading");
    const model = window.serializeForm(form);

    const ajax = request(model);

    ajax.success(function() {
        const thankYou = $(".thank.you");
        form.removeClass("loading").hide();
        thankYou.show();
    });

    ajax.fail(function() {
        form.removeClass("loading");
    });
};