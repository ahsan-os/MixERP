// Ajax File upload with jQuery and XHR2
// Sean Clark http://square-bracket.com
// xhr2 file upload
// data is optional
$.fn.upload = function (remote, data, successFn, progressFn, failFn) {
	function getFormData(el) {
		const formData = new FormData();
		const files = $(el)[0].files;
		for (let i = 0; i < files.length; i++) {
			formData.append($(el).attr("name"), $(el)[0].files[i]);
		};

		return formData;
	};

	// if we dont have post data, move it along
	if (typeof data != "object") {
		progressFn = successFn;
		successFn = data;
	};

	return this.each(function () {
		if ($(this)[0].files.length) {
			const formData = getFormData(this);

			// if we have post data too
			if (typeof data == "object") {
				for (let i in data) {
					if (data.hasOwnProperty(i)) {
						formData.append(i, data[i]);
					};
				};
			};

			// do the ajax request
			$.ajax({
				url: remote,
				type: 'POST',
				xhr: function () {
					const xhr = $.ajaxSettings.xhr();
					if (xhr.upload && progressFn) {
						// ReSharper disable once Html.EventNotResolved
						xhr.upload.addEventListener('progress', function (prog) {
							const value = ~~((prog.loaded / prog.total) * 100);

							// if we passed a progress function
							if (progressFn && typeof progressFn == "function") {
								progressFn(prog, value);

								// if we passed a progress element
							} else if (progressFn) {
								$(progressFn).val(value);
							};
						}, false);
					}
					return xhr;
				},
				data: formData,
				headers: window.getHeaders(),
				dataType: "json",
				cache: false,
				contentType: false,
				processData: false,
				error: function(e){
					if (failFn) failFn(e);
				},
				complete: function (res) {
					var json;
					try {
						json = JSON.parse(res.responseText);
						if (successFn) successFn(json);
					} catch (e) {
						if (failFn) failFn(e);
					};
				}
			});
		};
	});
};