function facebookSignIn(userId, email, officeId, token, culture) {
    function request() {
        const url = "/account/facebook/sign-in";
        const loginDetails = {
            FacebookUserId: userId,
            Email: email,
            OfficeId: officeId,
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
            alert(window.translate("FacebookRegistrationClosed"));
        };
    });

    ajax.fail(function () {
        $("#SignInSegment").removeClass("loading");
    });
};
$("#FacebookButton").click(function () {
    $("#SignInSegment").addClass("loading");
    var officeId = $("#SocialOfficeSelect").val();
    var culture = $("#SocialLanguageSelect").val() || "en-US";

    window.FB.login(function (response) {
        if (response.authResponse) {
            var fbUserId = response.authResponse.userID;
            var fbToken = response.authResponse.accessToken;

            window.FB.api('/me', { locale: 'en_US', fields: 'name, email' }, function (response) {
                facebookSignIn(fbUserId, response.email, officeId, fbToken, culture);
            }
            );
        } else {
            alert(window.translate("AuthorizeAppOnFacebook"));
            $("#SignInSegment").removeClass("loading");
        }
    },
    {
        scope: window.fbScope
    });
});

window.fbAsyncInit = function () {
    window.FB.init({
        appId: window.fbAppId, // App ID
        status: true, // check login status
        cookie: true, // enable cookies to allow the server to access the session
        xfbml: true // parse XFBML
    });

};

// Load the SDK Asynchronously
(function (d) {
    const id = 'facebook-jssdk';
    const ref = d.getElementsByTagName('script')[0];
    if (d.getElementById(id)) {
        return;
    }
    const js = d.createElement('script');
    js.id = id;
    js.async = true;
    js.src = "//connect.facebook.net/en_US/all.js";
    ref.parentNode.insertBefore(js, ref);
}(document));