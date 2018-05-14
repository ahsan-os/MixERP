var bigError = $(".big.error");
$(".email.address.field").hide();

$(document).ready(function() {
    window.validator.initialize($(".signup.segment"));
});

$("#EmailInputEmail").change(function() {
    function request(email) {
        const url = `/account/sign-up/validate-email?email=${email}`;
        return window.getAjaxRequest(url, "POST");
    };

    var el = $(this);
    const email = el.val();

    if (!email) {
        return;
    };

    $("#SignUpButton").addClass("disabled");
    const ajax = request(email);

    ajax.success(function(response) {
        if (response) {
            bigError.html("");
            window.removeDirty(el);
            $("#SignUpButton").removeClass("disabled");
        } else {
            bigError.html(window.translate("EmailAddressInUse"));
            window.makeDirty(el);
        };
    });
});

$("#SignUpButton").click(function() {
    function request(model) {
        const url = "/account/sign-up";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function validate() {
        bigError.html("");
        const formEl = $(".signup.segment");

        if (!$("#AgreementCheckbox").is(":checked")) {
            bigError.html(window.translate("CreateAccountAgreeToTermsAndCondition"));
            return false;
        };

        var isValid = window.validator.validate(formEl);

        const hp = $("#EmailAddressInput").val();

        if (hp) {
            isValid = false;
        };

        return isValid;
    };

    bigError.html("");

    const isValid = validate();
    if (!isValid) {
        return;
    };


    const formEl = $(".signup.segment");
    formEl.addClass("loading");
    const model = window.serializeForm(formEl);


    const ajax = request(model);
    ajax.success(function(response) {
        if (response) {
            window.location = "/account/sign-up/confirmation-email-sent";
        };
    });
});