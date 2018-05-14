function setMoments() {
    const els = $(".refreshing.moment");

    $.each(els, function () {
        const el = $(this);
        var val = el.attr("data-time");

        if (!val) {
            val = window.parseInt(el.attr("data-server-time"));
        };

        const time = new Date(val);

        if (!isNaN(time)) {
            el.text(window.moment(time).fromNow());
            el.attr("title", time.toString());
        };
    });

    setTimeout(function () {
        setMoments();
    }, 5000);
};

$.getJSON("/dashboard/meta", function (response) {
    window.meta = response;
    window.culture = meta.Culture;
    window.language = meta.Language;
    window.jqueryUIi18nPath = meta.JqueryUIi18NPath;
    window.today = meta.Today;
    window.now = meta.Now;
    window.date = today;
    window.userId = meta.UserId;
    window.user = meta.User;
    window.office = meta.Office;
    window.metaView = meta.MetaView;
    window.shortDateFormat = meta.ShortDateFormat;
    window.longDateFormat = meta.LongDateFormat;
    window.thousandSeparator = meta.ThousandSeparator;
    window.decimalSeparator = meta.DecimalSeparator;
    window.currencyDecimalPlaces = meta.CurrencyDecimalPlaces;
    window.currencySymbol = meta.MetaView.CurrencySymbol || meta.CurrencySymbol;
    window.datepickerFormat = window.convertNetDateFormat(meta.ShortDateFormat);
    window.datepickerShowWeekNumber = meta.DatepickerShowWeekNumber;
    window.datepickerWeekStartDay = meta.DatepickerWeekStartDay;
    window.datepickerNumberOfMonths = meta.DatepickerNumberOfMonths;

    $(document).trigger("metaready");
});

$.getJSON("/dashboard/custom-variables", function (response) {
    window.customVars = response;
});

jQuery.ajaxSetup({
    cache: true
});

var lastPage;

function loadUI() {
    window.localize();
    window.loadDatepicker();
    window.setRegionalFormat();
	
	setTimeout(function(){
		window.initializeSearchable();
	}, 1000);
};

var frapidApp = angular.module('FrapidApp', ['ngRoute']);


frapidApp.config(function ($routeProvider, $locationProvider, $httpProvider) {
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';

    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });


    $routeProvider.
        when('/dashboard', {
            templateUrl: "/dashboard/my/template/Contents/apps.html"
        }).
        when('/dashboard/:url*', {
            templateUrl: function (url) {
                var path = '/dashboard/' + url.url;
                const qs = [];

                for (let q in url) {
                    if (url.hasOwnProperty(q)) {
                        if (q === "url") {
                            continue;;
                        };

                        if (url.hasOwnProperty(q)) {
                            qs.push(q + "=" + url[q]);
                        };
                    }
                };

                if (qs.length) {
                    path = path + "?" + qs.join("&");
                };

                return path;
            }
        });
});

frapidApp.run(function ($rootScope, $location) {
    $rootScope.$on('$locationChangeStart', function (e, goingTo, cameFrom) {
        function extractPath(fullUrl) {
            const anchor = document.createElement("a");
            anchor.href = fullUrl;
            return anchor.pathname;
        };


        //This hack fixes a strange angularjs bug which, out of the blue, 
        //forces a page to load "fromUrl" during the route change. 
        //When this condition occurs, "toUrl" url is swapped with "fromUrl",
        //which results in the page reload.

        //No referrer. The current url is the last page.
        if (cameFrom !== goingTo) {
            //Seems like the destination url is acutally the document url.
            //Don't want to be in this page anymore.
            if (goingTo === location.toString()) {

                //fromUrl is actually the destination.
                const goTo = extractPath(cameFrom);

                $location.url(goTo);				
                $location.search({});
            };
        };

        window.overridePath = null;
    });

    $rootScope.$on('$routeChangeStart', function () {
        $("#dashboard-container").addClass("loading");
    });
	
	$rootScope.$on('$viewContentLoaded', function(){
		setTimeout(function(){
			if(window.viewReady === false && typeof(window.prepareScrudView) === "function"){
				window.prepareScrudView();
			};
		}, 1000);
	});


    $rootScope.$on('$routeChangeSuccess', function () {
        $("#dashboard-container").removeClass("loading");
        buildMenus();
        loadUI();
    });

    $rootScope.toogleDashboard = function () {
        if (window.location.pathname !== "/dashboard") {
            lastPage = {
                path: window.location.pathname,
                query: window.location.search.substring(1)
            };

            $location.url("/dashboard");
        } else {
            if (lastPage) {
                $location.url(lastPage.path + "?" + lastPage.query);
            };
        };
    };
});

