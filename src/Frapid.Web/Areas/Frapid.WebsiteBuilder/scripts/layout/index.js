//Todo: Remove Semantic UI Dropdown dependency 
//$(".dropdown").dropdown({placeholder: false, forceSelection: false});

var container = $("#container");
var closeButton = $("#CloseButton");

var deleteThemeModal = $("#DeleteThemeModal");
var deleteThemeModalExceptionMessageHeader = $("#DeleteThemeModal .exception.header");
var deleteThemeButton = $("#DeleteThemeButton");

var fileInputFile = $("#FileInputFile");
var fileModal = $("#FileModal");
var folderModal = $("#FolderModal");

var loadButton = $("#LoadButton");

var newThemeForm = $("#NewThemeModal .form");
var newThemeModal = $("#NewThemeModal");
var newThemeModalSegment = $("#NewThemeModal .segment");
var newThemeModalErrorMessageHeader = $("#NewThemeModal .error.message .header");
var newThemeModalErrorMessage = $("#NewThemeModal .error.message");

var remoteUploadUrl = $("#RemoteUploadUrl");

var saveButton = $("#SaveButton");

var themeNameText = $("#DeleteThemeModal .theme.name.text");

var themeContainer = $(".theme.container");
var themeDropdown = $("#ThemeDropdown");
var actionContainer = $("#ActionContainer");
var treeContainer = $(".tree.container");


var uploadButton = $("#UploadButton");
var uploadModal = $("#UploadModal");
var uploadModalMessage = $("#UploadModal .message");
var uploadModalImage = $("#UploadModal .preview");

var viewer = $("#viewer");
var viewerImage = $("#viewer img");
var uploadThemeInputFile = $("#UploadThemeInputFile");


uploadButton.click(function() {
    uploadModalMessage.hide();
    uploadModal.modal({
        blurring: true,
        onHide: function() {
            loadResources();
        }
    }).modal("show");
});

function confirmAction() {
    const areYouSure = window.translate("AreYouSure") || "Are you sure?";
    return confirm(areYouSure);
};

fileInputFile.change(function() {
    window.clearTimeout(window.fileTimeOut);
});


fileInputFile.on("readComplete", function() {
    setTimeout(function() {
        uploadModal.modal("refresh");
    }, 200);
});


fileInputFile.on("done", function() {
    uploadModalMessage.show();

    window.fileTimeOut = setTimeout(function() {
        uploadModalImage.fadeOut(1000, function() {
            uploadModalMessage.hide();
            uploadModalImage.attr("src", "/Static/images/logo.png").fadeIn(1000);
        });
    }, 5000);
});


closeButton.click(function() {
    themeContainer.fadeIn(500);
    treeContainer.hide();
    actionContainer.hide();
});

function setFileUploadHandler(container) {
    var url = "/dashboard/my/website/themes/resources/upload?themeName={0}&container={1}";
    const theme = themeDropdown.val();
    url = window.stringFormat(url, theme, container);

    fileInputFile.attr("data-handler", url);
};

