function googleSignIn(id, email, officeId, name, token, culture) {
    $("#SignInSegment").addClass("loading");

    function request() {
        const url = "/account/google/sign-in";

        const loginDetails = {
            Email: email,
            OfficeId: officeId,
            Name: name,
            Token: token,
            Culture: culture
        };

        const data = JSON.stringify(loginDetails);

        return window.getAjaxRequest(url, "POST", data);
    };

    const ajax = request();

    ajax.success(function (response) {
        localStorage.setItem("access_token", response);

        if (response) {
            window.location = "/dashboard";
        } else {
            alert(window.translate("GoogleRegistrationClosed"));
        };
    });

    ajax.fail(function () {
        $("#SignInSegment").removeClass("loading");
    });
};

function onSignIn(googleUser) {
    const request = googleUser.getBasicProfile();
    const id = request.getId();
    const email = request.getEmail();
    const name = request.getName();
    const token = googleUser.getAuthResponse().id_token;

    const officeId = $("#SocialOfficeSelect").val();
    const culture = $("#SocialLanguageSelect").val() || "en-US";

    googleSignIn(id, email, officeId, name, token, culture);
};
(function () {
    const po = document.createElement('script');
    po.type = 'text/javascript'; po.async = true;
    po.src = 'https://apis.google.com/js/client:plusone.js?onload=render';
    const s = document.getElementsByTagName('script')[0];
    s.parentNode.insertBefore(po, s);
})();