loadDatepicker();

function setHeight(el) {
    const candidates = $(".query.container, .report-panel iframe");
    const height = (el.contentDocument.body.scrollHeight + 100) + 'px';
    candidates.css("height", height);
};

(function () {
    const persist = JSON.parse(localStorage.getItem(document.location.pathname));

    if (persist) {
        $.each(persist, function () {
            const el = $(`#${this.id}`);
            const value = this.value;
            var defaultValue;

            if (el.is("select")) {
                defaultValue = el.attr("data-default-value");

                if (!defaultValue) {
                    el.attr("data-default-value", value);
                };
            } else {
                if (el.is(".date")) {
                    const date = new Date(value);
                    el.datepicker("setDate", date);
                } else {
                    el.val(value);
                };
            };
        });
    };


    const candidates = $("select[data-url]");

    $.each(candidates, function () {
        var el = $(this);
        const url = el.attr("data-url");
        const keyField = el.attr("data-key-field");
        const valueField = el.attr("data-value-field");
        var defaultValue = el.attr("data-default-value");
        const optional = el.attr("data-optional").toLowerCase().indexOf("t") > -1;


        window.ajaxDataBind(url, el, null, keyField, valueField, defaultValue, function () {
            setTimeout(function () {

                if (defaultValue) {
                    el.val(defaultValue);
                };

                if (optional) {
                    el.prepend("<option value='null'>Select</option>");

                    if (defaultValue === "0") {
                        el.find(":selected").removeAttr("selected");
                        el.find("option:first-child").prop("selected", true);
                    };
                };
            }, 100);
        }, false, true);
    });
})();

$("#ShowButton").off("click").on("click", function () {
    var persist = [];

    var query = [];

    const dateEls = $(".parameter.form .field input.date");

    $.each(dateEls, function () {
        const el = $(this);
        const parameter = el.attr("data-paramter");
        const value = el.datepicker("getDate").valueOf();
        const id = el.attr("id");

        persist.push({
            id: id,
            value: value
        });

        if (parameter) {
            query.push(parameter + "=" + value);
        };
    });

    const otherEls = $(".parameter.form .field input:not(.date), .parameter.form .field select");

    $.each(otherEls, function () {
        const el = $(this);
        const parameter = el.attr("data-paramter");
        var value = el.val();

        if (el.is("input[type='checkbox']")) {
            value = el.is(":checked");
        };

        const id = el.attr("id");

        persist.push({
            id: id,
            value: value
        });

        if (parameter) {
            query.push(parameter + "=" + value);
        };
    });

    localStorage.setItem(document.location.pathname, JSON.stringify(persist));

    const location = document.location.pathname + "?" + query.join("&");
    document.location = location;
});

function startDownload(path, fileName) {
    const anchor = $("#DownloadAnchor");
    anchor.attr("target", "_self");
    anchor.attr("href", path);
    anchor.attr("download", fileName);
    anchor[0].click();
};

function getReportHtml() {
    var html;

    if ($("#ReportIframe").length) {
        const source = $("#ReportIframe").contents().find("html")[0].outerHTML;
        html = `<!DOCTYPE html>${source}`;
    } else {
        html = document.documentElement.outerHTML;;
    }

    return html;
};

