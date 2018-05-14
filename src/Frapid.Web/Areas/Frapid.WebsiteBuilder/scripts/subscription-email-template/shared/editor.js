$("#SaveButton").click(function() {
    function request(model) {
        const url = window.saveUrl;
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        const editor = window.ace.edit("editor");
        const contents = editor.getSession().getValue();

        return {
            Type: window.type,
            Contents: contents
        };
    };

    const model = getModel();
    const ajax = request(model);

    ajax.success(function() {
        window.displaySuccess();
    });
});

var stringUnEncode = function(str) {
    return str.replace(/&amp;/g, "&").replace(/&quot;/g, "\"");
};

function initializeAceEditor() {
    if (!window.ace) {
        return;
    };

    window.html = stringUnEncode(window.html);
    const content = window.html;

    const editor = window.ace.edit("editor");
    editor.$blockScrolling = Infinity;
    $("#editor").removeClass("initially, hidden");
    editor.setTheme("ace/theme/sqlserver");
    editor.getSession().setMode("ace/mode/html");
    editor.setValue(content, -1);
};

initializeAceEditor();