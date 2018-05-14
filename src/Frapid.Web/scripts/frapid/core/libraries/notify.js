var taskCompletedSuccessfully = translate("TaskCompletedSuccessfully");

function displayMessage(a, b) {
    if (window.notify) {
        displayNotification(a, b);
        return;
    };

    alert(a);
};

function displaySuccess() {
    if (window.notify) {
        displayNotification(taskCompletedSuccessfully, "success");
    };
};

function logError(a, b) {
    if (window.notify) {
        displayNotification(a, b);
    }else{
    	alert(b + " -> " + a);
    };
};

function displayNotification(message, type, alwaysDisplay){
	type = type || "error";
	var icon = "";

	switch(type){
		case "alert":
			icon = "warning circle";
			break;
		case "success":
			icon = "check circle";
			break;
		case "error":
			icon = "remove circle";
			break;
		case "warning":
			icon = "warning circle";
			break;
		case "info":
			icon = "info circle";
			break;
	};
	
	icon = "<i class='" + icon + " icon'></i>";
	message = icon + message;
	
	window.notify({
		type: type, //alert | success | error | warning | info
		title: "",
		message: message,
		position: {
			x: "right", //right | left | center
			y: "top" //top | bottom | center
		},
		size: "normal", //normal | full | small
		overlay: false, //true | false
		closeBtn: close, //true | false
		overflowHide: false, //true | false
		spacing: 20, //number px
		theme: "default", //default | dark-theme
		autoHide: !alwaysDisplay, //true | false
		delay: 10000, //number ms
		onShow: null, //function
		onClick: null, //function
		onHide: null, //function
		template: '<div class="notify"><div class="notify-text"></div></div>'
	});
};

function logAjaxErrorMessage(xhr) {
    logError(getAjaxErrorMessage(xhr));
};