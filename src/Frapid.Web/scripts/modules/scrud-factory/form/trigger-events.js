function triggerFormReadyEvent() {
    if (!window.formReady) {
        window.localize();
        window.formReady = true;
        $(document).trigger("formready");
        showForm();
    };
};

function triggerOnSavingEvent() {
    $(document).trigger("onsaving");
};
