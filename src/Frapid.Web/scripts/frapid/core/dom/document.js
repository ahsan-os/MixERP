function getDocHeight(margin) {
    var d = document;
    var height = Math.max(
        Math.max(d.body.scrollHeight, d.documentElement.scrollHeight),
        Math.max(d.body.offsetHeight, d.documentElement.offsetHeight),
        Math.max(d.body.clientHeight, d.documentElement.clientHeight)
    );

    if (margin) {
        height += parseInt(margin);
    };

    return height;
};

var repaint = function () {
    setTimeout(function () {
        $(document).trigger('resize');
    }, 1000);
};