var autocomplete;

function initMap() {
    var map = new window.google.maps.Map(document.getElementById('map'));
    const input = document.getElementById('LocationInputText');

    autocomplete = new window.google.maps.places.Autocomplete(input);
    autocomplete.setTypes([]);
    autocomplete.setOptions({ strictBounds: false });

    // Bind the map's bounds (viewport) property to the autocomplete object,
    // so that the autocomplete requests use the current map bounds for the
    // bounds option in the request.
    autocomplete.bindTo('bounds', map);

    var infowindow = new window.google.maps.InfoWindow();
    var infowindowContent = document.getElementById('infowindow-content');
    infowindow.setContent(infowindowContent);
    var marker = new window.google.maps.Marker({
        map: map,
        anchorPoint: new window.google.maps.Point(0, -29)
    });

    function onChange() {
        infowindow.close();
        marker.setVisible(false);
        const place = autocomplete.getPlace();
        if (!(place && place.geometry)) {
            // User entered the name of a Place that was not suggested and
            // pressed the Enter key, or the Place Details request failed.
            return;
        }

        // If the place has a geometry, then present it on a map.
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17); // Why 17? Because it looks good.
        }
        marker.setPosition(place.geometry.location);
        marker.setVisible(true);

        var address = '';
        if (place.address_components) {
            address = [
                (place.address_components[0] && place.address_components[0].short_name || ''),
                (place.address_components[1] && place.address_components[1].short_name || ''),
                (place.address_components[2] && place.address_components[2].short_name || '')
            ].join(' ');
        }

        infowindowContent.children['place-icon'].src = place.icon;
        infowindowContent.children['place-name'].textContent = place.name;
        infowindowContent.children['place-address'].textContent = address;
        infowindow.open(map, marker);
    };

    autocomplete.addListener('place_changed', function () {
        onChange();
    });
};


$("#LocationInputText").on("change", function () {
    const val = $("#LocationInputText").val();
    if ((val || "").trim()) {
        const input = document.getElementById('LocationInputText');
        window.google.maps.event.trigger(input, 'focus');
        window.google.maps.event.trigger(input, 'keydown', { keyCode: 40 });
        window.google.maps.event.trigger(input, 'keydown', { keyCode: 13 });
    };
});
