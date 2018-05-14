var mapContainerInitialized = false;

function updateTitle() {
    const view = $('#calendar').fullCalendar('getView');
    $(".organizer .title span").html(view.title);
};

function showOrganizerEvent(name) {
    var target = null;

    switch (name) {
        case "previous":
            target = $(".fc-prev-button");
            break;
        case "next":
            target = $(".fc-next-button");
            break;
        case "today":
            target = $(".fc-today-button");
            break;
        case "day":
            target = $(".fc-agendaDay-button");
            break;
        case "week":
            target = $(".fc-agendaWeek-button");
            break;
        case "month":
            target = $(".fc-month-button");
            break;
        case "list":
            target = $(".fc-listMonth-button");
            break;
        default:
    };

    if (!target) {
        return;
    };


    target.trigger("click");
    updateTitle();
};

function createEvent(start, end, allDay) {
    $("#AllDayCheckbox").prop("checked", allDay).trigger("change");

    if (start) {
        $("#StartsAtCalendar").calendar("set date", start.local().toDate());
    };

    if (end) {
        $("#EndsOnCalendar").calendar("set date", end.local().toDate());
    };

};


function initializeDatePicker() {
    window.initializeCalendar();

    $('.ui.date.only.picker').calendar("setting", "onChange", function (date) {
        $('#calendar').fullCalendar('gotoDate', date);
    });

    $('.ui.date.only.picker').calendar("set date", new Date(window.today), false, false);
};


function showEventModal() {
    $("#EventModal").show();

    if (!mapContainerInitialized) {
        initMap();
        mapContainerInitialized = true;
    };
};

function hideEventModal() {
    $("#EventModal").hide();
};


$("#NaturalLanguageInputText").keyup(function (e) {
    const el = $(this);
    const input = el.val();

    if (!input.trim()) {
        hideEventModal();
        return false;
    };

    showEventModal();


    const parsed = parseTokens(input);

    $("#NameInputText").val(parsed.Title.trim()).trigger("change");
    $("#LocationInputText").val(parsed.Location.trim()).trigger("keyup").trigger("change");

    $("#StartsAtCalendar").calendar("set date", parsed.StartsFrom);
    $("#EndsOnCalendar").calendar("set date", parsed.EndsOn);

    if (e.which === 13) {
        const locationInput = $("#LocationInputText");
        locationInput.focus();
        return false;
    };
});

$("#CloseEventModalButton").off("click").on("click", function () {
    $("#EventModal").removeAttr("data-event-id");
    $("#EventModal").hide();
});

function pad(num, size) {
    var s = num + "";
    while (s.length < size) s = `0${s}`;
    return s;
};

function parseTime(text, hasPrimeMeridian) {
    const time = text.match(/(\d?\d):?(\d?\d?)/);
    var hours = window.parseInt(time[1], 10);
    const minutes = window.parseInt(time[2], 10) || 0;

    if (hasPrimeMeridian) {
        hours += 12;
    };

    if (hours >= 24) {
        return null;
    };


    const date = new Date();
    date.setHours(hours);
    date.setMinutes(minutes);

    return date;
};

function parseTokens(input) {
    function getTime(token) {
        const hasSeparator = token.indexOf(":") > -1;
        const hasAmplitudeMeridian = token.toLowerCase().indexOf("am") > -1;
        const hasPrimeMeridian = token.toLowerCase().indexOf("pm") > -1;

        if (hasSeparator) {
            token = token.replace(":", "");
        };

        if (hasAmplitudeMeridian) {
            token = token.replace(/am/i, "");
        };

        if (hasPrimeMeridian) {
            token = token.replace(/pm/i, "");
        };

        var timeValue = window.parseInt(token) || 0;

        if (timeValue > 0 && timeValue < 2400) {
            if (timeValue < 10) {
                timeValue *= 100;
            } else if (timeValue < 24) {
                timeValue *= 100;
            };

            const parsed = parseTime(pad(timeValue, 4), hasPrimeMeridian);
            return parsed;
        };

        return null;
    };

    if (!input) {
        return null;
    };

    var startsFrom = undefined;
    var endsOn = undefined;
    var title = "";
    var location = "";

    input = input.trim();

    if (input.toLocaleLowerCase().indexOf(" tonight") > -1) {
        startsFrom = parseTime("7", true);
        endsOn = parseTime("10", true);
        input = input.replace(/tonight/i, "");
    };

    const candidates = input.split(/\Wat\W/i);;
    if (candidates.length) {
        title = candidates[0];
        candidates.shift();
    };

    for (let i = 0; i < candidates.length; i++) {
        const token = candidates[i];
        const time = getTime(token);

        if (!startsFrom && time) {
            startsFrom = time;
        };

        if (!time) {
            location = token;
        };
    };

    return {
        Title: title || "",
        Location: location || "",
        StartsFrom: startsFrom,
        EndsOn: endsOn
    };
};