var menuBuilder = {
    build: function (app, container, menuId) {
        const myMenus = window.Enumerable.From(window.appMenus)
            .Where(function (x) { return x.AppName === app; })
            .Where(function (x) { return x.ParentMenuId === menuId; })
            .OrderBy(function (x) { return x.Sort; })
            .ThenBy(function (x) { return x.MenuId; })
            .ToArray();


        var isSubMenu = menuId != null && myMenus.length > 0;

        if (isSubMenu) {
            if (container.hasClass("item")) {
                container.addClass("ui dropdown");
                container.append('<i class="dropdown icon"></i>');
                container.append("<div class='sub menu' />");
            };
        };

        $.each(myMenus, function () {
            const menu = this;
            const anchor = $("<a />");
            const span = $("<span/>");
            anchor.addClass("item");
            anchor.attr("data-menu-id", menu.MenuId);
            anchor.attr("data-app-name", menu.AppName);
            anchor.attr("data-parent-menu-id", menu.ParentMenuId);
            anchor.attr("href", menu.Url || "javascript:void(0);");
            //anchor.attr("target", "_self");

            span.text(window.translate(menu.I18nKey));

            if (menu.Icon) {
                const i = $("<i/>");
                i.addClass(menu.Icon);
                i.addClass("icon");

                anchor.append(i);
            };

            anchor.append(span);

            if (isSubMenu) {
                container.find(".sub.menu").append(anchor);
            } else {
                container.append(anchor);
            };


            anchor.off("click").on("click", function () {
                const key = meta.Tenant + "_menu_events";

                function fromStorage() {
                    return JSON.parse(window.localStorage.getItem(key));
                };

                function toStorage(menuId) {
                    const current = fromStorage() || {};
                    const currentCount = current[menuId] || 0;

                    current[menuId] = currentCount + 1;

                    window.localStorage.setItem(key, JSON.stringify(current));
                };

                if (menu.Url) {
                    toStorage(menu.MenuId);
                };
            });

            window.menuBuilder.build(app, anchor, menu.MenuId);
        });
    }
};

function buildMenus() {
    setTimeout(function () {
        const target = $('[data-scope="app-menus"]').html("");
        var path = window.overridePath || window.location.pathname;
        
		if (window.menuBuilder) {
            const application = window.Enumerable.From(window.appMenus)
                .Where(function (x) { return x.Url === path; })
                .FirstOrDefault();

            if (application) {
                window.menuBuilder.build(application.AppName, target, null);
                $(".dashboard.menu .dropdown").dropdown({ placeholder: false, forceSelection: false });
            };
        };
		
		target.toggle(target.find(".item").length > 0);				
    }, 500);
};

(function () {
    function loadMenus() {
        function request() {
            const url = "/dashboard/my/menus";
            return window.getAjaxRequest(url);
        };

        const ajax = request();

        ajax.success(function (response) {
            window.appMenus = response;
            buildMenus();
        });
    };

    loadMenus();
})();


function initializeSelectApis() {
    const candidates = $("select[data-api]");

    candidates.each(function () {
        var el = $(this);
        const apiUrl = el.attr("data-api");
        const valueField = el.attr("data-api-value-field") || "Value";
        const keyField = el.attr("data-api-key-field") || "Key";
        const isArray = el.attr("data-is-array") || false;
        const removePlaceholder = el.attr("data-remove-placeholder") === "true" || false;

        window.ajaxDataBind(apiUrl, el, null, keyField, valueField, null, function () {
            var selectedValue = el.attr("data-api-selected-value");
            var selectedValues = el.attr("data-api-selected-values");

            if (selectedValue) {
                //Todo: Remove Semantic UI Dropdown dependency
                // setTimeout(function () {
                //     el.dropdown("set selected", selectedValue.toString());
                // }, 100);
                el.val(selectedValue.toString());
            };

            if (selectedValues) {
                //Todo: Remove Semantic UI Dropdown dependency
                // setTimeout(function () {
                //     const values = selectedValues.split(",");
                //     el.dropdown("set selected", values);
                // }, 100);

                const values = selectedValues.split(",");
                el.val(values);
            };
            
            if(selectedValue || selectedValues){
                setTimeout(function(){
                    el.trigger("change");
                }, 500);
            };

        }, isArray, removePlaceholder);
    });
};

