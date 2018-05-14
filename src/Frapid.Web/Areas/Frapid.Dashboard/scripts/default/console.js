$("#AddNewButton").off("click").on("click", function () {
    $(".widget-container").html("");
    $(".document.heading").html(window.translate("UntitledDashboard")).focus();
});

function createMyConsoleDropdown(consoles) {
    function getEl(console) {
        const item = $("<div class='item' />");
        const icon = $("<i class='file outline icon'></i>");
        const span = $("<span />");
        span.html(console.Name);
        item.attr("data-value", console.Name);

        item.append(icon).append(span);

        if (console.IsDefault) {
            const defaultIcon = $("<i class='tiny inverted green circular check mark icon'></i>");
            item.append(defaultIcon);
        };

        item.on("click", function () {
            loadConsole(console);
        });

        return item;
    };

    $("#OpenButton .menu .item").remove();
    const container = $("#OpenButton .menu");

    consoles = window.Enumerable.From(consoles).OrderBy(function (x) {
        return x.Name;
    }).ToArray();

    for (let i = 0; i < consoles.length; i++) {
        const console = consoles[i];
        const el = getEl(console);
        container.append(el);
    };
};

function getWidgets(callback) {
    function request(scope) {
        var url = "/dashboard/widgets/get/my/{scope}/all";
        url = url.replace("{scope}", scope);

        return window.getAjaxRequest(url);
    };

    const ajax = request(window.widgetScope);

    ajax.success(function (response) {
        if (!response.length) {
            $(".document.heading").focus();
            return;
        };

        const consoleName = $(".document.heading").text().trim();
        window.consoles = response;
        createMyConsoleDropdown(response);

        const current = window.Enumerable.From(response).Where(function (x) {
            return x.Name.trim() === consoleName;
        }).ToArray();

        var console = response[0];

        if (current.length) {
            console = current[0];
        } else {
            const defaultConsole = window.Enumerable.From(response).Where(function (x) {
                return x.IsDefault;
            }).ToArray();

            if (defaultConsole.length) {
                console = defaultConsole[0];
            };
        };

        if (console) {
            if (typeof (callback) === "function") {
                callback(console);
            };
        };
    });
};

function loadWidget(widget) {
    $(".console.segment").addClass("loading");
    const path = "/Areas/" + widget.AreaName + "/widgets/" + widget.WidgetName + ".html";

    $.get(path, function (data) {
        const container = $(".widget-container");
        const el = $(data);

        el.attr("data-area-name", widget.AreaName);
        el.attr("data-widget-name", widget.WidgetName);

        if (widget.hasOwnProperty("Order")) {
            el.attr("data-order", widget.Order);
        };

        container.append(el);

        container.find('.column').sort(function (a, b) {
            return +a.getAttribute('data-order') - +b.getAttribute('data-order');
        }).appendTo(container);


        el.find(".close.button").off("click").on("click", function () {
            if (!window.confirmAction()) {
                return;
            };

            el.remove();
        });


        el.find(".collapse.button").off("click").on("click", function () {
            const button = $(this);
            const icon = button.find("i");
            const hide = icon.is(".up");


            if (hide) {
                icon.removeClass("up").addClass("down");
            } else {
                icon.removeClass("down").addClass("up");
            };

            el.find(".body").toggleClass("hidden");
        });

		window.localize();

		$(".console.segment").removeClass("loading");
    });
};

function loadConsole(console) {
    //alert(JSON.stringify(console));
    $(".widget-container").html("");

    $(".document.heading").text(console.Name);
    $("section.heading .check.mark.icon").remove();

    if (console.IsDefault) {
        const icon = $(`<i class="inverted green circular check mark icon"></i>`);
        $("section.heading").append(icon);
    };

    for (let i = 0; i < console.Widgets.length; i++) {
        const widget = console.Widgets[i];

        setTimeout(function () {
            loadWidget(widget);
        });
    };
};

function refreshWidgets() {
    $(".widget-container").html("");
    $(".widget-container").sortable({ 
		handle: '.widget.segment>.header', 
		placeholder: 'ui-state-highlight',
		start: function(e, ui){
				ui.placeholder.height(ui.item.height());
				ui.placeholder.width(ui.item.width());
			}
	});
    window.initializeSelectApis();

    getWidgets(function (console) {
        loadConsole(console);
    });
};

$("#AddWidgetButton").off("click").on("click", function () {
    const widget = {
        AreaName: $("#WidgetAreaSelect").val(),
        WidgetName: $("#WidgetSelect").val()
    };

    const existing = $(".widget-container [data-area-name='" + widget.AreaName + "'][data-widget-name='" + widget.WidgetName + "']");

    if (existing.length) {
        existing.find(".widget").addClass("error");

        setTimeout(function () {
            existing.find(".widget").removeClass("error");
        }, 2000);

        return;
    };

    loadWidget(widget);
    $("#WidgetSelect").focus();
});

$("#WidgetAreaSelect").off("change").on("change", function () {
    const area = $(this).val();
    if (!area) {
        $("#WidgetSelect").html("");
    };

    const url = "/dashboard/widgets/get/" + area;

    window.ajaxDataBind(url, $("#WidgetSelect"), null, "", "", null, null, true);
});

$("#DefaultButton").off("click").on("click", function () {
    const starIcon = $("section.heading .check.mark.icon");

    if (!starIcon.length) {
        const icon = $(`<i class="inverted green circular check mark icon"></i>`);
        $("section.heading").append(icon);
        return;
    };

    starIcon.remove();
});

$("#SaveButton").off("click").on("click", function () {
    function request(model) {
        const url = "/dashboard/widgets/save/my";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        function getWidgets() {
            const widgets = [];
            const candidates = $(".widget-container .column");

            $.each(candidates, function (i) {
                const candidate = $(this);

                widgets.push({
                    Scope: window.widgetScope,
                    AreaName: candidate.attr("data-area-name"),
                    WidgetName: candidate.attr("data-widget-name"),
                    Order: i
                });
            });

            return widgets;
        };

        return {
            Scope: window.widgetScope,
            Name: $(".document.heading").text().trim(),
            IsDefault: $("section.heading .check.mark.icon").length === 1,
            Widgets: getWidgets()
        };
    };

    const model = getModel();
    const ajax = request(model);

    ajax.success(function () {
        window.displaySuccess();
        $(".add.widget.segment").hide();
        refreshWidgets();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});

$("#OpenButton").dropdown();

$("#AddConsoleButton").off("click").on("click", function () {
    const consoleName = $(".document.heading").text().trim();
    if (!consoleName) {
        return;
    };

    const icon = $(`<i class="inverted green circular check mark icon"></i>`);
    $(".document.heading").text(consoleName);
    $("section.heading").append(icon);

    $(".add.widget.segment").show();
});

$("#DeleteButton").off("click").on("click", function () {
    function request(model) {
        const url = "/dashboard/widgets/delete/my";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "DELETE", data);
    };

    function getModel() {
        return {
            Scope: window.widgetScope,
            Name: $(".document.heading").text().trim()
        };
    };

    if (!window.confirmAction()) {
        return;
    };

    const model = getModel();
    const ajax = request(model);

    ajax.success(function () {
        $(".document.heading").html("");
        window.displaySuccess();
        refreshWidgets();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });

});

if (window.widgetScope) {
    window.refreshWidgets();
};
