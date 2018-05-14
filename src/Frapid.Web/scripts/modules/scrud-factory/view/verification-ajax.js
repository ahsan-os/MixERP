function verify(primaryKey, verificationStatusId, reason) {
    var url = window.scrudFactory.verificationApi;

    var model = {
        PrimaryKeyValue: primaryKey,
        Reason: reason,
        VerificationStatusId: verificationStatusId
    };

    var data = JSON.stringify(model);

    return getAjaxRequest(url, "PUT", data);
};

function getSelected() {
    var el = $("#ScrudView input:checkbox:checked").first();
    var isCard = false;

    if (isCardView()) {
        isCard = true;
        el = $("#kanban input:checkbox:checked").first();
    };

    return getPrimaryKeyValue(el, isCard);
};

$("#ApproveButton").click(function () {
    var primaryKey = getSelected();

    if (!hasVerification() || !primaryKey) {
        return;
    };

    if(!scrudFactory.verificationApi){
        return;
    };

    var reason = reasonTextArea.val();
    var verificationStatusId = 2; //2 -> Approved, -3 -> Rejected

    var verifyAjax = verify(primaryKey, verificationStatusId, reason);

    verifyAjax.success(function () {
        prepareScrudView();
        displayMessage(window.translate("TaskCompletedSuccessfully"), "success");
    });
});

$("#RejectButton").click(function () {
    var primaryKey = getSelected();

    if (!hasVerification() || !primaryKey) {
        return;
    };


    if(!scrudFactory.verificationApi){
        return;
    };

    var reason = reasonTextArea.val();
    var verificationStatusId = -3; //2 -> Approved, -3 -> Rejected

    var verifyAjax = verify(primaryKey, verificationStatusId, reason);

    verifyAjax.success(function () {
        prepareScrudView();
        displayMessage(window.translate("TaskCompletedSuccessfully"), "success");
    });
});
