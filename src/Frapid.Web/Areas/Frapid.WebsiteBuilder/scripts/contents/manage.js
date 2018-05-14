window.overridePath = "/dashboard/website/contents";

$('#TitleInputText').keyup(function () {
    function getAlias(title) {
        return title.toLowerCase().replace(/ +(?= )/g, "").replace(/ /g, "-").replace(/[^\w-]+/g, "");
    };

    $('#AliasInputText').val(getAlias($(this).val()));
});

function save() {
    function request(model) {
        const url = "/dashboard/website/contents/add-or-edit";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        function getEditorContents() {
            const editor = window.ace.edit("editor");
            const contents = editor.getSession().getValue();
            return contents;
        };

        const isMarkdown = $("#IsMarkdownInputCheckbox").is(":checked");
        const model = window.serializeForm($(".content.segment"));

        if (!isMarkdown) {
            model.Contents = getEditorContents();
        };

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
    const editor = window.ace.edit("editor");
    const isMarkdown = $("#IsMarkdownCheckbox").is(":checked");
    const contents = editor.getSession().getValue();

    if (isMarkdown) {
        $("input[data-entity='markdown']").val(contents);
    };

    var html;

    if (isMarkdown) {
        html = window.marked(contents);

        try {
            $("#content").html(html);
        } catch (e) {

        }
        return;
    };

    html = contents;
    $("#content").html(html);
};

var stringUnEncode = function (str) {
    return str.replace(/&amp;/g, "&").replace(/&quot;/g, "\"");
};

function initializeAceEditor() {
    if (!window.ace) {
        return;
    };

    window.html = stringUnEncode(window.html);

    var content = $("input[data-entity='markdown']").val();

    if (!content) {
        content = html;
    };

    const editor = window.ace.edit("editor");
    const contents = editor.getSession().getValue();

    if (contents) {
        return; //Do not load editor more than once.
    };

    editor.$blockScrolling = Infinity;
    $("#editor").removeClass("initially, hidden");
    editor.setTheme("ace/theme/sqlserver");
    editor.getSession().setMode("ace/mode/html");
    editor.setValue(content, -1);

    editor.setOptions({
        maxLines: Infinity
    });

    editor.on("input", function () {
        displayContent();
    });
};


$(document).ready(function () {
    window.initializeSelectApis();
    const target = window.localStorage.getItem("autoOpenTarget");
    if (target) {
        maximize(target);
    };

    window.initializeUITags();
    $(".ui.checkbox").checkbox();

    setTimeout(function () {
        initializeAceEditor();
    }, 2000);
});

function maximize(target, width) {
    window.localStorage.setItem("autoOpenTarget", target);

    const items = $("[data-target]");
    const el = $(`[data-target=${target}]`);
    items.hide();

    if (!el.hasClass("sixteen wide")) {
        el.removeClass(width).addClass("sixteen wide");
        el.fadeIn();
        return;
    };

    el.removeClass("sixteen wide").addClass(width);
    items.fadeIn();
};

setTimeout(function () {
    initializeAceEditor();
    window.loadDatepicker();

    $("input[type='checkbox'][data-value]").each(function () {
        const el = $(this);
        el.prop("checked", el.attr("data-value").toLowerCase() === "true");
    });

}, 2000);
