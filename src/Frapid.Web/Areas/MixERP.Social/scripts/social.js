var imageTemplate = `<div class="image item" data-is-local="true" data-file-name="{FileName}">
		<i onclick="deleteItem(this)" class="delete icon"></i>
		<img onclick="preview(this);" src="{ImageSource}" />
		<div class="file name">{FileName}</div>
	</div>`;

var filePlaceholderTemplate = `<div data-is-local="true" class="{Extension} placeholder item" data-file-name="{FileName}">
		<i onclick="deleteItem(this)" class ="delete icon"></i>
		<div class="text">{Extension}</div>
		<div class="file name">{FileName}</div>
	</div>`;

var template = `<div class ="original story" data-feed-id="{FeedId}" data-event-time="{EventTimestamp}"
            data-parent-feed-id="{ParentFeedId}" data-scope="{Scope}" data-child-count="{ChildCount}">
            <div class ="avatar">
                <div class ="photo">
                    <img src="/dashboard/social/avatar/{CreatedBy}/{CreatedByName}/100/100" />
                </div>
                <div class ="info">
                    <a class ="name">{CreatedByName}</a>
                    <div class ="refreshing moment" data-time="{EventTimestamp}" title="{EventTimestamp}">{EventTimestamp}</div>
                </div>
            </div>
            <div class ="text">
                {FormattedText}
                <a data-feed-id="{FeedId}" onclick='deleteFeed(this);' class="remove feed">
                    <i class ="delete icon"></i>
                </a>
                <div class ="ui gallery">
                </div>
            </div>
            <div class ="actions">
                <a onclick="like(this);" data-feed-id="{FeedId}" class ="like"><i class ='thumbs up icon'></i><span class="toggle like" data-localize="Like"></span></a>
                <a onclick="comment(this)" class ="comment"><i class ='comment icon'></i><span  data-localize="Comment"></span></a>
            </div>
            <a class ="story liked by" title="{LikedBy}">
                <i class='thumbs up icon'></i><span>{TotalLikes}<span>
            </a>
            <div class ="stories">
            </div>
            <div class ="post">
                <textarea placeholder="Leave a comment" rows="2"></textarea>
            </div>
            <div class ="buttons">
                <div class ="left">
                </div>
                <div class ="right">
                    <button data-feed-id="{FeedId}" onclick="postComment(this)" class ="ui basic blue button" data-localize="Post"></button>
                </div>
            </div>
        </div>`;

var replyTemplate = `
        <div class ="reply" data-feed-id="{FeedId}" data-event-time="{EventTimestamp}"
            data-parent-feed-id="{ParentFeedId}" data-scope="{Scope}">
            <div class ="avatar">
                <div class ="photo">
                    <img src="/dashboard/social/avatar/{CreatedBy}/{CreatedByName}/100/100" />
                </div>
            </div>
            <div class ="story">
                <div class ="info">
                    <a class ="name">{CreatedByName}:</a>
                    <span>{FormattedText}</span>
                    <div class="meta">
                        <a class ="like" onclick="like(this);" data-feed-id="{FeedId}"><span class ="toggle like" data-localize="Like"></span></a>
                        <a class ="reply" onclick="comment(this)"><span data-localize="Reply"></span></a>
                        <a class ="comment liked by" title="{LikedBy}"><i class ='thumbs up icon'></i><span>{TotalLikes}</span></a>
                        <a class ="refreshing moment" data-time="{EventTimestamp}" title="{EventTimestamp}">{EventTimestamp}</a>
                    </div>
                </div>
                <a data-feed-id="{FeedId}" onclick='deleteFeed(this);' class ="remove feed">
                    <i class ="delete icon"></i>
                </a>
            </div>
        </div>`;

var showPrevisouAnchorTemplate = `<a class="show previous comments" onclick="showPreivousComments(this);" data-localize="ShowPreviousComments"></a>`;

