$(".phone.number.field").hide();

$(document).ready(function() {
    window.validator.initialize($(".contact.form"));
});

function sendEmail(el) {
    function request(model) {
        const url = "/contact-us";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function validate(el) {
        var isValid = window.validator.validate(el);

        const hp = el.find(".phone.number.field input").val();

        if (hp) {
            isValid = false;
        };

        return isValid;
    };


    el = $(el);
    var formEl = el.closest(".contact.form");
    const isValid = validate(formEl);

    if (!isValid) {
        return;
    };


    formEl.addClass("loading");
    const model = window.serializeForm(formEl);


    const ajax = request(model);
    ajax.success(function() {
        const message = '<div class="ui positive message">' + window.translate("ThankYouForContactingUs") + '</div>';
        formEl.html(message).removeClass("loading");
    });
};