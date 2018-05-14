var printGridView = function (reportTitle, table, windowName, offset, offsetLast, hiddenFieldToUpdate, callback) {
    const templatePath = "/report/assets/print.html";
    const headerPath = "/dashboard/reports/header";

    //Load report template from the path.
    $.get(templatePath, function () { }).done(function (data) {
        //Load report header template.
        $.get(headerPath, function () { }).done(function (header) {

            table.find("tr.tableFloatingHeader").remove();


            if(offset){
                table.find("th:nth-child(-n" + offset + ")").remove();
                table.find("td:nth-child(-n" + offset + ")").remove();                
            };

            if(offsetLast){
                table.find("th:nth-last-child(-n" + offsetLast + ")").remove();
                table.find("td:nth-last-child(-n" + offsetLast + ")").remove();
            }

            table.find("td").removeAttr("style");
            table.find("tr").removeAttr("style");

            table = "<table class='preview'>" + table.html() + "</table>";

            data = data.replace(/{Header}/g, header);
            data = data.replace(/{ReportHeading}/g, reportTitle);
            data = data.replace(/{PrintDate}/g, (new Date()).toLongDateString());
            data = data.replace(/{UserName}/g, user);
            data = data.replace(/{OfficeCode}/g, office);
            data = data.replace(/{Table}/g, table);

            if (hiddenFieldToUpdate) {
                //Update the hidden field with data, but do not print.
                $(hiddenFieldToUpdate).val(data);
                if (typeof (callback) === "function") {
                    callback();
                };

                return;
            };

            //Creating and opening a new window to display the report.
            var w = window.open('', windowName,
                + ',menubar=0'
                + ',toolbar=0'
                + ',status=0'
                + ',scrollbars=1'
                + ',resizable=0');
            w.moveTo(0, 0);
            w.resizeTo(screen.width, screen.height);


            //Writing the report to the window.
            w.document.writeln(data);
            w.document.close();

            //Report sent to the browser.
        });
    });
};
