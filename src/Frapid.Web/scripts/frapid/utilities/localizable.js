function translate(key){
	function humanize(key){
		var items = key.split('.');
		
		if(!items){
			return key;
		};
		
		var text = items[items.length -1];
		
		if(text){
			text = text.replace(/([A-Z])/g, ' $1').trim();
			return text;
		};

		return key;
	};

	function getLocalizedResource(text) {
		function toProperCase(str) {
		    var result = str.replace(/_([a-z])/g, function (g) { return g[1].toUpperCase(); });
		    return result.charAt(0).toUpperCase() + result.slice(1);
		};

	    var key = toProperCase(text);
	    
	    if(!window.i18n){
	    	return "";
	    };

	    var parsed = window.i18n[key];

	    return parsed;
	};

	var localized = getLocalizedResource(key);

	if (!localized) {
		localized = humanize(key);
	};
	
	return localized;
};

function localize() {
    /*
    The HTML nodes having the data-localize attribute 
    will be localized with their respective culture values.
    
    For example:
    
    <span data-localize="ScrudResource.company_name"></span>
    
    will be converted to 
    
    Company Name in English or
    Firmenname in German
    */
    var localizable = $("[data-localize]");
	
	$.each(localizable, function(){
		var el = $(this);
		
        var key = el.attr("data-localize");
        var localized = window.translate(key);

		var tag = el.prop("tagName").toLowerCase();
		
		switch(tag){
			case "input":
				el.val(localized);
				break;
			default:
				el.html(localized);
				break;
		};
	});

    var localizable = $("[data-localized-placeholder]");
	
	$.each(localizable, function(){
		var el = $(this);
		
        var key = el.attr("data-localized-placeholder");
        var localized = window.translate(key);
		
		el.attr("placeholder", localized);
	});


    var localizable = $("[data-localized-title]");
	
	$.each(localizable, function(){
		var el = $(this);
		
        var key = el.attr("data-localized-title");
        var localized = window.translate(key);
		
		el.attr("title", localized);
	});

    /*
    The HTML nodes having the data-localized-resource attribute 
    will be localized with their respective culture values on the target attribute.
    
    For example:
    
    <input data-localized-resource="ScrudResource.company_name" data-localization-target="value" />
    
    will be converted to 
    
    <input data-localized-resource="ScrudResource.company_name" value="Company Name" />
    in English or

    <input data-localized-resource="ScrudResource.company_name" value="Firmenname " />
    in German
    */
    $("[data-localized-resource]").each(function () {
        var el = $(this);
        var key = el.attr("data-localized-resource");
        var localized = window.translate(key);

        if (localized) {
            var target = el.attr("data-localization-target");
            el.attr(target, localized);
        };
    });
};
