$("#ConfirmEmailInputEmail").hide();
$(document).ready(function () {
    window.validator.initialize($(".reset.password.segment"));
});


$("#SetPasswordButton").click(function () {
    function request(token, password) {
        var url = "/account/reset/confirm?token=" + token;
        url += "&password=" + password;

        return window.getAjaxRequest(url, "POST");
    };

    function validate() {
        $(".big.error").html("");
        const formEl = $(".reset.password.segment");
        const isValid = window.validator.validate(formEl);
        return isValid;
    };

    $(".big.error").html("");

    const isValid = validate();
    if (!isValid) {
        return;
    };


    const formEl = $(".reset.password.segment");
    formEl.addClass("loading");
    const model = window.serializeForm(formEl);
    const token = window.getQueryStringByName("token");

    const ajax = request(token, model.Password);

    ajax.success(function (response) {
        if (response) {
            window.location = "/account/sign-in";
        };
    });
});
