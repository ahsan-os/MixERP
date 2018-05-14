var validator = new function () {
	function isNullOrWhiteSpace(obj) {
		if ($.isArray(obj)) {
			return isArrayNullOrWhiteSpace(obj) || obj.length === 0;
		} else {
			return (!obj || $.trim(obj) === "");
		}
	};
	
    function makeDirty(el){
        var field = el.closest(".field");
        field.addClass("error");
    };

    function removeDirty(el){
        var field = el.closest(".field");
        field.removeClass("error");
    };
    
    var isValid = true;
    var requiredFieldMessage = translate("ThisFieldIsRequired");

    this.logError = function (message) {
        console.log(message);
    };

    var validateField = function (field) {
        var val = field.val();
		
        if (isNullOrWhiteSpace(val)) {
            this.isValid = false;
            makeDirty(field);
        } else {
            removeDirty(field);
        };
		
		if(field.is(".currency")){
			if(val === window.currencySymbol){
				this.isValid = false;
				makeDirty(field);				
			};
		};
    };

    this.initialize = function (el) {
        el.find("[required]:not(:disabled):not([readonly])").blur(function () {
            var field = $(this);
            validateField(field);
        });
    };

    this.validate = function (el, oninvalid, log) {
        var required = el.find(".image.form-field, [required]:not(:disabled):not([readonly]):visible, .dropdown>select[required]");
        required.trigger("blur");

		var chosenDropdowns = $(".chosen-container:visible");

		$.each(chosenDropdowns, function(){
			var id = $(this).attr("id");
			
			if(!id){
				return true;
			};
			
			var target = id.replace("_chosen", "");
			$("#" + target).trigger("blur");
		});
		
        if (jQuery().timepicker) {
            $(".ui-timepicker-input").timepicker("hide");
        };

        var emailFields = el.find('input[type=email]:visible');
        
        $.each(emailFields, function () {
            var el = $(this);
            var val = el.val();

            var exp = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
            if (!exp.test(val)) {
                makeDirty(el);
                isValid = false;
                return false;
            };
        });

        var errorFields = el.find(".error:not(.big):not(.message)");

        isValid = errorFields.length === 0;

        $.each(errorFields, function (i, v) {
            var field = $(v);
            var label = field.closest(".field").find("label");
            var message = (label.html() || field.find("[id]").attr("id")) + " : " + requiredFieldMessage;

            if (log) {
                console.log(message);
            };
        });

        var expressions = el.find("[data-validation-expression]:visible");

        expressions.each(function () {
            var el = $(this);
            var val = el.val();
            var expression = el.attr("data-validation-expression");
            var message = el.attr("data-validation-message");
            var target = $(el.attr("data-validation-target"));
            target.html("");

            if (!val.match(expression)) {
                makeDirty(el);
                isValid = false;
                target.html(message);
                return false;
            };
        });


        var matchTargets = el.find("[data-match-target]");

        matchTargets.each(function () {
            var el = $(this);
            var name = el.siblings("label").html();
            var val = el.val();

            var targetSelector = "#" + el.attr("data-match-target");
            var target = $(targetSelector);
            var targetVal = target.val();
            var targetName = target.siblings("label").text();

            if (val !== targetVal) {
                makeDirty(el);
                makeDirty(target);
                alert("The " + name + " does not match with " + targetName + ".");
                isValid = false;
                return false;
            };
        });

        if (!isValid) {
            if (typeof (oninvalid) === "function") {
                oninvalid(errorFields);
            };
        };

        return isValid;
    };
};