function initializeFullCalendar() {
    var initialized = false;

    function initializeUi() {
        if (initialized) {
            return;
        };

        try {
            if (!(window.jQuery().fullCalendar && typeof ($('#calendar').fullCalendar) === "function")) {
                setTimeout(function () {
                    initializeUi();
                    return;
                }, 500);
            };


            initializeDatePicker();
            $(".nano").nanoScroller();
            $(".dropdown").dropdown();

            $("#RemindMeDropdown").off("change").on("change", function () {
                const selected = window.parseInt($("#RemindMeDropdown").val());

                if (!selected) {
                    $("#ReminderTypePanel").hide();
                    return;
                };

                $("#ReminderTypePanel").show();
            });

            $("#RepeatDropdown").off("change").on("change", function () {
                const selected = $("#RepeatDropdown").val();
                const repeatValueInputText = $("#RepeatEveryInputText");
                $(".toggle.group.item").hide();
                $(`.toggle.group.item[data-value='${selected}']`).show();

                if (selected === "None") {
                    repeatValueInputText.prop("disabled", true);
                    repeatValueInputText.val("");
                    $("#RepeatUntilPanel").hide();
                    return;
                };

                $("#RepeatUntilPanel").show();
                $("#RepeatEveryInputText").prop("disabled", false);
                if (!window.parseInt(repeatValueInputText.val())) {
                    repeatValueInputText.val("1");
                };
            });

            $("#CategoryDropdown").off("change").on("change", function () {
                const selected = window.parseInt($("#CategoryDropdown").val());

                if (!selected) {
                    return;
                };

                window.localStorage.setItem(window.metaView.Tenant + "_calendar_selected_category", selected);
            });

            loadState();
            displayCalendar();
            initialized = true;
        } catch (e) {
            console.log(e.message);
        }

    };

    initializeUi();
};

function toggleMore() {
    $('.show.more.form').fadeToggle(500);
    setTimeout(function () {
        $(window).trigger('resize'); //Nano scroll needs this hack.
    }, 1000);
};

function removeTime(date) {
    return new Date(date.setHours(0, 0, 0, 0));
};

$("#AllDayCheckbox").off("change").on("change", function () {

    const el = $(this);
    const allDay = el.is(":checked");

    if (allDay) {
        $("#StartsAtCalendar").removeClass("time").calendar("setting", "type", "date").calendar("refresh");
        $("#EndsOnCalendar").removeClass("time").calendar("setting", "type", "date").calendar("refresh");

        let start = $("#StartsAtCalendar").calendar("get date");

        if (start) {
            start = removeTime(start);
            $("#StartsAtCalendar").calendar("set date", start);
        };

        let end = $("#EndsOnCalendar").calendar("get date");

        if (end) {
            end = removeTime(end);
            $("#EndsOnCalendar").calendar("set date", end);
        };

        return;
    };

    $("#StartsAtCalendar").addClass("time").calendar("setting", "type", "datetime").calendar("refresh");
    $("#EndsOnCalendar").addClass("time").calendar("setting", "type", "datetime").calendar("refresh");
});

function toggleDisplay(state) {
    function refreshUi() {
        const modal = $("#EventModal");
        if (!modal.is(":visible")) {
            return;
        };
    };

    window.localStorage.setItem(window.metaView.Tenant + "_calendar_toggle_state", state);

    if (state) {
        $(".right.panel").removeClass("full width");
        $(".left.panel").fadeIn();
        $(".display.toggler").hide();

        refreshUi();
        return;
    };

    $(".right.panel").addClass("full width");
    $(".left.panel").hide();
    $(".display.toggler").fadeIn();


    refreshUi();
};

