function initializeSearchable() {
	function displaySearchableModal(element, targetControlId, title) {
		function getTemplate() {
			return `<div class="ui select search modal">
					<div class ="header">
						Find an Item
					</div>
					<div class ="content">
						<table class ="ui compact celled striped attached form table">
							<thead>
								<tr>
									<th><input type="search" placeholder="Search" /></th>
								</tr>
							</thead>
							<tbody>
							</tbody>
						</table>
					</div>
				</div>`;
		};

		$(".ui.select.search.modal").hide().remove();
		const template = $(getTemplate());

		const tableBody = template.find("tbody").html("");
		const selectedValue = element.val();

		$.each(element.find("option"), function () {
			const option = $(this);
			const value = option.attr("value");
			const text = option.text();

			if (!value) {
				return true;//continue
			};

			const row = $("<tr />").appendTo(tableBody);
			const cell = $("<td />").appendTo(row);
			cell.attr("data-selected", value);

			const input = $(`<input type="radio" name="dropdown selection radio" />`).appendTo(cell);

			const anchor = $("<a href='javascript:void(0);'/>").appendTo(cell);
			anchor.text(" #" + value + " / " + text);

			if (selectedValue === value) {
				input.prop("checked", true);
				row.addClass("positive");
			};
		});

		function makeSelection(el) {
			const cell = el.parent();
			const value = cell.attr("data-selected");

			$(".ui.select.search.modal").modal("hide");
			$(".ui.select.search.modal").remove();
			$("#" + targetControlId).val(value).trigger("change").focus();
		};

		tableBody.find("a").off("click").on("click", function () {
			const el = $(this);
			makeSelection(el);
		});

		tableBody.find("input").off("keyup").on("keyup", function (e) {
			if (e.keyCode === 13) {
				const el = $(this);
				makeSelection(el);
			};
		});

		template.find("input[type='search']").off("keyup").on("keyup", function () {
			$.extend($.expr[':'], {
				'ilike': function (elem, i, match, array) {
					return (elem.textContent || elem.innerText || '').toLowerCase()
					.indexOf((match[3] || "").toLowerCase()) >= 0;
				}
			});

			$(".ui.select.search.modal table tbody tr").hide();
			$(".ui.select.search.modal table tbody tr:ilike('" + this.value + "')").show();
		});

		if (title) {
			template.find(".header").text(title);
		};

		template.modal('setting', 'transition', 'scale').modal("show");
	};



	$("select").keyup(function (e) {
		function bindEvents(el){
			var id = el.attr("id");

			if (!id) {
				id = "Select" + (3000 + Math.floor(Math.random() * 600000));
				el.attr("id", id);
			};

			const label = el.parent().find("label").text();

			displaySearchableModal(el, id, label);
		};

		e = e || window.event;
		const keyCode = e.keyCode || e.which;
		const arrow = { left: 37, up: 38, right: 39, down: 40 };
		const el = $(this);

		if (e.ctrlKey) {
			if (keyCode === arrow.up || keyCode === arrow.right) {
				bindEvents(el);
			};
		};

		if(e.which === 68 && e.ctrlKey && e.shiftKey ){
			bindEvents(el);
	    };
	});
};
