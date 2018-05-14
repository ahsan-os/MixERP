function popUnder(div, button) {
    div.removeClass("initially hidden");
    div.fadeIn().position({
        my: "left top",
        at: "left bottom",
        of: button
    });
};