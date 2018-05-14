//Todo: Remove Semantic UI Dropdown dependency 
//$(".dropdown").dropdown({placeholder: false, forceSelection: false});

var container = $("#container");
var closeButton = $("#CloseButton");


var fileInputFile = $("#FileInputFile");
var fileModal = $("#FileModal");
var folderModal = $("#FolderModal");

var loadButton = $("#LoadButton");

var remoteUploadUrl = $("#RemoteUploadUrl");

var saveButton = $("#SaveButton");

var actionContainer = $("#ActionContainer");
var treeContainer = $(".tree.container");


var uploadButton = $("#UploadButton");
var uploadModal = $('#UploadModal');
var uploadModalMessage = $("#UploadModal .message");
var uploadModalImage = $("#UploadModal .preview");

var viewer = $("#viewer");
var viewerImage = $("#viewer img");


uploadButton.click(function () {
    uploadModalMessage.hide();
    uploadModal.modal({
        blurring: true,
        onHide: function () {
            loadResources();
        }
    }).modal('show');
});

function confirmAction() {
    const areYouSure = window.i18n.AreYouSure || "Are you sure?";
    return confirm(areYouSure);
};

fileInputFile.change(function () {
    window.clearTimeout(window.fileTimeOut);
});


fileInputFile.on("readComplete", function () {
    setTimeout(function () {
        uploadModal.modal("refresh");
    }, 200);
});



fileInputFile.on("done", function () {
    uploadModalMessage.show();

    window.fileTimeOut = setTimeout(function () {
        uploadModalImage.fadeOut(1000, function () {
            uploadModalMessage.hide();
            uploadModalImage.attr("src", "/Static/images/logo.png").fadeIn(1000);
        });
    }, 5000);
});



function setFileUploadHandler(container) {
    var url = "/dashboard/config/file-manager/resources/upload?container={0}";
    url = window.stringFormat(url, container);

    fileInputFile.attr("data-handler", url);
};

