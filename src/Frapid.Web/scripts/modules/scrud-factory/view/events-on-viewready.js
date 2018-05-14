function popUnder(div, button) {
    $(".popunder").hide();
    div.removeClass("initially hidden");
    div.fadeIn().position({
        my: "left top",
        at: "left bottom",
        of: button
    });
};

$(document).on("viewready", function () {
    window.localize();
    if (hasVerification()) {
        var verifyButton = $("#VerifyButton");

        verifyButton.show();

        verifyButton.click(function () {
            popUnder(verificationPopUnder, verifyButton);
        });
    };

});

function triggerViewReadyEvent() {
    if (!window.viewReady) {
        window.viewReady = true;
        $(document).trigger("viewready");
    };
};