function toggleCategoryPanel() {
    $("#QueryPanel").toggle();
    $("#CategoryPanel").toggle();

    const count = $("#QueryPanel .categories .nano-content > .category").length + 1;

    $("#CategoryPanel .colors .color").removeClass("active");
    $(`#CategoryPanel .colors .color:nth-of-type(${count})`).addClass("active");

    toggleDisplay(true);
};

$("#DeleteCategoryButton").off("click").on("click", function () {
    function request(categoryId) {
        var url = "/dashboard/calendar/category/{categoryId}";
        url = url.replace("{categoryId}", categoryId);

        return window.getAjaxRequest(url, "DELETE");
    };

    const selectedEl = $("#QueryPanel .categories .category input[type='checkbox']:checked");
    const categoryId = selectedEl.attr("data-category-id");

    if (!categoryId) {
        window.displayMessage("Please select a category.");
        return;
    };

    const confirmed = window.confirmAction();

    if (!confirmed) {
        return;
    };

    const ajax = request(categoryId);

    ajax.success(function () {
        selectedEl.closest(".category").remove();
        window.displaySuccess();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});


function loadState() {
    const toggleState = window.localStorage.getItem(window.metaView.Tenant + "_calendar_toggle_state");
    toggleDisplay(toggleState === "true");

    const defaultCategoryId = window.localStorage.getItem(window.metaView.Tenant + "_calendar_selected_category");

    if (defaultCategoryId) {
        $("#CategoryDropdown").val(defaultCategoryId);
    };

    var categories = window.localStorage.getItem(window.metaView.Tenant + "_calendar_selected_categories");
    if (categories) {
        categories = categories.split(",");

        for (let i = 0; i < categories.length; i++) {
            const categoryId = categories[i];
            const selector = `#QueryPanel .categories input[type='checkbox'][data-category-id='${categoryId}']`;

            $(selector).attr("checked", "checked");
        };
    };
};

$(".category.form.panel .colors .color").off("click").on("click", function () {
    const el = $(this);
    $(".category.form.panel .colors .color").removeClass("active");

    el.addClass("active");
});


$("#SaveCategoryButton").off("click").on("click", function () {
    function request(model) {
        var url = "/dashboard/calendar/category";
        var type = "POST";

        if (model.CategoryId) {
            url += `/${model.CategoryId}`;
            type = "PUT";
        };

        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, type, data);
    };

    function getModel() {
        const categoryName = $("#CategoryNameInputText").val();
        const categoryId = $("#CategoryPanel").attr("data-category-id") || 0;
        const colorCode = $("#CategoryPanel .colors .active.color").attr("class").replace("active", "");

        return {
            CategoryName: categoryName,
            ColorCode: colorCode,
            CategoryId: categoryId
        };
    };

    function validate(model) {
        if (!model) {
            window.displayMessage(window.translate("InvalidModelData"));
            return false;
        };

        if (!model.CategoryName) {
            window.displayMessage(window.translate("PleaseEnterCategoryName"));
            return false;
        };

        if (!model.ColorCode) {
            window.displayMessage(window.translate("PleaseSelectCategoryColor"));
            return false;
        };

        return true;
    };


    const model = getModel();
    const isValid = validate(model);
    if (!isValid) {
        return;
    };

    const ajax = request(model);

    ajax.success(function (categoryId) {
        var el;

        if (model.CategoryId) {
            el = $(`.ui.calendar.categories div.category input[data-category-id='${model.CategoryId}']`)
                .closest(".category");
        } else {
            el = $(".ui.calendar.categories div.category:first-of-type");

            if (!el.length) {
                //There is no element to clone!
                window.location = window.location;
            };

            el = $(el.get(0).outerHTML);
            el.removeClass("ui-sortable-handle");
            el.find("input").attr("data-category-id", categoryId);
        };

        el.find("label").html(model.CategoryName);
        el.find(".category.color").attr("class", model.ColorCode);
        el.find("input").attr("data-category-name", model.CategoryName);
        el.find("input").attr("data-color-code", model.ColorCode).prop("checked", true);

        toggleCategoryPanel();


        if (model.CategoryId) {
            //We are done upadting the element.
            $("#CategoryPanel").removeAttr("data-category-id");
            return;
        };

        $(".ui.calendar.categories .nano-content").append(el);
        $(window).trigger('resize'); //Nano scroll needs this hack.
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});

$("#SaveCategoryOrderButton").off("click").on("click", function () {
    function request(model) {
        const url = "/dashboard/calendar/category/order";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "PUT", data);
    };

    function getModel() {
        const model = [];
        var order = 0;

        $("#QueryPanel .categories input[type='checkbox']").each(function () {
            const el = $(this);
            const categoryId = el.attr("data-category-id");
            model.push({
                Order: order,
                CategoryId: categoryId
            });

            order++;
        });

        return model;
    };

    const model = getModel();

    const ajax = request(model);

    ajax.success(function () {
        window.displaySuccess();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});

function refreshCalendar() {
    $('#calendar').fullCalendar("refetchEvents");
};

$("#QueryPanel .categories input[type='checkbox']").off("change").on("change", function () {
    function saveState() {
        const candidates = $("#QueryPanel .categories input[type='checkbox']:checked");
        const categories = candidates.map(function () {
            return $(this).attr("data-category-id");
        }).get();

        window.localStorage.setItem(window.metaView.Tenant + "_calendar_selected_categories", categories.join(","));
    };

    saveState();
    refreshCalendar();
});

$("#EditCategoryButton").off("click").on("click", function () {
    const candidate = $("#QueryPanel .categories input[type='checkbox']:checked").first();
    if (!candidate.length) {
        window.displayMessage(window.translate("PleaseSelectCategoryToEdit"));
        return;
    };

    toggleCategoryPanel();

    const categoryId = candidate.attr("data-category-id");
    const categoryName = candidate.attr("data-category-name");
    const colorCode = `.${candidate.attr("data-color-code").trim().split(" ").join(".")}`;

    $("#CategoryPanel").attr("data-category-id", categoryId);
    $("#CategoryNameInputText").val(categoryName);
    $("#CategoryPanel .colors .color").removeClass("active");
    $("#CategoryPanel .colors").find(colorCode).addClass("active");
});

function getIcalNetFrequencyType(token) {
    switch (token) {
        case "NDays":
            return "Daily";
        case "NWeeks":
            return "Weekly";
        case "NMonthsChosenDate":
        case "NMonthsChosenDay":
            return "Monthly";
        case "NYears":
            return "Yearly";
        default:
            return "None";
    };
};

function getRecurrencyType(icalNetFrequencyType, byMonthDay) {
    switch (icalNetFrequencyType) {
        case 4: //Daily
            return "NDays";
        case 5: //Weekly
            return "NWeeks";
        case 6: //Monthly
            if (byMonthDay > 0) {
                return "NMonthsChosenDate";
            };

            return "NMonthsChosenDay";
        case 7: //Yearly
            return "NYears";
        default:
            return "None";
    };
};

$("#DeleteEventButton").off("click").on("click", function () {
    function request(eventId) {
        var url = "/dashboard/calendar/{eventId}";
        url = url.replace("{eventId}", eventId);

        return window.getAjaxRequest(url, "DELETE");
    };

    const confirmed = window.confirmAction();
    if (!confirmed) {
        return;
    };

    $(".event.popup").hide();
    const eventId = $(".event.popup").attr("data-event-id");
    const ajax = request(eventId);

    ajax.success(function () {
        refreshCalendar();
        window.displaySuccess();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});

$("#EditEventButton").off("click").on("click", function () {
    const eventId = $(".event.popup").attr("data-event-id");
    const event = window.Enumerable.From(window.events).Where(function (x) {
        return x.EventId === eventId;
    }).FirstOrDefault();

    const daysOfWeek = window.Enumerable.From(event.Recurrence.ByDay).Select(function (x) {
        return x.DayOfWeek.toString();
    }).ToArray();

    $(".event.popup").hide();
    showEventModal();

    $("#EventModal").attr("data-event-id", eventId);
    $("#NameInputText").val(event.Name);
    $("#CategoryDropdown").val(event.CategoryId);
    $("#LocationInputText").val(event.Location);
    $("#RemindMeDropdown").val(event.RemindBeforeInMinutes);
    $("#ReminderTypesSelect").val(event.ReminderTypes);

    $("#UrlInputUrl").val(event.Url);
    $("#NoteInputText").val(event.Note);
    $("#IsPrivateCheckbox").prop("checked", event.IsPrivate);
    $("#AllDayCheckbox").prop("checked", event.AllDay);

    $("#StartsAtCalendar").calendar("set date", new Date(event.StartsAt));
    $("#EndsOnCalendar").calendar("set date", new Date(event.EndsOn));

    if (event.Until) {
        $("#UntilCalendar").calendar("set date", new Date(event.Until));
    };

    if (event.Recurrence.Interval) {
        $("#RepeatEveryInputText").val(event.Recurrence.Interval);
    };

    if (event.Recurrence.Frequency && event.Recurrence.ByMonthDay) {
        $("#RepeatDropdown").val(getRecurrencyType(event.Recurrence.Frequency, event.Recurrence
            .ByMonthDay));
    };

    if (event.Recurrence.ByMonthDay) {
        $("#MonthDayInputText").val(event.Recurrence.ByMonthDay);
    };


    if (event.Recurrence.ByDay && daysOfWeek.length === 1) {
        $("#DayOfMonthSelect").val(daysOfWeek[0]);
    };

    if (event.Recurrence.ByDay && event.Recurrence.ByDay.Offset) {
        $("#DayOfMonthTypeSelect").val(event.Recurrence.ByDay.Offset);
    };

    if (event.Recurrence.ByDay && daysOfWeek.length) {
        $("#DayOfWeekSelect").val(daysOfWeek);
    };
});

$("#OkButton").off("click").on("click", function () {
    function request(model) {
        const url = "/dashboard/calendar";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        //For more info, see Ical.Net.DataTypes.RecurrencePattern
        function getRecurrence(model) {
            if (model.RepeatType === "None") {
                return null;
            };

            const frequency = getIcalNetFrequencyType(model.RepeatType);

            if (frequency === "None") {
                return null;
            };

            const recurrence = {
                Interval: model.RepeatEvery,
                Frequency: frequency,
                Until: model.Until
            };


            if (model.RepeatType === "NMonthsChosenDate") {
                recurrence.ByMonthDay = [model.MonthDay];
            } else if (model.RepeatType === "NMonthsChosenDay") {
                recurrence.ByDay = [
                    {
                        DayOfWeek: model.DayOfMonth,
                        Offset: model.DayOfMonthType
                    }
                ];
            };


            if (model.RepeatType === "NWeeks") {
                const byDay = [];
                for (let i = 0; i < model.DayOfWeek.length; i++) {
                    byDay.push({
                        DayOfWeek: model.DayOfWeek[i]
                    });
                };

                recurrence.ByDay = byDay;
            }

            //alert(JSON.stringify(recurrence));
            return recurrence;
        };

        const model = window.serializeForm($("#EventModal"));
        const eventId = $("#EventModal").attr("data-event-id");
        const starts = $("#StartsAtCalendar").calendar("get date");
        const ends = $("#EndsOnCalendar").calendar("get date");
        const until = $("#UntilCalendar").calendar("get date");

        if (eventId) {
            model.EventId = eventId;
        };


        model.StartsAt = starts;
        model.EndsOn = ends;
        model.Until = until;

        model.Recurrence = getRecurrence(model);

        return model;
    };

    const model = getModel();
    const ajax = request(model);

    ajax.success(function (eventId) {
        $("#EventModal").removeAttr("data-event-id");
        refreshCalendar();
        hideEventModal();
        window.displaySuccess();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});


function getEvents(start, end, categoryIds, callback) {
    function request(model) {
        const url = "/dashboard/calendar/my";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "POST", data);
    };

    const model = {
        Start: start,
        End: end,
        CategoryIds: categoryIds
    };

    const ajax = request(model);

    ajax.success(function (response) {
        window.events = response;
        callback(parseToFullCalendarEvents(response));
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
};


function displayCalendar() {
    const el = $('#calendar');
    el.fullCalendar({
        eventAfterAllRender: function () {
        },
        header: {
            left: '',
            center: 'prev title next',
            right: 'today year agendaDay,agendaWeek,month,listMonth,listYear'
        },
        defaultView: "month",
        select: function (start, end) {
            createEvent(start, end, false);
            showEventModal();
        },
        unselect: function () {
            //const input = $("#NaturalLanguageInputText").val().trim();
            //if (!input) {
            //    hideEventModal();
            //};
        },
        defaultDate: window.today ? new Date(window.today) : new Date(),
        navLinks: true, // can click day/week names to navigate views
        editable: false,
        eventStartEditable: false,
        eventDurationEditable: false,
        unselectAuto: false,
        selectable: true,
        selectHelper: true,
        eventLimit: true, // allow "more" link when too many events
        eventClick: function (calEvent, jsEvent) {
            if (calEvent.url) {
                return false;
            };
        },
        events: function (start, end, timezone, callback) {
            //console.log(start + "-->" + end + "-->" + timezone);
            const categoryIds = $(".ui.calendar.categories .category input:checked").map(function () {
                return $(this).attr("data-category-id");
            }).get();

            getEvents(start.toDate(), end.toDate(), categoryIds, callback);
        },
        viewRender: function (view, element) {
            $('.event.popup').hide();
        },
        eventRender: function (calEvent, element) {
            updateTitle();
            $(element).addClass(calEvent.cssClass);

            element.bind('dblclick', function (jsEvent) {
                function getTopPosition(el, top) {
                    const windowHeight = $(window).height();
                    const elHeight = el.height();
                    const offset = 20;


                    if (top > windowHeight - elHeight - offset) {
                        top -= elHeight + (offset * 2);
                        return top;
                    };

                    top += offset;
                    return top;
                };

                function getLeftPosition(el, left) {
                    const windowWidth = $(window).width();
                    const elWidth = el.width();
                    const offset = 10;
                    left += offset;

                    if (left > windowWidth - elWidth - offset) {
                        left -= elWidth;
                    };

                    return left;
                };


                const tooltip = $('.event.popup');
                tooltip.attr("data-event-id", calEvent.id);
                tooltip.attr("data-category-id", calEvent.categoryId);
                tooltip.find("a.title").html(calEvent.title);
                tooltip.find(".location span:first-child").attr("class", calEvent.cssClass);
                tooltip.find(".location .name").html(calEvent.location);
                tooltip.find("span.starts").html(calEvent.start.fromNow());
                tooltip.find("span.reminder.minutes").html(calEvent.alarm);
                tooltip.css('top', getTopPosition(tooltip, jsEvent.pageY));
                tooltip.css('left', getLeftPosition(tooltip, jsEvent.pageX));
                tooltip.show();
            });
        }
    });
};


function parseToFullCalendarEvents(data) {
    function setTime(date, time) {
        date = new Date(date);
        time = new Date(time);

        date.setUTCHours(time.getUTCHours());
        date.setUTCMinutes(time.getUTCMinutes());

        return date;
    };

    if (!data) {
        return null;
    };


    const events = [];

    for (let i = 0; i < data.length; i++) {
        const event = {
            id: data[i].EventId,
            title: data[i].Name,
            start: new Date(data[i].StartsAt),
            end: new Date(data[i].EndsOn),
            allDay: data[i].AllDay,
            editable: true,
            url: data[i].Url,
            cssClass: data[i].ColorCode,
            categoryId: data[i].CategoryId,
            categoryName: data[i].CategoryName,
            location: data[i].Location,
            alarm: data[i].RemindBeforeInMinutes,
            reminderTypes: data[i].ReminderTypes,
            isPrivate: data[i].IsPrivate,
            note: data[i].Note,
            recurrence: data[i].Recurrence
        };

        events.push(event);

        if (data[i].Occurences) {
            for (let j = 0; j < data[i].Occurences.length; j++) {
                if (window.moment(new Date(data[i].StartsAt)).format("YYYY/MM/DD") === window
                    .moment(new Date(data[i].Occurences[j])).format("YYYY/MM/DD")) {
                    continue;
                };


                const occurence = JSON.parse(JSON.stringify(event));


                occurence.start = setTime(data[i].Occurences[j], data[i].StartsAt);
                occurence.end = setTime(data[i].Occurences[j], data[i].EndsOn);
                occurence.occurences = data[i].Occurences;

                events.push(occurence);
            };
        };
    };

    //alert(JSON.stringify(events));
    return events;
};

$(".sortable").sortable();


window.initializeFullCalendar();
