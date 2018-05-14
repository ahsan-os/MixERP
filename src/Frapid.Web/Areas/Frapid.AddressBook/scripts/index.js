function setTimeZone() {
    const offset = new Date().getTimezoneOffset();
    $("#TimeZoneSelect option").removeAttr("selected");
    $(`#TimeZoneSelect option[data-offset='${offset}']`).prop("selected", true);
    $("#LanguageSelect").val(window.culture);
};

$("#PrefixInputText, #SuffixInputText, #FirstNameInputText, #MiddleNameInputText, #LastNameInputText")
    .off("keyup change blur").on("keyup change blur", function() {
        const prefix = $("#PrefixInputText").val().trim();
        const suffix = $("#SuffixInputText").val().trim();
        const firstName = $("#FirstNameInputText").val().trim();
        const middleName = $("#MiddleNameInputText").val().trim();
        const lastName = $("#LastNameInputText").val().trim();

        var name = prefix.trim();

        if (name) {
            name += " ";
        };

        name += firstName;

        if (middleName) {
            name += ` ${middleName}`;
        };

        if (lastName) {
            name += ` ${lastName}`;
        };

        if (suffix) {
            name += ` ${suffix}`;
        };

        $("#FormattedNameInputText").val(name);
    });

function resetForm() {
    function getGuid() {
        return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function(c) {
            const r = Math.random() * 16 | 0, v = c === "x" ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    };

    const container = $(".address.book .right.panel");
    const contactId = getGuid();

    container.removeAttr("data-contact-id");
    container.find("input, textarea").val("");
    container.find(".tags .label").remove();
    container.find("select option").prop("selected", false);
    $("#FirstNameInputText").focus();

    container.find("select").find(":selected").removeAttr("selected");
    container.find("select").find("option:first-child").prop("selected", true);

    $(".entries .entry").removeClass("selected");
    $(".entries .entry input[type='checkbox']").prop("checked", true);
    $("#ContactIdInputHidden").val(contactId);
    $("#PhotoInputHidden").val("");
    $("#PhotoInputHidden").attr("data-handler", `/dashboard/address-book/avatar/${contactId}`);
    $("#UploaderSegment img").attr("src", "/images/logo.png");
};

$(".new.contact.item").off("click").on("click", function() {
    resetForm();
    setTimeZone();
});

$("#SaveButton").off("click").on("click", function() {
    function request(model) {
        var url = "/dashboard/address-book";
        var method = "POST";

        if (model.Edit) {
            url = "/dashboard/address-book/edit";
            method = "PUT";
        };

        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, method, data);
    };

    function getModel() {
        const container = $(".right.panel");
        const model = window.serializeForm(container);
        const contactId = $(".right.panel").attr("data-contact-id");
        model.Edit = false;

        if (contactId) {
            model.Edit = true;
            model.ContactId = contactId;
        };

        model.AssociatedUserId = window.parseInt(model.AssociatedUserId) || null;

        return model;
    };

    const model = getModel();
    const ajax = request(model);

    ajax.success(function() {
        window.displaySuccess();
        loadContacts();
        resetForm();
    });

    ajax.fail(function(xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});


var fileInputFile = $("#FileInputFile");

function readUrl(input) {
    if (input.files && input.files[0]) {
        const reader = new FileReader();

        reader.onload = function(e) {
            $(input).trigger("readComplete", [{ e: e }]);
        };

        reader.readAsDataURL(input.files[0]);
    };
};

fileInputFile.on("change", function() {
    var el = $(this);
    const handler = el.attr("data-handler");

    const loaderTarget = el.attr("data-loader-id");
    var targetSelector = el.attr("data-target");
    var segment = el.closest(".segment");

    if (loaderTarget) {
        segment = $(`#${loaderTarget}`);
    };

    var target = null;

    if (targetSelector) {
        target = $(`#${targetSelector}`);
    };

    readUrl(this);

    if (segment.length) {
        segment.addClass("loading");
    };

    el.upload(handler, function(response) {
            if (targetSelector && target && response) {
                target.val(response);
                target.attr("data-val", response);
            };

            if (segment.length) {
                segment.removeClass("loading");
            };

            el.trigger("done", [{ response: response }]);
        },
        null, null, function error(xhr) {
            $(".frapid.address.book.segment").removeClass("loading");
            window.logAjaxErrorMessage(xhr);
        });
});

fileInputFile.on("done", function() {
    document.location = document.location;
});

function getQueryModel() {
    const tags = $(".tags input:checkbox:checked").map(function() {
        return $(this).attr("data-tag");
    }).get().join(",");
    const privateOnly = $(".select.type.form input:checked").val() === "private";

    return {
        Tags: tags,
        PrivateOnly: privateOnly
    };
};

$("#ExportContactsAnchor").off("click").on("click", function() {
    function request(model) {
        const url = "/dashboard/address-book/export/vcard";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    const el = $(this);
    if (el.hasClass("loading") || el.attr("href")) {
        return;
    };

    el.find("i").removeClass("cloud download").addClass("loading circle notched");

    const model = getQueryModel();
    const ajax = request(model);

    ajax.success(function(response) {
        el.find("i").removeClass("loading circle notched").addClass("cloud download");

        el.attr("target", "_self");
        el.attr("href", response);
        el.attr("download", "Contacts.vcf");
        el[0].click();
    });

    ajax.fail(function(xhr) {
        el.find("i").removeClass("loading circle notched").addClass("cloud download");
        window.logAjaxErrorMessage(xhr);
    });
});

$(".toolbar .email.item").off("click").on("click", function() {
    $("#EmailModal textarea").trumbowyg({ svgPath: "/scripts/trumbowyg/dist/ui/icons.svg" });
    $("#EmailModal").modal("show");
});

$(".toolbar .sms.item").off("click").on("click", function() {
    $("#SmsModal").modal("show");
});

$("#ShowMoreAnchor").off("click").on("click", function() {
    $(".more.form").toggle();
    window.initializeCalendar();
});

$("#DeleteAnchor").off("click").on("click", function() {
    function request(contactId) {
        var url = "/dashboard/address-book/delete/{contactId}";
        url = url.replace("{contactId}", contactId);

        return window.getAjaxRequest(url, "DELETE");
    };

    if (!window.confirmAction()) {
        return;
    };

    const entry = $(".entries .selected.entry").first();
    const contactId = entry.attr("data-contact-id");
    const ajax = request(contactId);

    ajax.success(function() {
        entry.remove();
        resetForm();
        window.displaySuccess();
    });

    ajax.fail(function(xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});

function loadContact(el) {
    function request(contactId) {
        var url = "/dashboard/address-book/{contactId}";
        url = url.replace("{contactId}", contactId);

        return window.getAjaxRequest(url);
    };

    el = $(el);
    const contactId = el.attr("data-contact-id");

    if (!contactId) {
        return;
    };

    resetForm();
    const segment = $(".address.book.segment");
    segment.addClass("loading");

    const ajax = request(contactId);

    ajax.success(function(response) {
        segment.removeClass("loading");
        $(".right.panel").attr("data-contact-id", contactId);
        $("#ContactIdInputHidden").val(contactId);
        $("#PhotoInputHidden").attr("data-handler", `/dashboard/address-book/avatar/${contactId}`);
        var avatar = "/dashboard/address-book/avatar/{contactId}/{name}/100/100";
        avatar = avatar.replace("{contactId}", contactId).replace("{name}", response.FormattedName);
        $("#UploaderSegment img").attr("src", avatar);

        window.deserializeForm($(".address.book.segment .right.panel"), response);
    });

    ajax.fail(function(xhr) {
        segment.removeClass("loading");
        window.logAjaxErrorMessage(xhr);
    });
};

function loadContacts() {
    const target = $(".ui.entries");
    const segment = $(".address.book.segment");

    function addToDom(contact) {
        function getPhoto() {
            const photo = $("<div class=\"photo\">");

            var path = "/dashboard/address-book/avatar/{contactId}/{name}/100/100";
            path = path.replace("{contactId}", contact.ContactId);
            path = path.replace("{name}", contact.FormattedName);

            const img = $("<img />");
            img.attr("src", path);
            photo.append(img);

            return photo;
        };

        function getInfo() {
            const info = $("<div class=\"info\" />");
            const name = $("<div class=\"name\" />");

            const checkbox = $("<div class=\"ui checkbox\"><input checked type=\"checkbox\" /><label></label></div>");
            checkbox.find("input").attr("data-contact-id", contact.ContactId);
            checkbox.checkbox();
            checkbox.find("label").append(contact.FormattedName);
            name.append(checkbox);

            info.append(name);

            const detail = $("<div class=\"detail\">");
            detail.html(contact.DisplayInfo);
            info.append(detail);

            return info;

        };


        const entry = $("<div class=\"entry\" tabindex=\"0\" />");
        entry.attr("data-contact-id", contact.ContactId);
        entry.attr("data-mobile-numbers", contact.MobileNumbers);
        entry.attr("data-email-addresses", contact.EmailAddresses);

        entry.append(getPhoto());
        entry.append(getInfo());
        target.append(entry);

        entry.off("click").on("click", function () {
            const selectedEl = target.find(".entry.selected");

            if (selectedEl.is(entry)) {
                return;
            };

            loadContact(this);

            target.find(".entry").removeClass("selected");
            entry.addClass("selected");
        });

        entry.off("keyup").on("keyup", function(e) {
            function previous(el) {
                target.find(".entry").removeClass("selected");
                el.prev().addClass("selected");
            };

            function next(el) {
                target.find(".entry").removeClass("selected");
                el.next().addClass("selected");
            };

            const el = $(".entries .selected.entry");

            switch (e.which) {
            case 13: //enter
                el.removeClass("selected").trigger("click");
                break;
            case 37: // left
                previous(el);
                break;
            case 38: // up
                previous(el);
                break;

            case 39: // right
                next(el);
                break;

            case 40: // down
                next(el);
                break;

            default:
                return; // exit this handler for other keys
            };
        });

    };

    function request(model) {
        const url = "/dashboard/address-book/get";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };


    segment.addClass("loading");
    const model = getQueryModel();
    const ajax = request(model);


    ajax.success(function(response) {
        target.html("");
        $.each(response, function() {
            addToDom(this);
        });

        segment.removeClass("loading");
    });

    ajax.fail(function(xhr) {
        segment.removeClass("loading");
        window.logAjaxErrorMessage(xhr);
    });
};

$(".select.type.form input:radio, .tags input:checkbox").off("change").on("change", function() {
    loadContacts();
});

$(".tag.title input:checkbox").off("change").on("change", function() {
    const checked = $(this).is(":checked");

    $(".tags input:checkbox").prop("checked", checked);
});


function getSelectedContacts() {
    return $(".entries .entry input[type='checkbox']:checked").map(function() {
        return $(this).attr("data-contact-id");
    }).get();
};

$("#SendEmailButton").click(function() {
    function request(model) {
        const url = "/dashboard/address-book/send/bulk-email";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        const model = window.serializeForm($("#EmailModal"));
        model.Contacts = getSelectedContacts();

        return model;
    };

    const model = getModel();

    if (!model.Contacts || !model.Contacts.length) {
        window.displayMessage(window.translate("PleaseSelectAtLeastOneContact"));
        return;
    };

    if (!model.Subject.trim()) {
        window.displayMessage(window.translate("PleaseEnterSubject"));
        return;
    };

    if (!model.Message.trim()) {
        window.displayMessage(window.translate("PleaseEnterMessage"));
        return;
    };

    const ajax = request(model);

    ajax.success(function(result) {
        if (!result) {
            window.displayMessage(window.translate("CouldNotSendEmailSetupProvider"));
            return;
        };

        window.displaySuccess();
        $("#EmailModal").modal("hide");
    });

    ajax.fail(function(xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});

$("#SendSmsButton").click(function() {
    function request(model) {
        const url = "/dashboard/address-book/send/bulk-sms";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        const model = window.serializeForm($("#SmsModal"));
        model.Contacts = getSelectedContacts();
        return model;
    };

    const model = getModel();

    if (!model.Contacts || !model.Contacts.length) {
        window.displayMessage(window.translate("PleaseSelectAtLeastOneContact"));
        return;
    };

    if (!model.Subject.trim()) {
        window.displayMessage(window.translate("PleaseEnterSubject"));
        return;
    };

    if (!model.Message.trim()) {
        window.displayMessage(window.translate("PleaseEnterMessage"));
        return;
    };

    const ajax = request(model);

    ajax.success(function(result) {
        if (!result) {
            window.translate("CouldNotSendSmsSetupProvider");
            return;
        };

        window.displaySuccess();
        $("#SmsModal").modal("hide");
    });

    ajax.fail(function(xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});

loadContacts();
window.initializeUploader();
$(".ui.checkbox").checkbox();
setTimeZone();
initializeUITags();