function displayImage(target, file, fileName) {
    const reader = new FileReader();

    reader.onload = function (e) {
        const base64 = e.target.result;
        var el = imageTemplate;

        el = el.replace(/{ImageSource}/g, base64);
        el = el.replace(/{FileName}/g, fileName);
        el = $(el);

        el.find("i.delete").remove();

        target.append(el);
    };

    reader.readAsDataURL(file);
};

function displayFile(target, file, fileName, extension) {
    const reader = new FileReader();

    reader.onload = function (e) {
        const base64 = e.target.result;
        var el = filePlaceholderTemplate;

        el = el.replace(/{Base64}/g, base64);
        el = el.replace(/{Extension}/g, extension);
        el = el.replace(/{FileName}/g, fileName);

        el = $(el);
        el.find("i.delete").remove();

        target.append(el);
    };

    reader.readAsDataURL(file);
};

function isEmbeddedContent() {
    return $(".social.network.container").is(".embedded");
};

function getUrl() {
    return window.location.href.split('?')[0];
};


function uploadAttachments(el) {
    const handler = "/dashboard/social/attachment";

    el.upload(handler, null, function (response) {
        el.attr("data-uploaded-files", JSON.stringify(response));
        $("#PostButton").removeClass("loading").prop("disabled", false);
    }, function (progress, value) {
    }, function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
};


function isImage(fileName) {
    const images = ["jpg", "jpeg", "png", "gif"];
    const extension = fileName.split('.').pop().toLowerCase();

    if (images.indexOf(extension) >= 0) {
        return true;
    };

    return false;
};

$("#UploadInputFile").off("change").on("change", function () {
    const el = $(this);
    $("#PostButton").addClass("loading").prop("disabled", true);
    const target = $(".add.a.new.post .ui.gallery").html("");

    const files = this.files;

    for (var i = 0; i < files.length; i++) {
        const file = files[i];
        const fileName = file.name.prop("disabled", true)
        const extension = fileName.split('.').pop().toLowerCase();

        if (isImage(fileName)) {
            displayImage(target, file, fileName);
        } else {
            displayFile(target, file, fileName, extension);
        };
    };

    uploadAttachments(el);
    window.localize();
});


$("#UploadAvatarInputFile").on("change", function () {
    const el = $(this);
    const handler = "/dashboard/social/avatar";

    el.upload(handler, null, function (response) {
        window.displaySuccess();
        const myImages = "img[src*='dashboard/social/avatar/" + window.userId + "']";
        $.each($(myImages), function () {
            $(this).attr('src', $(this).attr("src") + "?" + Math.random());
        });

    }, function (progress, value) {
    }, function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
});


function displayAttachment(el, attachment) {
    var template;
    const extension = attachment.split('.').pop().toLowerCase();

    if (isImage(attachment)) {
        template = imageTemplate;
        template = template.replace(/{ImageSource}/g, "/dashboard/social/attachment/" + attachment);
        template = template.replace(/{FileName}/g, attachment);

        template = $(template);
        template.removeAttr("data-is-local");
    } else {
        template = filePlaceholderTemplate;
        template = template.replace(/{Base64}/g, "");
        template = template.replace(/{Extension}/g, extension);
        template = template.replace(/{FileName}/g, attachment);

        template = $(template);
        template.removeAttr("data-is-local");

        template.find(".text").on("click", function () {
            const newTab = window.open("/dashboard/social/attachment/" + attachment, '_blank');

            if (newTab) {
                newTab.focus();
            } else {
                window.displayMessage('Please allow popups to view this file.');
            };
        });
    };


    el.append(template);
};

function displayAttachments(el, attachments) {
    attachments = attachments.split(",");

    for (let i = 0; i < attachments.length; i++) {
        const attachment = attachments[i];
        displayAttachment(el, attachment);
    };
};

function getCard(model) {
    var el = template;

    var likedByMe = false;
    var likedBy = "";
    var totalLikes = 0;

    if (model.Likes && model.Likes.length) {
        likedByMe = window.Enumerable.From(model.Likes).Where(function (x) {
            return x.LikedBy === window.userId;
        }).ToArray().length === 1;

        likedBy = window.Enumerable.From(model.Likes)
            .OrderByDescending(function (x) {
                return new Date(x.LikedOn);
            })
            .Select(function (x) {
                return x.LikedBy === window.userId ? "Me" : x.LikedByName;
            }).ToArray().join(", ");

        totalLikes = model.Likes.length;
    };

    if (model.ParentFeedId) {
        el = replyTemplate;
    };


    el = el.replace(/{FeedId}/g, model.FeedId);
    el = el.replace(/{EventTimestamp}/g, model.EventTimestamp);
    el = el.replace(/{CreatedBy}/g, model.CreatedBy);
    el = el.replace(/{CreatedByName}/g, model.CreatedByName);
    el = el.replace(/{ParentFeedId}/g, model.ParentFeedId);
    el = el.replace(/{Scope}/g, model.Scope);
    el = el.replace(/{FormattedText}/g, model.FormattedText);
    el = el.replace(/{ChildCount}/g, model.ChildCount);
    el = el.replace(/{TotalLikes}/g, totalLikes);
    el = el.replace(/{LikedBy}/g, likedBy);

    //alert(JSON.stringify(model.Likes));

    el = $(el);


    if (model.Attachments) {
        displayAttachments(el.find(".ui.gallery"), model.Attachments);
    };


    if (!likedByMe) {
        return el;
    };

    if (model.ParentFeedId) {
        el.find("a span.toggle.like").html(window.translate("Unlike"));
        el.find(".meta a.like").addClass("liked");
        el.find(".meta a.like").attr("onclick", "unlike(this);");
    } else {
        el.find("a.like span").html(window.translate("Unlike"));
        el.find("a.like").addClass("liked");
        el.find("a.like").attr("onclick", "unlike(this);");
    };

    return el;
};

function createCard(model, prepend) {
    const el = getCard(model);
    var target;

    if (model.ParentFeedId) {
        target = $(".story[data-feed-id='" + model.ParentFeedId + "'] .stories");
    } else {
        target = $(".all.stories");
    }

    if (prepend) {
        target.prepend(el);
    } else {
        target.append(el);
    }

    if (model.ChildCount > 10) {
        el.find(".post").before(showPrevisouAnchorTemplate);
    };

    window.localize();

    return el;
};

function createUI(lastFeedId, parentFeedId, model) {
    $.each(model, function () {
        createCard(this);
    });
};

function displayStories(lastFeedId, parentFeedId) {
    function request(model) {
        const url = "/dashboard/social/feeds";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "POST", data);
    };

    lastFeedId = lastFeedId || 0;
    parentFeedId = parentFeedId || 0;

    const model = {
        LastFeedId: lastFeedId,
        ParentFeedId: parentFeedId,
        Url: isEmbeddedContent() ? getUrl() : ""
    };

    const ajax = request(model);

    ajax.success(function (response) {
        const model = window.Enumerable.From(response).OrderBy(function (x) {
            return x.ParentFeedId || 0;
        }).ThenBy(function (x) {
            return x.RowNumber;
        }).ToArray();

        createUI(lastFeedId, parentFeedId, model);
        window.setMoments();

        $(document).trigger("storiesdisplayed", [model]);
    });
};

displayStories();

$("#PostButton").off("click").on("click", function () {
    function request(model) {
        const url = "/dashboard/social/new";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel() {
        function getAttachments() {
            const attachments = [];
            const uploadedFiles = $("#UploadInputFile").attr("data-uploaded-files");
            var files = null;

            if (uploadedFiles) {
                files = JSON.parse(uploadedFiles);
            };


            $.each(files, function () {
                attachments.push(this.FileName);
            });

            return attachments.join(",");
        };

        function getScope() {
            return ""; //Todo
        };

        const text = $("#WhatsOnYourMindTextArea").val().trim();

        return {
            FormattedText: text,
            Attachments: getAttachments(),
            Scope: getScope(),
            IsPublic: true,
            Url: isEmbeddedContent() ? getUrl() : ""
        };
    };


    const model = getModel();

    if (!model.FormattedText) {
        return;
    };


    const ajax = request(model);
    var el = $(this);
    el.addClass("loading");

    ajax.success(function (response) {
        $("#WhatsOnYourMindTextArea").val("");
        $("#UploadInputFile").attr("data-uploaded-files", "");
        $(".add.a.new.post .ui.gallery").html("");
        window.displayMessage(window.translate("Awesome"), "success");
        el.removeClass("loading");
        createCard(response, true);
        window.setMoments();
    });

    ajax.fail(function () {
        window.displayMessage(window.translate("SomethingWentWrong"));
        el.removeClass("loading");
    });
});

function postComment(element) {
    function request(model) {
        const url = "/dashboard/social/new";
        const data = JSON.stringify(model);

        return window.getAjaxRequest(url, "POST", data);
    };

    function getModel(el) {
        function getAttachments() {
            return ""; //Todo
        };

        function getScope() {
            return ""; //Todo
        };

        function getParentFeedId(el) {
            return el.attr("data-feed-id") || null;
        };

        const text = el.parent().parent().parent().find(".post textarea").val().trim();

        return {
            FormattedText: text,
            Attachments: getAttachments(),
            Scope: getScope(),
            IsPublic: true,
            ParentFeedId: getParentFeedId(el)
        };
    };

    const el = $(element);
    el.addClass("loading");
    const model = getModel(el);

    if (!model.FormattedText) {
        return;
    };

    const ajax = request(model);

    ajax.success(function (response) {
        el.parent().parent().parent().find(".post textarea").val("");
        el.removeClass("loading");
        const card = createCard(response, true);
        const allStories = $(".all.stories");
        const originalStory = card.closest(".original.story");

        originalStory.prependTo(allStories);
        window.setMoments();
        window.scrollTo(0, 0);

        card.addClass("selected");
        setTimeout(function () {
            card.toggleClass("selected");
        }, 5000);
    });

    ajax.fail(function () {
        window.displayMessage(window.translate("SomethingWentWrong"));
        el.removeClass("loading");
    });
};

function deleteFeed(element) {
    function request(feedId) {
        var url = "/dashboard/social/delete/{feedId}";
        url = url.replace("{feedId}", feedId);

        return window.getAjaxRequest(url, "DELETE");
    };

    const el = $(element);
    const feedId = window.parseInt(el.attr("data-feed-id"));

    if (!feedId) {
        return;
    }

    const confirmed = window.confirmAction();
    if (!confirmed) {
        return;
    };


    const ajax = request(feedId);

    ajax.success(function () {
        var target = el.closest(".reply");

        if (!target.length) {
            target = el.closest(".story");
        }

        target.remove();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
};

function comment(element) {
    const el = $(element);
    const story = el.closest(".original.story");
    const textArea = story.find("textarea");
    textArea.focus();
};

function showPreivousComments(element) {
    const el = $(element);
    const story = el.closest(".story");
    const parentFeedId = story.attr("data-feed-id");
    const childCount = story.attr("data-child-count");

    const feedIds = story.find(".reply").map(function () {
        return $(this).attr("data-feed-id");
    }).get();

    const lastFeedId = Math.min.apply(null, feedIds);

    displayStories(lastFeedId, parentFeedId);

    $(document).off("storiesdisplayed").on("storiesdisplayed", function () {
        const totalItems = story.find(".reply").length;

        if (totalItems >= childCount) {
            el.remove();
        };
    });
};

function loadOlderStories(el) {
    const feedIds = $(".all.stories>.story").map(function () {
        return $(this).attr("data-feed-id");
    }).get();

    const lastFeedId = Math.min.apply(null, feedIds);

    displayStories(lastFeedId);

    $(document).off("storiesdisplayed").on("storiesdisplayed", function (e, model) {
        if (!model.length) {
            $(el).remove();
            window.displayMessage(window.translate("NoMoreStoriesToDisplay"), "info");
        };
    });
};


function deleteItem(el) {
    function request(feedId, attachment) {
        var url = "/dashboard/social/delete/{feedId}/attachment/{attachment}";
        url = url.replace("{feedId}", feedId);
        url = url.replace("{attachment}", attachment);

        return window.getAjaxRequest(url, "DELETE");
    };

    const confirmed = window.confirmAction();

    if (!confirmed) {
        return;
    };

    el = $(el);
    const item = el.closest(".item");
    const feedId = item.closest(".story").attr("data-feed-id");

    const fileName = item.attr("data-file-name");

    const ajax = request(feedId, fileName);

    ajax.success(function () {
        item.remove();
        window.displaySuccess();
    });

    ajax.fail(function (xhr) {
        window.logAjaxErrorMessage(xhr);
    });
};

function preview(el) {
    const image = $(el);
    const target = $("#LightBoxModal img");
    target.attr("src", image.attr("src"));
    const height = image.get(0).naturalHeight;
    const width = image.get(0).naturalWidth;
    const windowWidth = $(window).width();
    var left = (windowWidth - width) / 2;
    const offset = 400;

    if (left < 0) {
        left = 0;
    };

    left += offset;

    $("#LightBoxModal").css("height", height + "px");
    $("#LightBoxModal").css("width", width + "px");
    $("#LightBoxModal").css("left", left + "px");

    $("#LightBoxModal").modal("show");
};

function unlike(el) {
    function request(feedId) {
        var url = "/dashboard/social/unlike/{feedId}";
        url = url.replace("{feedId}", feedId);

        return window.getAjaxRequest(url, "PUT");
    };

    el = $(el);
    const feedId = el.attr("data-feed-id");
    const counterEl = el.closest(".story").find(".liked.by");
    const likedBy = (counterEl.attr("title") || "").split(",");

    const index = likedBy.indexOf(window.translate("Me"));

    if (index > -1) {
        likedBy.splice(index, 1);
        counterEl.attr("title", likedBy.join(", "));
    };

    const totalLikes = window.parseInt(counterEl.find("span").html());
    if (!feedId) {
        return;
    };

    const ajax = request(feedId);

    ajax.success(function () {
        el.attr("onclick", "like(this);");
        el.removeClass("liked");
        el.find("span").html(window.translate("Like"));
        counterEl.find("span").html(totalLikes - 1);
    });

    ajax.fail(function () {
        window.displayMessage(window.translate("ServerError"));
    });
};

function like(el) {
    function request(feedId) {
        var url = "/dashboard/social/like/{feedId}";
        url = url.replace("{feedId}", feedId);

        return window.getAjaxRequest(url, "PUT");
    };

    el = $(el);
    const feedId = el.attr("data-feed-id");
    const counterEl = el.closest(".story").find(".liked.by");
    var likedBy = counterEl.attr("title");

    if (!likedBy) {
        likedBy = "Me";
    } else {
        likedBy += "Me, ";
    };

    counterEl.attr("title", likedBy);

    const totalLikes = window.parseInt(counterEl.find("span").html());
    if (!feedId) {
        return;
    };

    const ajax = request(feedId);

    ajax.success(function () {
        el.attr("onclick", "unlike(this);");
        el.addClass("liked");
        el.find("span").html(window.translate("Unlike"));
        counterEl.find("span").html(totalLikes + 1);
    });

    ajax.fail(function () {
        window.displayMessage(window.translate("ServerError"));
    });
};
