function getKeyColumnPosition() {
    if (typeof (window.scrudFactory.keyColumnPosition) === "undefined") {
        window.scrudFactory.keyColumnPosition = "3";
    };

    return window.scrudFactory.keyColumnPosition;
};
