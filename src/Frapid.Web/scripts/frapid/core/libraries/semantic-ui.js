//Semantic UI
var semanticGrid = [undefined, "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen"];

$(document).ready(function () {
    var tabItems = $('.tabular .item');

    if (tabItems && tabItems.length > 0) {
        tabItems.tab();
    };

    //Semantic UI Button Support
    var buttons = $(".buttons .button");

    buttons.on("click", function () {
        buttons.removeClass("active");
        $(this).addClass("active");
    });

    if ($.isFunction($.fn.popup)) {
        $('.activating.element').popup();
    };

    if ($.isFunction($.fn.checkbox)) {
        $('.ui.checkbox').checkbox();
    };


    //initalizeDropdowns();
});

function initalizeDropdowns() {
    //We could not work with Semantic UI dropdown properly, too many breaking changes, 
    //too many UI bugs have been introduced.

    //$('.ui.dropdown').each(function () {
    //    var el = $(this);
    //    var placeholder = (el.attr("data-placeholder") || false);
    //    el.dropdown({ placeholder: placeholder, forceSelection: false });
    //});    
};