var backgrounds = [];

$.getJSON("/dashboard/backgrounds", function (response) {
    backgrounds = response;

    if (backgrounds.length) {
        $('.background.slider').css("background-color", "black");
    };

    loadBackground();
});

function loadBackground() {
    $.each(backgrounds, function (i, v) {
        setTimeout(function () {
            if (i === backgrounds.length - 1) {
                setTimeout(function () {
                    loadBackground();
                }, 10000);
            };

            setBackground(v);
        }, i * 10000);


    });
};

function setBackground(image) {
    var slider = $('.background.slider:not(.active)');
    var activeSlider = $('.background.slider.active');

    slider.css('background-image', "url('" + image + "')");

    activeSlider.fadeOut(1500, function () {
        activeSlider.css('z-index', -2).show().removeClass('active');
        slider.css('z-index', -1).addClass('active');
    });

};

$('.notification.item').popup({
    inline: true,
    hoverable: false,
    position: 'bottom right',
    popup: $('.notification.popup'),
    on: 'click',
    closable: true,
    delay: {
        show: 300,
        hide: 800
    }
});

function addNotification(model, supressMessage) {
    function getIcon(icon, fromApp) {
        return icon || fromApp;
    };

    function updateTotal() {
        const totalItems = $(".notification.popup .items").find(".notification.item.unseen").length;

        if (totalItems) {
            const sticker = $("<div class='sticker' />");
            sticker.html(totalItems);
            $(".right.menu .notification.item").append(sticker);
        } else {
            $(".right.menu .notification.item .sticker").remove();
        };
    };

    function getEl() {
        const el = $("<div class='notification unseen item' />");
        el.attr("data-notification-id", model.NotificationId);
        el.attr("event-timestamp", model.EventTimestampOffset);
        el.attr("data-associated-app", model.AssociatedApp);
        el.attr("data-associated-menu-id", model.AssociatedMenuId);

        el.attr("data-url", model.Url);

        if (model.hasOwnProperty("Seen")) {
            if (model.Seen) {
                el.removeClass("unseen").addClass("seen");
            };
        };

        const appIcon = getIcon(model.Icon, model.AssociatedApp);

        const app = $("<div class='app' />");
        const icon = $("<div class='icon' />");
        const i = $("<i class='icon' />");
        i.addClass(appIcon).appendTo(icon);
        icon.appendTo(app);

        app.appendTo(el);

        const message = $("<a class='message' />");
        message.attr("href", model.Url);
        message.html(model.FormattedText);

        const timestamp = $("<span class='refreshing timestamp moment'></span>");
        timestamp.attr("data-time", model.EventTimestampOffset);
        timestamp.html(model.EventTimestampOffset);
        timestamp.attr("title", model.EventTimestampOffset);

        timestamp.appendTo(message);

        message.appendTo(el);

        el.off("click").on("click", function (e) {
            function request(notificationId) {
                var url = "/dashboard/my/notifications/set-seen/{notificationId}";
                url = url.replace("{notificationId}", notificationId);
                return window.getAjaxRequest(url, "POST");
            };

            e.preventDefault();

            const el = $(this);
            const notificationId = el.attr("data-notification-id");

            const ajax = request(notificationId);

            ajax.success(function () {
                el.removeClass("unseen").addClass("seen");
                updateTotal();
                document.location = el.attr("data-url");
            });
        });

        return el;
    };

    const target = $(".notification.popup .items");
    target.find(".placeholder").remove();

    if (!supressMessage) {
        window.displayMessage(model.FormattedText, "info");
    };

    const el = getEl();

    target.prepend(el);


    updateTotal();
};

const notifcationHub = $.connection.notificationHub;

if (notifcationHub) {
    $(function () {
        notifcationHub.client.notificationReceived = function (message) {
            message.EventTimestampOffset = new Date();
            addNotification(message);
        };

        $.connection.hub.start().done(function () {

        });
    });
};

