function getFileName() {
    var filterName = getFilterName();

    if (filterName) {
        filterName += "-";
    };

    return window.scrudFactory.title + "-" + filterName + stringFormat(window.translate("PageN"), getPageNumber());
};

function createPDF() {
    var table = $("#ScrudView").clone();
    table.find("thead tr:nth-child(2)").remove();

    printGridView(window.scrudFactory.title, table, '', 2, 0, $("#MarkupHidden"), downloadPDF);
};

function createXls() {
    var table = $("#ScrudView").clone();
    table.find("thead tr:nth-child(2)").remove();

    printGridView(window.scrudFactory.title, table, '', 2, 0, $("#MarkupHidden"), downloadExcel);
};

function createDoc() {
    var table = $("#ScrudView").clone();
    table.find("thead tr:nth-child(2)").remove();

    printGridView(window.scrudFactory.title, table, '', 2, 0, $("#MarkupHidden"), downloadWord);
};

function print() {
    var table = $("#ScrudView").clone();
    table.find("thead tr:nth-child(2)").remove();

    printGridView(window.scrudFactory.title, table, '', 2, 0);
};


function startDownload(path, fileName) {
    var anchor = $("#DownloadAnchor");
    anchor.attr("target", "_self");
    anchor.attr("href", path);
    anchor.attr("download", fileName);
    anchor[0].click();
};


function downloadFile(extension) {
    function request(model) {
        var url = "/dashboard/reports/export/" + extension;
        
        var data = JSON.stringify(model);
        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel(){
        var html = $("#MarkupHidden").val();
        var fileName = getFileName() + "." + extension;
        
        return {
            Html: html,
            DocumentName: fileName
        };      
    };
    
    var model = getModel();
    var ajax = request(model);

    ajax.success(function (response) {
        startDownload(response, model.DocumentName);
    });
    
    ajax.fail(function(xhr){
        window.logAjaxErrorMessage(xhr);
    });
};

function downloadPDF() {
    downloadFile("pdf");
};


function downloadExcel() {
    downloadFile("xls");
};


function downloadWord() {
    downloadFile("docx");
};

