window.overridePath = "/dashboard/website/blogs";


$('#TitleInputText').keyup(function () {
    function getAlias(title) {
        return title.toLowerCase().replace(/ +(?= )/g, "").replace(/ /g, "-").replace(/[^\w-]+/g, "");
    };

    $('#AliasInputText').val(getAlias($(this).val()));
});


function save() {
    function request(model) {
        const url = "/dashboard/website/blogs/add-or-edit";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        const model = window.serializeForm($(".content.segment"));
        model.IsHomePage = false;
        model.IsMarkdown = false;

        return model;
    };

    const model = getModel();
    if (!model) {
        return;
    };

    const ajax = request(model);


    ajax.success(function (response) {
        window.displaySuccess();
        var target;

        if (!window.getQueryStringByName("ContentId")) {
            target = window.updateQueryString("ContentId", response);
            document.location.href = target;
        };
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
};

$("#CancelButton").click(function () {
    const target = decodeURIComponent(window.getQueryStringByName("ReturnUrl")) || "../contents";

    location.href = target;
});

$("#SaveButton").click(function () {
    save();
});

$(window).keypress(function (event) {
    if (!(event.which === 115 && event.ctrlKey) && !(event.which === 19)) return true;
    save();
    event.preventDefault();
    return false;
});

function displayContent() {
    alert("todo");
};

$(document).ready(function () {
    window.initializeSelectApis();
    window.initializeUITags();
    $(".ui.checkbox").checkbox();
    window.loadDatepicker();

    $("input[type='checkbox'][data-value]").each(function () {
        const el = $(this);
        el.prop("checked", el.attr("data-value").toLowerCase() === "true");
    });
    setTimeout(function () {
        $("#ContentsTextArea").trumbowyg({ svgPath: "/scripts/trumbowyg/dist/ui/icons.svg", autogrow: true });
        $("#ContentsTextArea").show();
    }, 1000);
});


setTimeout(function () {
    $("#ContentsTextArea").trumbowyg({ svgPath: "/scripts/trumbowyg/dist/ui/icons.svg", autogrow: true });
    $("#ContentsTextArea").show();
}, 1000);