function sayHi() {
    const greetings =
    [
        window.i18n.GreetingGoodToSeeYouAgain,
        window.i18n.GreetingNiceToSeeYou,
        window.i18n.GreetingHowWasYourDay,
        window.i18n.GreetingWelcomeBack,
        window.i18n.GreetingHi,
        window.i18n.GreetingThereYouAre,
        window.i18n.GreetingWeMissedYou,
        window.i18n.GreetingYouAreBackWithABang,
        window.i18n.GreetingYouAreAwesome
    ];

    const greeted = localStorage.getItem("greeted");

    if (greeted) {
        return;
    };

    localStorage.setItem("greeted", true);

    var name = "";

    if (window.metaView) {
        name = window.metaView.Name;
    };

    const hello = greetings[Math.floor(Math.random() * greetings.length)];
    window.displayMessage(window.stringFormat(hello, name), "success");
};

setTimeout(function () {
    sayHi();
}, 1000);


function scrollToElement(container, el) {
    if (!container.length || !el.length) {
        return;
    };

    const offset = el.offset().top - container.scrollTop();

    if (offset > container.innerHeight()) {
        container.scrollTop(offset);
    };
    if (offset < 0) {
        container.scrollTop(0);
    }
};

window.addEventListener("keydown", function (e) {
    if (e.keyCode === 114 || (e.ctrlKey && e.shiftKey && e.keyCode === 70)) {
        $("[data-feature-search]").focus();

        e.stopPropagation();
        e.preventDefault();
        return false;
    };
});

function getFeatures() {
    //Clone the array
    const features = (window.appMenus || []).slice(0);

    features.push({
        MenuId: null,
        AppName: "Frapid",
        MenuName: window.i18n.Dashboard,
        Sort: 0,
        Url: "/dashboard",
        Icon: "pin"
    });

    features.push({
        MenuId: null,
        AppName: "Frapid",
        MenuName: window.i18n.ShowNotifications,
        Sort: 0,
        Url: "javascript:void(0);",
        Click: function () {
            $('.notification.item').popup("show");
        },
        Icon: "pin"
    });

    features.push({
        MenuId: null,
        AppName: "Frapid",
        MenuName: window.i18n.SayHi,
        Sort: 0,
        Url: "javascript:void(0);",
        Click: function () {
            localStorage.removeItem("greeted");
            window.sayHi();
        },
        Icon: "smile"
    });

    features.push({
        MenuId: null,
        AppName: "Frapid",
        MenuName: window.i18n.LogOff,
        Sort: 0,
        Url: "javascript:void(0);",
        Click: function () {
            window.displayMessage(window.i18n.HopeToSeeYouSoon, "success");
            setTimeout(function () {
                document.location = "/account/sign-out";
            }, 1000);
        },
        Icon: "smile"
    });

    features.push({
        MenuId: null,
        AppName: "Frapid",
        MenuName: window.i18n.SignOut,
        Sort: 0,
        Url: "javascript:void(0);",
        Click: function () {
            window.displayMessage(window.i18n.HopeToSeeYouSoon, "success");
            setTimeout(function () {
                document.location = "/account/sign-out";
            }, 1000);
        },
        Icon: "smile"
    });

    features.push({
        MenuId: null,
        AppName: "Frapid",
        MenuName: window.i18n.GoBack,
        Sort: 0,
        Url: "javascript:history.go(-1);",
        Icon: "angle double left"
    });

    features.push({
        MenuId: null,
        AppName: "Frapid",
        MenuName: window.i18n.Goodbye,
        Sort: 0,
        Url: "javascript:void(0);",
        Click: function () {
            window.displayMessage(window.i18n.HopeToSeeYouSoon, "success");
            setTimeout(function () {
                document.location = "/account/sign-out";
            }, 1000);
        },
        Icon: "smile"
    });

    return features;
};

