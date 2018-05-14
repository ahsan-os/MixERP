function subscribe(el){
    function request(model){
        var url = "/subscription/add";
        var data = JSON.stringify(model);
        
        return window.getAjaxRequest(url, "POST", data);
    };

    function validate(el) {
        var isValid = window.validator.validate(el, null, true);

        var hp = el.find(".ui.confirm.email.input input").val();

        if (hp) {
            isValid = false;
        };

        return isValid;
    };
    
    el = $(el);
    $("#ConfirmEmailAddressInputEmail").hide();
    var form = el.closest(".form");
    
    var isValid = validate(form);

    if(!isValid){
        return;
    };
    
    form.addClass("loading");
    var model = window.serializeForm(form);
    
    var ajax = request(model);
    
    ajax.success(function(){
        var thankYou = el.closest(".subscription").find(".thank.you");
        form.removeClass("loading").hide();
        thankYou.show();
    });
    
    ajax.fail(function(xhr){
        form.removeClass("loading");
    });
};