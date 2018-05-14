function loadIcons(apps) {
	function addIcon(app, target) {
		const div = $("<div/>");
		const anchor = $("<a/>");
		const icon = $("<i/>");
		const image = $("<img />");

		const span = $("<span/>");

		div.attr("class", "item");
		anchor.attr("class", "app");
		anchor.attr("data-app-name", app.AppName);
		anchor.attr("href", app.LandingUrl || "javascript:void(0);");

		const iContainer = $("<div class='icon'/>");

		if (app.Icon.substr(0, 10) === "data:image") {
		    image.attr("src", app.Icon);
		    iContainer.append(image).addClass("image");
		} else {
		    icon.attr("class", (app.Icon || "user") + " inverted circular icon");
		    iContainer.append(icon);
		};


		span.text(window.translate(app.I18nKey));

		anchor.append(iContainer);
		anchor.append(span);
		div.append(anchor);

		target.append(div);
	};

	const target = $("#PhoneMenu");

	for (let i = 0; i < apps.length; i++) {
		addIcon(apps[i], target);
	};
};

function loadApps() {
	function request() {
	    const url = "/dashboard/my/apps";
	    return window.getAjaxRequest(url);
    };

	const ajax = request();

	ajax.success(function(response) {
		loadIcons(response);
	});
};