function downloadFile(extension) {
    function request(model) {
        const url = `/dashboard/reports/export/${extension}`;

        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        const html = getReportHtml();
        const documentName = $(".report.title").html();

        return {
            Html: html,
            DocumentName: documentName
        };
    };

    var model = getModel();
    const ajax = request(model);

    ajax.success(function (response) {
        startDownload(response, model.DocumentName);
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
};

$(".pdf.button").off("click").on("click", function () {
    downloadFile("pdf");
});

$(".excel.button").off("click").on("click", function () {
    downloadFile("xls");
});

$(".word.button").off("click").on("click", function () {
    downloadFile("docx");
});

function getXml(reportTitle) {
    function normalize(string) {
        string = string.replace(" ", "");
        string = string.replace("#", "");
        return string;
    };

    function getHeaders(table) {
        const headerCells = table.find("thead tr th");

        var members = [];

        $.each(headerCells, function () {
            const cell = $(this).html();
            members.push(normalize(cell));
        });

        return members;
    };


    var contents = "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>";
    contents += "\n";

    if (!window.metaView) {
        window.metaView = {};
    };

    contents += `<Report title="${reportTitle}"  exportedOn="${(new Date()).toLocaleString()}" exportedBy="${window
        .metaView.Name}" userId="${window.metaView.UserId}" loginId="${window.metaView.LoginId}" officeId="${window
        .metaView.OfficeId}">`;

    var tables;

    if ($("#ReportIframe").length) {
        tables = $("#ReportIframe").contents().find(".gridviews table");
    } else {
        tables = $(".gridviews table");
    }

    $.each(tables, function () {
        const table = $(this);
        const rows = table.find("tbody tr");

        contents += "\n";
        contents += "\t<Candidates>";
        contents += "\n";
        var headers = getHeaders(table);


        $.each(rows, function () {
            const row = $(this);
            const cells = row.find("td");

            contents += "\t\t<Item>";
            contents += "\n";
            $.each(cells, function (i, v) {
                const text = $(this).text();

                const header = headers[i];

                contents += `\t\t\t<Element id="${header}">`;
                contents += "\n";
                contents += "\t\t\t\t";
                contents += text;
                contents += "\n";
                contents += "\t\t\t</Element>";
                contents += "\n";

            });
            contents += "\t\t</Item>";
            contents += "\n";
        });

        contents += "\t</Candidates>";
    });

    contents += "\n";
    contents += "</Report>";
    contents += "\n";
    return contents;
};

function getTableInText() {
    var source;

    if ($("#ReportIframe").length) {
        source = $("#ReportIframe").contents();
    } else {
        source = $("html");
    }

    const headerCells = source.find(".gridviews table:first thead tr th");
    const rows = source.find(".gridviews table:first tbody tr");

    var contents = "";

    var members = [];

    $.each(headerCells, function () {
        const cell = $(this);
        members.push(cell.html());
    });

    contents += members.join(",");
    members = [];
    contents += "\n";

    $.each(rows, function () {
        const row = $(this);
        const cells = row.find("td");

        $.each(cells, function () {
            const cell = $(this);

            members.push(cell.html());
        });

        contents += members.join(",");
        contents += "\n";
        members = [];
    });

    return contents;
};


$(".xml.button").off("click").on("click", function () {
    const documentName = $(".report.title").html();
    const xml = getXml(documentName);
    const href = `data:text/plain;charset=utf-8,${encodeURIComponent(xml)}`;

    startDownload(href, documentName + ".xml");
});

$(".text.button").off("click").on("click", function () {
    const documentName = $(".report.title").html();
    const text = getTableInText(documentName);
    const href = `data:text/plain;charset=utf-8,${encodeURIComponent(text)}`;

    startDownload(href, documentName + ".txt");
});

var fontSize = 1;

$(".zoom.in.button").off("click").on("click", function () {
    var source;

    if ($("#ReportIframe").length) {
        source = $("#ReportIframe").contents();
    } else {
        source = $("html");
    };

    fontSize += 0.25;

    if (fontSize > 2) {
        fontSize = 2;
    };

    source.find("body").css("font-size", fontSize + "em");
});


$(".mail.button").off("click").on("click", function () {
    $('#MessageTextArea').trumbowyg({ svgPath: "/scripts/trumbowyg/dist/ui/icons.svg" });
    $("#EmailModal").modal("show");
});


$("#SendEmailButton").off("click").on("click", function () {
    function request(model) {
        const url = "/dashboard/reports/email";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        const model = window.serializeForm($("#EmailModal"));
        model.Html = getReportHtml();

        return model;
    };

    const valid = window.validator.validate($("#EmailModal"));

    if (!valid) {
        return;
    };

    $("#EmailModal").find(".form.segment").addClass("loading");

    const model = getModel();
    const ajax = request(model);

    ajax.success(function () {
        $("#EmailModal").find(".form.segment").removeClass("loading");
        window.displaySuccess();
        $("#EmailModal").modal("hide");
    });

    ajax.fail(function (xhr) {
        $("#EmailModal").find(".form.segment").removeClass("loading");
        window.logAjaxErrorMessage(xhr);
    });
});

$(".zoom.out.button").off("click").on("click", function () {
    var source;

    if ($("#ReportIframe").length) {
        source = $("#ReportIframe").contents();
    } else {
        source = $("html");
    };

    fontSize -= 0.25;

    if (fontSize < 0.75) {
        fontSize = 0.75;
    };

    source.find("body").css("font-size", fontSize + "em");
});

window.validator.initialize($("#EmailModal"));

function removeToolbar() {
    if ($("iframe").length) {
        $($("iframe")[0].contentWindow.document).find(".toolbar").remove();
    };
};

if (top.parent) {
    if(typeof(top.parent.removeToolbar) === "function"){
        top.parent.removeToolbar();
    };
};

$("input[type='checkbox'][data-checked='true']").prop("checked", true);