$("[data-feature-search]").on("keyup", function (e) {
    const features = getFeatures();

    const upArrow = 38;
    const downArrow = 40;
    const enter = 13;
    const escape = 27;

    const el = $(this);
    const target = $(".ui.find.feature");
    const container = $(".ui.find.feature>.results");

    function nav(key) {
        if (key === escape) {
            el.val("");
            target.fadeOut(500);
            return;
        };

        var active = target.find(".result.active");

        if (key === upArrow || key === downArrow) {
            if (!active.length) {
                target.find(".result").first().addClass("active");
                return;
            };

            if (key === downArrow) {
                active = active.removeClass("active").next(".result").addClass("active");

                if (!active.length) {
                    active = target.find(".result").first().addClass("active");
                };

                scrollToElement(container, active);
                return;
            };

            if (key === upArrow) {
                active = active.removeClass("active").prev(".result").addClass("active");

                if (!active.length) {
                    active = target.find(".result").last().addClass("active");
                };

                scrollToElement(container, active);
                return;
            };
        };

        if (key === enter && active.length) {
            active.find("a").trigger("click");
        };
    };

    const navKeys = [13, 38, 40, 27];


    if (navKeys.indexOf(e.which) >= 0) {
        nav(e.which);
        return;
    };

    const query = el.val().toLowerCase();

    if (!query) {
        target.hide();
        return;
    };

    target.find(".result").remove();

    function getBreadCrumb(menuId, breadcrumb) {
        const menu = window.Enumerable.From(features).Where(function (x) {
            return x.MenuId === menuId;
        }).FirstOrDefault();

        if (!breadcrumb) {
            breadcrumb = "";
        } else {
            breadcrumb = " / " + breadcrumb;
        };

        if (menu) {
            breadcrumb = menu.MenuName + breadcrumb;

            if (menu.ParentMenuId) {
                return getBreadCrumb(menu.ParentMenuId, breadcrumb);
            };

            breadcrumb = menu.AppName + " / " + breadcrumb;

            return breadcrumb;
        } else {
            return breadcrumb;
        }
    };

    const matches = window.Enumerable.From(features).Where(function (x) {
        return x.MenuName.toLowerCase().indexOf(query) !== -1 && x.Url;
    }).ToArray();

    if (!matches.length) {
        target.hide();
    };

    $.each(matches, function () {
        const match = this;
        const el = $("<div class='result' />");

        const icon = $("<div class='icon' />");
        const i = $("<i class='icon' />");
        i.addClass(match.Icon).appendTo(icon);

        icon.appendTo(el);

        const feature = $("<a class='feature' />");
        feature.html(match.MenuName);
        feature.attr("href", match.Url);

        if (match.Click) {
            feature.on("click", function () {
                match.Click();
            });
        };

        feature.on("click", function () {
            $("[data-feature-search]").val("");
            target.fadeOut(500);
        });

        const breadcrumb = $("<span class='breadcrumb' />");
        breadcrumb.html(getBreadCrumb(match.MenuId)).appendTo(feature);

        feature.appendTo(el);
        el.appendTo(container);
    });

    if (matches.length) {
        const left = $(".search.item").offset().left;
        const offset = $(".search.item").width();
        const width = target.width();
        const padding = 20;

        if ($("body").is(".rtl")) {
            target.css("right", "auto").css("left", (left + offset - width + padding) + "px");
        } else {
            target.css("right", "auto").css("left", left + "px");
        };

        target.fadeIn(500);
    };
});


function showNotifications() {
    function request() {
        const url = "/dashboard/my/notifications";
        return window.getAjaxRequest(url);
    };

    const ajax = request();

    ajax.success(function (response) {
        const ordered = window.Enumerable.From(response).OrderByDescending(function (x) {
            return x.EventDate;
        }).ToArray();


        $.each(ordered, function () {
            this.EventTimestampOffset = this.EventDate;
            addNotification(this, true);
        });

        setMoments();
    });

};

showNotifications();
setMoments();

$(document).ready(function () {
    function updateLanguage() {
        function update(cultureCode) {
            document.cookie = "culture=" + cultureCode + ";path=/";
        };

        const el = $(".select.language.dropdown");
        const language = el.dropdown("get value");
        update(language);

        window.displaySuccess();

        document.location = document.location;
    };

    $('.select.language.dropdown .scrolling.menu .item').off("click").on("click", function () {
        setTimeout(function () {
            updateLanguage();
        }, 1000);
    });

    $("#SearchLanguageButton").off("keyup").on("keyup", function (e) {
        if (e.keyCode === 13) {
            setTimeout(function () {
                updateLanguage();
            }, 1000);
        };
    });
});

$(".ui.theme.selector.dropdown").dropdown();
$(".ui.select.language.dropdown").dropdown();
