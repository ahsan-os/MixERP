function supportsBrowserStorage() {
    try {
        return 'localStorage' in window && window['localStorage'] !== null;
    } catch (e) {
        return false;
    };
};

function loadPersister(){
	$("[data-persist]").each(function(){
		var el = $(this);
		var id = el.attr("id");
		
		if(!id){
			return;
		};

		var key = document.location.pathname + '/' + id;
		var value = localStorage.getItem(key);
		el.val(value);
	});

	$("[data-persist]").change(function(){
		var el = $(this);
		var id = el.attr("id");
		var value = el.val();
		
		if(!id){
			return;
		};
		
		var key = document.location.pathname + '/' + id;
		localStorage.setItem(key, value);
	});	
};

loadPersister();
