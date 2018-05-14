jQuery.fn.getSelectedItem = function () {
    var listItem = $(this[0]);
    return listItem.find("option:selected");
};

jQuery.fn.getSelectedValue = function () {
    return $(this[0]).getSelectedItem().val();
};

jQuery.fn.getSelectedText = function () {
    return $(this[0]).getSelectedItem().text();
};

jQuery.fn.setSelectedText = function (text) {
    var target = $(this).find("option").filter(function () {
        return this.text === text;
    });

    target.prop('selected', true);
};

function displayFieldBinder(el, url, notNull, filters, callback) {
	function request() {
		function getRequest(){
			return window.getAjaxRequest(url);			
		};
		
		function postRequest(){
			var data = JSON.stringify(filters);
			return window.getAjaxRequest(url, "POST", data);
		};
		
		if(!filters){
			return getRequest();
		};
		
		return postRequest();
	};

	var ajax = request();

	ajax.success(function (response) {
		var options = "";

		if (!notNull) {
			options += "<option>Select</option>";
		};
		
		var totalItems = response.length;
		
		$.each(response, function (i) {
			var option = "<option value='{key}' {selected}>{value}</option>";
			option = option.replace("{key}", this.Key);
			option = option.replace("{value}", this.Value);
			
			if(notNull && i === 0){
				option = option.replace("{selected}", "selected='selected'");									
			}else{
				option = option.replace("{selected}", "");													
			};

			options += option;
		});

        el.html(options);

        setTimeout(function(){
			el.trigger("change").trigger("blur");
        });
		
		if(typeof(callback) === "function"){
			callback();
		};
	});
};
