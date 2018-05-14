function shuffle(o) {
    for (var j, x, i = o.length; i; j = Math.floor(Math.random() * i), x = o[--i], o[i] = o[j], o[j] = x);
    return o;
};

//var chartColors = ["#DF0101", "#DF3A01", "#DF7401", "#DBA901", "#D7DF01", "#A5DF00", "#74DF00", "#3ADF00", "#01DF74", "#01DFA5", "#01DFD7", "#01A9DB", "#0174DF", "#013ADF", "#0101DF", "#3A01DF", "#7401DF", "#A901DB", "#DF01D7", "#DF01A5", "#DF0174", "#DF013A", "#6E6E6E"];
var chartColors = ["#FF6384", "#4BC0C0", "#FFCE56", "#36A2EB", "#005BAC", "#CC6858", "#00894F", "#7D6B55", "#938F3A", "#C3CA00", "#917199", "#DF816E", "#CB5252", "#83CCD2", "#509D69", "#8F939F", "#D5B329", "#A76283", "#70A68C", "#E95383", "#A6BAB2", "#D5B329", "#4994C4", "#009591", "#4E4770", "#BA5054", "#D6E9C4", "#32ADC6"];

function getFillColor(index) {
    var color = hexToRgb(chartColors[index]);
    var opacity = 0.8;
    return "rgba(" + color.r + "," + color.g + "," + color.b + "," + opacity + ")";
};

function hexToRgb(hex) {
    // Expand shorthand form (e.g. "03F") to full form (e.g. "0033FF")
    var shorthandRegex = /^#?([a-f\d])([a-f\d])([a-f\d])$/i;
    hex = hex.replace(shorthandRegex, function (m, r, g, b) {
        return r + r + g + g + b + b;
    });

    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
        r: parseInt(result[1], 16),
        g: parseInt(result[2], 16),
        b: parseInt(result[3], 16)
    } : null;
};

function prepare1DChart(datasourceId, canvasId, legendId, type, remove, titleColumnIndex, valueColumnIndex) {
    //chartColors = shuffle(chartColors);
    var table = $("#" + datasourceId);
    var labels = [];
    var datasets = [];

    labels.push("");

    table.find("tr").not(":first").each(function (i) {
        //Get an instance of the current row
        var row = $(this);
        var title = row.find("td:eq(" + parseInt(titleColumnIndex) + ")").text();

        var value = parseFloat2(row.find("td:eq(" + parseInt(valueColumnIndex) + ")").text());

        var dataset =
        {
            backgroundColor: getFillColor(i),
            borderColor: "transparent",
            data: [value],
            label: title
        };

        //Add the dataset object to the array object.
        datasets.push(dataset);

    });


    if (remove) {
        table.remove();
    };

    var reportData = {
        labels: labels,
        datasets: datasets
    };


    var ctx = document.getElementById(canvasId).getContext("2d");

    switch (type) {
        case "line":
            new Chart(ctx, { type: "line", data: reportData });
            break;
        case "radar":
            new Chart(ctx, { type: "radar", data: reportData });
            break;
        default:
            new Chart(ctx, { type: "bar", data: reportData });
            break;
    };

    //legend(document.getElementById(legendId), reportData);
};

function prepareReportChart(datasourceId, canvasId, legendId, type, hide, titleColumnIndex, valueColumnIndex) {
    var pieCharts = ["polararea", "pie", "doughnut"];

    if ($.inArray(type, pieCharts) === -1) {
        prepare1DChart(datasourceId, canvasId, legendId, type, hide, titleColumnIndex, valueColumnIndex);
        return;
    };

    preparePieChart(datasourceId, canvasId, legendId, type, hide, titleColumnIndex, valueColumnIndex);
};

function preparePieChart(datasourceId, canvasId, legendId, type, hide, titleColumnIndex, valueColumnIndex) {
    //chartColors = shuffle(chartColors);
    var table = $("#" + datasourceId);
    var value;
    var data = { title: "", labels: [], datasets: [{ data: [], backgroundColor: [] }] };

    if (typeof titleColumnIndex === "undefined") {
        titleColumnIndex = 0;
    };

    if (typeof valueColumnIndex === "undefined") {
        valueColumnIndex = 1;
    };

    //Reset the counter.
    var counter = 0;

    //Loop through each row of the table body.
    table.find("tr").not(":first").each(function () {
        //Get an instance of the current row
        var row = $(this);

        //The first column of each row is the legend.
        var title = row.find("td:eq(" + parseInt(titleColumnIndex) + ")").text();

        //The first column of each row is the legend.
        value = parseFloat2(row.find("td:eq(" + parseInt(valueColumnIndex) + ")").text());

        data.labels.push(title);
        data.datasets[0].data.push(value);
        data.datasets[0].backgroundColor.push(chartColors[counter]);

        //Add the dataset object to the array object.
        counter++;
    });

    var ctx = document.getElementById(canvasId).getContext("2d");

    var animation = { animateScale: true };

    if (typeof (window.chartAnimation) !== "undefined") {
        animation = window.chartAnimation;
    };

    var options = {
        animation: animation
    };

    switch (type) {
        case "doughnut":
            new Chart(ctx, {
                type: 'doughnut',
                data: data,
                options: options
            });
            break;
        case "polararea":
            new Chart(ctx, {
                type: 'polarArea',
                data: data,
                options: options
            });
            break;
        default:
            new Chart(ctx, {
                type: 'pie',
                data: data,
                options: options
            });
            break;
    };

    //legend(document.getElementById(legendId), data);
    if (hide) {
        table.hide();
    };
};