function deleteItem(el) {
    function request(theme, resource) {
        var url = "/dashboard/my/website/themes/resources/delete?themeName=";
        url += theme;
        url += "&resource=";
        url += resource;

        return window.getAjaxRequest(url, "DELETE");
    };

    el = $(el);
    var resource = el.attr("data-path");
    const theme = themeDropdown.val();

    //"Are you sure you want to delete the following file?\n\n/Themes/{0}/{1}"
    const message = window.stringFormat(window.translate("FileDeleteConfirmationMessage"), theme, resource);

    if (!confirm(message)) {
        return;
    };


    const ajax = request(theme, resource);

    ajax.fail(function(xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function() {
        window.displaySuccess();
        loadResources();

        window.ace.edit("editor").setValue("");

        viewerImage.fadeOut(1000, function() {
            viewerImage.attr("src", "");
        });

        $("[data-container]").each(function() {
            const el = $(this);

            if (el.attr("data-container") === resource) {
                el.attr("data-container", "");
            };
        });
    });
};

function save() {
    function request(theme, container, file, contents) {
        const url = "/dashboard/my/website/themes/resources/edit/file";

        const model = {
            themeName: theme,
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

    var theme = themeDropdown.val();
    var container = "";
    const contents = window.ace.edit("editor").getValue();

    if (file.substring(file.length - 5) === ".less") {
        window.less.render(contents, { compress: false }, function(e, output) {
            const compiled = request(theme, container, file.replace(".less", ".css"), output.css);
            compiled.success(function() {
                window.displayMessage(window.translate("SuccessfullySavedCompiledCssFile"), "success");
            });
        });
        window.less.render(contents, { compress: true }, function(e, output) {
            const compiled = request(theme, container, file.replace(".less", ".min.css"), output.css);
            compiled.success(function() {
                window.displayMessage(window.translate("SuccessfullySavedMinifiedCssFile"), "success");
            });
        });
    };

    if (!theme) {
        return;
    };

    const ajax = request(theme, container, file, contents);

    ajax.fail(function(xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function() {
        window.displaySuccess();
    });

};

saveButton.click(function() {
    save();
});


$(window).keypress(function(event) {
    if (!(event.which === 115 && event.ctrlKey) && !(event.which === 19)) return true;
    save();
    event.preventDefault();
    return false;
});


function createFile(el) {
    function request(theme, container, file, contents) {
        const url = "/dashboard/my/website/themes/resources/create/file";

        const model = {
            themeName: theme,
            container: container,
            file: file,
            contents: contents
        };

        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "PUT", data);
    };

    el = $(el);
    const theme = themeDropdown.val();
    const container = el.parent().find(".container.label").attr("data-container");
    const file = el.parent().find("input").val();

    const ajax = request(theme, container, file, "");

    ajax.fail(function(xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function() {
        fileModal.modal("hide");
        window.displaySuccess();
        loadResources();
    });
};

function createFolder(el) {
    function request(theme, container, folder) {
        var url = "/dashboard/my/website/themes/resources/create/folder?themeName=";
        url += theme;
        url += "&container=";
        url += container;
        url += "&folder=";
        url += folder;

        return window.getAjaxRequest(url, "PUT");
    };

    el = $(el);
    const theme = themeDropdown.val();
    const container = el.parent().find(".container.label").attr("data-container");
    const folder = el.parent().find("input").val();

    const ajax = request(theme, container, folder);

    ajax.fail(function(xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function() {
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
    const theme = themeDropdown.val();
    path = `/dashboard/my/website/themes/blob?themeName=${theme}&file=${path}`;

    var format = path.split(".").pop().toLowerCase();

    if (isContent(format)) {
        $.get(path, function(response) {
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
        .Where(function(x) {
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

    const mode = `ace/mode/${extension || "html"}`;
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
    const theme = themeDropdown.val();
    const format = path.split(".").pop().toLowerCase();

    if (!isImage(format)) {
        displayBlob(path);
        return;
    };

    path = `/dashboard/my/website/themes/blob?themeName=${theme}&file=${path}`;

    saveButton.hide();

    viewerImage.attr("src", path).show();

    viewer.hide().fadeIn(300);
    $("#editor").hide();
};

function loadTree(model) {
    container.jstree("destroy");
    container.jstree({
        'core': {
            'data': model
        }
    }).on("changed.jstree", function(e, data) {
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
    }).on("loaded.jstree", function(e, data) {
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
    function request(themeName) {
        const url = `/dashboard/my/website/themes/resources?themeName=${themeName}`;
        return window.getAjaxRequest(url);
    };

    const themeName = themeDropdown.val();
    $("[data-theme]").text(themeName);

    if (!themeName) {
        return;
    };

    const ajax = request(themeName);

    ajax.fail(function(xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function(response) {
        loadTree(response);
        setFileUploadHandler("");
    });

    themeContainer.hide();
    treeContainer.fadeIn(500);
};

loadButton.click(function() {
    loadResources();
    actionContainer.show();
});

function loadThemes() {
    function request() {
        const url = "/dashboard/my/website/themes";
        return window.getAjaxRequest(url);
    };

    const ajax = request();

    ajax.fail(function(xhr) {
        window.displayMessage(window.getAjaxErrorMessage(xhr));
    });

    ajax.success(function(response) {
        themeDropdown.bindAjaxData(response, false, null, null, null, true);
    });
};

var stringUnEncode = function(str) {
    return str.replace(/&amp;/g, "&").replace(/&quot;/g, "\"");
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
loadThemes();


window.initializeUploader();

function displayNewThemeModal() {
    newThemeModalErrorMessageHeader.html("");
    newThemeModalErrorMessage.hide();

    newThemeModal.find("form")[0].reset();

    newThemeModal.modal({ blurring: true })
        .modal("setting", "closable", false)
        .modal("show");
};


function remoteUpload() {
    function request(remoteUrl) {
        var url = "/dashboard/my/website/themes/upload/remote?url=";
        url += remoteUrl;

        return window.getAjaxRequest(url, "POST");
    };

    const url = remoteUploadUrl.val();

    if (window.isNullOrWhiteSpace(url)) {
        return;
    };

    onNewThemeBegin();
    const ajax = request(url);

    ajax.success(function(response) {
        if (!response.success) {
            const error = response.error;
            onNewThemeError(error);
            return;
        };

        onNewThemeComplete();
    });

    ajax.fail(function(xhr) {
        const error = window.getAjaxErrorMessage(xhr);
        onNewThemeError(error);
    });
};

function createTheme() {
    function request(model) {
        const url = "/dashboard/my/website/themes/create";
        const data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function validate() {
        return window.validator.validate(newThemeForm, null, true);
    };

    const isValid = validate();

    if (!isValid) {
        return;
    };

    const model = window.serializeForm(newThemeForm);

    onNewThemeBegin();

    const ajax = request(model);

    ajax.success(function() {
        onNewThemeComplete();
    });

    ajax.fail(function(xhr) {
        const error = window.getAjaxErrorMessage(xhr);
        onNewThemeError(error);
    });
};

window.validator.initialize(newThemeForm);

function onNewThemeBegin() {
    newThemeModalSegment.addClass("loading");
};

function onNewThemeError(error) {
    newThemeModalSegment.removeClass("loading");
    newThemeModalErrorMessageHeader.html(error);
    newThemeModalErrorMessage.show();
};

function onNewThemeComplete() {
    newThemeModalSegment.removeClass("loading");
    newThemeModal.modal("hide");
    window.displaySuccess();
    window.loadThemes();
};

uploadThemeInputFile.change(function() {
    onNewThemeBegin();
});

uploadThemeInputFile.on("error", function(e, data) {
    const error = data.message;
    onNewThemeError(error);
});

uploadThemeInputFile.on("done", function(e, data) {
    if (data.response.success) {
        onNewThemeComplete();
        return;
    };

    const error = data.response;
    onNewThemeError(error);
    return;
});

function displayDeleteThemeModal() {
    const theme = themeDropdown.val();

    if (!theme) {
        return;
    };

    themeNameText.text(theme);

    deleteThemeModal.modal({
        blurring: true
    }).modal("show");
};

function showDeleteThemeModal() {
    deleteThemeModalExceptionMessageHeader.text("").parent().hide();
    $("#DeleteThemeModal").modal("hide");
};

deleteThemeButton.click(function() {
    function request(theme) {
        var url = "/dashboard/my/website/themes/delete?themeName=";
        url += theme;

        return window.getAjaxRequest(url, "DELETE");
    };

    if (!window.confirmAction()) {
        return;
    };

    const theme = themeNameText.text();
    if (!theme) {
        return;
    };

    const ajax = request(theme);

    ajax.success(function() {
        deleteThemeModal.modal("hide");
        loadThemes();
        //Todo: Remove Semantic UI Dropdown dependency 
        // themeDropdown.dropdown("clear");
        themeDropdown.find(":selected").removeAttr("selected");

        window.displaySuccess();
    });

    ajax.fail(function(response) {
        const error = response.responseText;
        deleteThemeModalExceptionMessageHeader.text(error).parent().show();
    });
});