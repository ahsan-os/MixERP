var candidates = {
    primary: ["Code", "Number", "Name"],
    secondary: [
        "Customer", "Supplier", "CustomerName", "SupplierName", "Item", "ItemName",
        "Employee", "EmployeeName", "EmployeeCode", "Office", "OfficeName",
        "CurrencyCode",
        "Reason", "NoticeDate", "ApprovedOn"
    ]
};

function getQualified(dynamic, prefix, suffixes) {
    var qualified = "";

    $.each(suffixes, function (i, v) {
        if (!qualified) {
            var candidate = prefix + v;

            if (dynamic.hasOwnProperty(candidate)) {
                qualified = candidate;
                return false;
            };
        };
    });

    return qualified;
};

function getExtraContent(id) {
    var extraContent = $('<div class="extra center content" />');

    var buttons = $('<div class="ui small basic buttons" />');

    if (window.scrudFactory.viewUrl) {
        var url = window.scrudFactory.viewUrl.replace("{Id}", id);
        var viewButton = $('<a class="ui basic button" />');
        viewButton.attr("href", url);
        viewButton.text(window.translate("View"));
        buttons.append(viewButton);
    };

    if (window.scrudFactory.allowEdit) {
        var editButton = $('<a class="ui basic button" href="javascript:void(0);" onclick="editRow(this, true);" />');
        editButton.text(window.translate("Edit"));
        buttons.append(editButton);
    }

    if (window.scrudFactory.allowDelete) {
        var deleteButton = $('<a class="ui basic button" href="javascript:void(0);" onclick="deleteRow(this, true);" />');
        deleteButton.text(window.translate("Delete"));
        buttons.append(deleteButton);
    };


    extraContent.append(buttons);

    return extraContent;
};

function getExtra(kanbanDetail) {
    var extra = $('<div class="extra" />');

    var text = $("<span />");
    text.text(window.translate("Rating"));
    extra.append(text);

    var dataRating = (kanbanDetail.Rating || 0);

    var rating = $('<div class="ui star rating" data-max-rating="5" />');
    rating.attr("data-rating", dataRating);

    extra.append(rating);


    return extra;
};

function getHeaderField(dynamic) {
    var className = getClassName();

    var primary = candidates.primary;
    var qualified = getQualified(dynamic, className, primary);

    if (!qualified) {
        var secondary = candidates.secondary;

        qualified = getQualified(dynamic, "", secondary);
    };

    return qualified;
};

function getMetaField(dynamic, headerField) {
    var className = getClassName();

    var primary = Enumerable.From(candidates.primary)
        .Where(function (x) { return x !== headerField.replace(className, "") })
        .ToArray();

    var qualified = getQualified(dynamic, className, primary);

    if (!qualified) {
        var secondary = Enumerable.From(candidates.secondary)
            .Where(function (x) { return x !== headerField })
            .ToArray();

        qualified = getQualified(dynamic, "", secondary);
    };

    return qualified;
};

function getIdField() {
    var table = window.scrudFactory.formTableName;

    if (!table) {
        table = window.scrudFactory.viewTableName;
    }

    var plural = table.split(".")[1];
    return plural.singularize() + "_id";
};

function getCardKey(dynamic) {
    if (!dynamic) {
        return "";
    };

    var keyField = toProperCase(getIdField());

    if (window.scrudFactory.card && window.scrudFactory.card.keyField) {
        keyField = window.scrudFactory.card.keyField;
    };

    return dynamic[keyField];
};

function getImageField(entity) {
    if (!entity) {
        return "";
    };

    var imageField;
    //alert(JSON.stringify(entity));

    $.each(entity, function (i) {
        //debugger;
        if (!imageField) {
            if ((i || "").toString().toLowerCase().indexOf("photo") !== -1) {
                imageField = i;
            };
        };

        if (!imageField) {
            if ((i || "").toString().toLowerCase().indexOf("logo") !== -1) {
                imageField = i;
            };
        };

        if (!imageField) {
            if ((i || "").toString().toLowerCase().indexOf("photo") !== -1) {
                imageField = i;
            };
        };
    });

    return imageField;
};

function getDescriptionField(entity) {
    if (!entity) {
        return "";
    };

    var candidates = ["Email", "Url", "City", "Phone", "CompanyName", "Currency"];
    return getQualified(entity, "", candidates);
};

function getCardField(card, field) {
    if (!card || !field) {
        return "";
    };

    var isExpression = field.substring(2, 0) === "{{" && field.slice(-2) === "}}";

    if (isExpression) {
        var expression = field.replace("{{", "").replace("}}", "");
        return eval(expression);
    };

    return card[field];
};

function createCard(dynamic, key, kanbanDetail) {
    var kanbanId = (kanbanDetail.KanbanId || 0);

    var text;
    var imageField = (window.scrudFactory.card.image || getImageField(dynamic));

    var headerField = (window.scrudFactory.card.header || getHeaderField(dynamic));
    var metaField = (window.scrudFactory.card.meta || getMetaField(dynamic, headerField));
    var descriptionField = (window.scrudFactory.card.description || getDescriptionField(dynamic));

    var card = $('<div class="kanban-card" />');
    card.attr("data-kanban-detail-id", kanbanDetail.KanbanDetailId);
    card.attr("data-key", key);


    if (imageField) {
        var src = getCardField(dynamic, imageField);
        if (src) {
            var image = $('<div class="image" />');
            var img = $("<img />");

            img.attr("src", src + "?Width=300&Height=300");

            image.append(img);
            card.append(image);
        };
    };

    var content = $('<div class="content" />');

    var checkbox = $('<div class="ui toggle checkbox" />');
    var input = $('<input name="public" type="checkbox">');

    checkbox.append(input);

    content.append(checkbox);


    if (headerField) {
        text = getCardField(dynamic, headerField);
        if (text) {
            var header = $('<div class="header" />');

            header.text(text);
            content.append(header);
        };
    };

    if (metaField) {
        text = getCardField(dynamic, metaField);
        if (text) {
            var meta = $('<div class="meta" />');

            meta.text(text);
            content.append(meta);
        };
    };

    if (descriptionField) {
        text = getCardField(dynamic, descriptionField);
        if (text) {
            var description = $('<div class="meta" />');

            description.html(text);
            content.append(description);
        };
    };

    card.append(content);
    card.append(getExtraContent(key));
    card.append(getExtra(kanbanDetail));

    $("#kanban" + kanbanId + " .kanban.holder").append(card);
    $(".checkbox").checkbox();
};