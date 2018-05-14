if (!String.prototype.endsWith) {
    // ReSharper disable once NativeTypePrototypeExtending
    String.prototype.endsWith = function (searchString, position) {
        var subjectString = this.toString();
        if (typeof position !== "number" || !isFinite(position) || Math.floor(position) !== position || position > subjectString.length) {
            position = subjectString.length;
        };

        position -= searchString.length;
        var lastIndex = subjectString.indexOf(searchString, position);
        return lastIndex !== -1 && lastIndex === position;
    };
};


function serializeForm(el) {
    function getControlName(id) {
        if (id) {
            var conventions = ["InputTel", "InputColor", "InputDate", "InputDateTime", "InputDateTimeLocal", "InputHidden", "InputNumber", "InputSearch", "InputTime", "InputUrl", "InputText", "InputPassword", "InputEmail", "Select", "Checkbox", "TextArea", "Calendar", "Tags", "Div"];
            for (var i = 0; i < conventions.length; i++) {
                var convention = conventions[i];
                
                if (id.endsWith(convention)) {
                    return id.replace(new RegExp(convention + "$"), "");
                };                
            };

            return id;
        };

        return "";
    };

    function getVal(el){
        var val = el.val() || "";
        var type = el.attr("type");
        var dataType = el.attr("data-type");
		
		if(dataType === "number"){
			type = "number";
		};
		
		if(el.is("select")){
			type = "select";
		};
		
		if(el.hasClass("hasDatepicker")){
			type = "datepicker";
		};
		
		if(el.hasClass("ui") && el.hasClass("calendar")){
			type = "calendar";
		};
		
		if(el.hasClass("decimal") || el.hasClass("decimal4") || el.hasClass("currency") || el.hasClass("integer")){
			type = "number";
		};
		
		if(el.hasClass("ui tags")){
			type = "tags";
		};
		
        
        switch(type){
			case "tags":
				var tags = el.find(".ui.label").map(
							function(){
								return $(this).text().trim();
							}).get();
				if(tags && tags.length){
					return tags.join(",");
				}
				return "";
            case "datepicker":
                if(!val){
                    return null;
                };
                
                return el.datepicker("getDate");
            case "calendar":
                if(!el.find("input").val().trim()){
                    return null;
                };

                return el.calendar("get date");
            case "checkbox":
                return el.is(":checked");
            case "number":                
                return window.parseFloatStrict(val || null);
			case "select":
				if(val === window.translate("Select")){
					val = null;
				};
				
				return val;
            default:
                return val;
        };
    };

    var members = el.find("input:not([type='radio']), input[type='radio']:checked, select, textarea, div.date.calendar, div.ui.tags");

    var form = {};
    members.each(function () {
        var item = $(this);
        var type = item.attr("type");
        var id = item.attr("id");
		
		if(type === "file"){
			return true;
		};
		
		var nameMember = item.is("[data-name-member]");
		if(nameMember){
			id = "";
		};

        var name = item.attr("name");
        
        if (id) {
            var val = getVal(item);

            var member = getControlName(id);

            if (member) {
                form[member] = val;
            };
        } else if(name){
            var val = getVal(item);
            var member = getControlName(name);
            
            if (member) {
                form[member] = val;
            };            
        };
    });

    return form;
};

function initializeUITags() {
	function processTags(el) {
		const tags = el.closest(".ui.tags");
		const labels = tags.find(".label");

		function addTag(value) {
			if (!value) {
				return;
			};

			var isDuplicate = false;

			$.each(labels, function () {
				const label = $(this);

				if (label.text().trim().toLowerCase() === value.toLowerCase()) {
					label.addClass('red');

					setTimeout(function () {
						label.removeClass('red');
					}, 2000);

					isDuplicate = true;
					return false;
				};
			});

			if (isDuplicate) {
				return;
			};

			const anchor = $("<a class='ui label' />");
			anchor.html(value);

			const icon = $("<i class=\"delete icon\"></i>");
			anchor.append(icon);
			tags.find("input").before(anchor);

			$(".ui.tags .label .delete.icon").off("click").on("click", function () {
				if (window.confirmAction()) {
					$(this).closest(".label").remove();
				};
			});
		};


		var value = el.val();
		el.val("");

		if (!value) {
			return;
		};

		if (value.substr(value.length - 1, 1) === ",") {
			value = value.substr(0, value.length - 1);
		};

		value = value.trim();

		if (!value) {
			return;
		};

		const values = value.split(',');
		for (let i = 0; i < values.length; i++) {
			addTag(values[i].trim());
		};
	};

	$(".ui.tags .label .delete.icon").off("click").on("click", function () {
		if (window.confirmAction()) {
			$(this).closest(".label").remove();
		};
	});

	$(".ui.tags input").keyup(function (e) {
		if (e.keyCode === 188) {
			const el = $(this);
			processTags(el);
		};
	});

	$(".ui.tags input").change(function () {
		const el = $(this);
		processTags(el);
	});

	$(".ui.tags input").trigger("change");
};

function deserializeForm(container, model) {
    const convention = ["InputTel", "InputColor", "InputDate", "InputDateTime", "InputDateTimeLocal", "InputHidden", "InputNumber", "InputSearch", "InputTime", "InputUrl", "InputText", "InputPassword", "InputEmail", "Select", "Checkbox", "TextArea", "Calendar", "Tags", "Div"];

    function setValue(el, value) {
        var type = el.attr("type");

        if (el.hasClass("hasDatepicker")) {
            type = "datepicker";
        };

        if (el.is("select")) {
            if (el.parent().hasClass("ui") && el.parent().hasClass("dropdown")) {
                type = "dropdown";
            };
        };


        if (el.hasClass("ui") && el.hasClass("calendar")) {
            type = "calendar";
        };

        if (el.hasClass("ui") && el.hasClass("tags")) {
            type = "tags";
        };

        if (!value) {
            if (el.is("select")) {
                el.find("option").prop("selected", false);
            };

            return;
        };

        switch (type) {
            case "tags":
                el.find(".ui.label").remove();
                el.find("input").val(value).trigger("change");
                break;
            case "datepicker":
                el.datepicker("setDate", new Date(value));
                break;
            case "calendar":
                el.calendar("set date", new Date(value));
                break;
            case "checkbox":
                el.prop("checked", value.toLowerCase === "true");
                break;
            case "dropdown":
                //Todo: Remove Semantic UI Dropdown dependency 
                //el.dropdown("set selected", value);
                console.log("Semantic UI Dropdown is not supported!!!");
                break;
            default:
                el.val(value);
                break;
        };
    };

    function searchEl(key) {
        for (let i = 0; i < convention.length; i++) {
            const selector = key + convention[i];

            var el = $("#" + selector);
            if (el.length) {
                return el;
            };

            el = $("[name='" + selector + "']");
            if (el.length) {
                return el;
            };
        };

        return null;
    };

    if (!model || !container.length) {
        return;
    };

    const keys = Object.keys(model);

    if (!keys || !keys.length) {
        return;
    };

    for (let i = 0; i < keys.length; i++) {
        const key = keys[i];

        const value = model[key];
        const el = searchEl(key);

        if (el && el.length) {
            setValue(el, value);
        };
    };

};

