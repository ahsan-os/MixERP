function createVerificationButton() {
    if (!hasVerification()) {
        return;
    };

    if (!$("#VerifyButton").length) {
        var anchor = $("<a href='javascript:void(0);' id='VerifyButton' class='ui basic button' />");
        anchor.html(window.translate("Verify"));

        anchor.insertAfter(addNewButton);
    };
};