function deleteItem(el) {
    function request(resource) {
        var url = "/dashboard/config/file-manager/delete";
        url += "?resource=";
        url += resource;

        return window.getAjaxRequest(url, "DELETE");
    };

    el = $(el);
    var resource = el.attr("data-path");    

    const message = window.stringFormat('Are you sure you want to delete the following file?\n\n/{0}', resource);
    if (!confirm(message)) {
        return;
    };


    const ajax = request(resource);

    ajax.fail(function (xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function () {
        window.displaySuccess();
        loadResources();

        window.ace.edit("editor").setValue("");

        viewerImage.fadeOut(1000, function () {
            viewerImage.attr("src", "");
        });

        $("[data-container]").each(function () {
            const el = $(this);

            if (el.attr("data-container") === resource) {
                el.attr("data-container", "");
            };
        });
    });
};

function save() {
    function request(container, file, contents) {
        const url = "/dashboard/config/file-manager/resources/edit/file";

        const model = {
            container: container,
            file: file,
            contents: contents
        };

        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "PUT", data);
    };
    const el = saveButton;
    var file = el.attr("data-path");

    if (!file) {
        return;
    };

    if (!window.confirmAction()) {
        return;
    };

    
    var container = "";
    const contents = window.ace.edit("editor").getValue();

    if (file.substring(file.length - 5) === ".less") {
        window.less.render(contents, { compress: false }, function (e, output) {
            const compiled = request(container, file.replace(".less", ".css"), output.css);
            compiled.success(function () {
                window.displayMessage("Successfully saved compiled css file.", "success");
            });
        });
        window.less.render(contents, { compress: true }, function (e, output) {
            const compiled = request(container, file.replace(".less", ".min.css"), output.css);
            compiled.success(function () {
                window.displayMessage("Successfully saved minified css file.", "success");
            });
        });
    };


    const ajax = request(container, file, contents);

    ajax.fail(function (xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function () {
        window.displaySuccess();
    });

};

saveButton.click(function () {
    save();
});


$(window).keypress(function (event) {
    if (!(event.which === 115 && event.ctrlKey) && !(event.which === 19)) return true;
    save();
    event.preventDefault();
    return false;
});


function createFile(el) {
    function request(container, file, contents) {
        const url = "/dashboard/config/file-manager/create/file";

        const model = {
            container: container,
            file: file,
            contents: contents
        };

        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "PUT", data);
    };

    el = $(el);
    

    const container = el.parent().find(".container.label").attr("data-container");
    const file = el.parent().find("input").val();

    const ajax = request(container, file, "");

    ajax.fail(function (xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function () {
        fileModal.modal("hide");
        window.displaySuccess();
        loadResources();
    });
};

function createFolder(el) {
    function request(container, folder) {
        var url = "/dashboard/config/file-manager/create/folder";
        url += "?container=";
        url += container;
        url += "&folder=";
        url += folder;

        return window.getAjaxRequest(url, "PUT");
    };

    el = $(el);
    

    const container = el.parent().find(".container.label").attr("data-container");
    const folder = el.parent().find("input").val();

    const ajax = request(container, folder);

    ajax.fail(function (xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function () {
        folderModal.modal("hide");
        window.displaySuccess();
        loadResources();
    });
};

function toggleEditorTheme() {
    const editor = window.ace.edit("editor");
    const theme = editor.getTheme();

    if (theme === "ace/theme/xcode") {
        editor.setTheme("ace/theme/monokai");
    } else {
        editor.setTheme("ace/theme/xcode");
    };
};

function displayBlob(path) {
    path = "/dashboard/config/file-manager/blob?file=" + path;

    var format = path.split('.').pop().toLowerCase();

    if (isContent(format)) {
        $.get(path, function (response) {
            const editor = window.ace.edit("editor");
            editor.setValue(response, -1);
            setAceMode(format);
        }, "text");

        saveButton.show();
        $("#editor").fadeIn(300);
        return;
    } else {
        const editor = window.ace.edit("editor");
        editor.setValue("");
        setAceMode("");
    };

    saveButton.hide();
    viewer.hide();
    $("#editor").hide();
};

function isContent(extension) {
    const candidates = ["html", "cshtml", "vbhtml", "xml", "config", "js", "json", "css", "less", "scss", "md"];
    if (candidates.indexOf(extension) !== -1) {
        return true;
    };
    return false;
};

function ToWellKnownType(extension) {
    const candidates = [
        {
            extensions: ["json", "js", "jscript", "jsfile"],
            wellKnownExtension: ["javascript"]
        },
        {
            extensions: ["cshtml", "html", "html"],
            wellKnownExtension: ["html"]
        },
        {
            extensions: ["md"],
            wellKnownExtension: ["markdown"]
        },
        {
            extensions: ["config", "xml"],
            wellKnownExtension: ["xml"]
        }
    ];


    const match = window.Enumerable
        .From(candidates)
        .Where(function (x) {
            return x.extensions.indexOf(extension) !== -1;
        }).FirstOrDefault();


    if (match) {
        return match.wellKnownExtension;
    };

    return extension;
};

function setAceMode(extension) {
    const editor = window.ace.edit("editor");

    if (!extension) {
        editor.getSession().setMode("ace/mode/html");
    };

    extension = ToWellKnownType(extension);

    const mode = "ace/mode/" + (extension || "html");
    editor.getSession().setMode(mode);
};

function isImage(extension) {
    const candidates = ["png", "jpg", "jpeg", "tiff", "bmp", "gif"];
    if (candidates.indexOf(extension) !== -1) {
        return true;
    };
    return false;
};

function displayImage(path) {
    const format = path.split('.').pop().toLowerCase();

    if (!isImage(format)) {
        displayBlob(path);
        return;
    };

    path = "/dashboard/config/file-manager/blob?file=" + path;

    saveButton.hide();

    viewerImage.attr("src", path).show();

    viewer.hide().fadeIn(300);
    $("#editor").hide();
};

function loadTree(model) {
    container.jstree('destroy');
    container.jstree({
        'core': {
            'data': model
        }
    }).on('changed.jstree', function (e, data) {
        const parent = data.instance.get_node(data.instance.get_node(data.selected[0]).parent).original;
        const model = data.instance.get_node(data.selected[0]).original;
        const path = model.path;

        var container = "";

        if (parent) {
            container = parent.path;
        };

        if (model.isDirectory) {
            container = model.path;
        };

        $("[data-container]").attr("data-container", container).removeClass("disabled");
        $(".container.label").text(container || "~");
        $("[data-path]").attr("data-path", model.path).removeClass("disabled");

        setFileUploadHandler(container);
        displayImage(path);
    }).on('loaded.jstree', function (e, data) {
        const instance = $(container).jstree(true);
        for (let i in instance._model.data) {
            if (instance._model.data.hasOwnProperty(i)) {
                if (i === "#") {
                    continue;
                };

                if (instance._model.data[i].parents.length < 4) {
                    instance._open_to(i);
                };
            };
        };
    });
};

function loadResources() {
    function request() {
        const url = "/dashboard/config/file-manager/resources";
        return window.getAjaxRequest(url);
    };

    const ajax = request();

    ajax.fail(function (xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function (response) {
        loadTree(response);
        setFileUploadHandler("");
    });

    treeContainer.fadeIn(500);
};


var stringUnEncode = function (str) {
    return str.replace(/&amp;/g, '&').replace(/&quot;/g, "\"");
};

function initializeAceEditor() {
    if (!window.ace) {
        return;
    };

    const editor = window.ace.edit("editor");
    editor.$blockScrolling = Infinity;
    $("#editor").removeClass("initially, hidden");
    editor.setTheme("ace/theme/xcode");
    editor.setOption("showPrintMargin", false);
    setAceMode("");
};

initializeAceEditor();


window.initializeUploader();



loadResources();
