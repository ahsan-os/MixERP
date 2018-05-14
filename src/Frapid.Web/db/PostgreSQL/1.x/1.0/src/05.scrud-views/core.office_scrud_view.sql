﻿DROP VIEW IF EXISTS core.office_scrud_view;

CREATE VIEW core.office_scrud_view
AS
SELECT
	core.offices.office_id,
	core.offices.office_code,
	core.offices.office_name,
	core.offices.currency_code,
	parent_office.office_code || ' (' || parent_office.office_name || ')' AS parent_office
FROM core.offices
LEFT JOIN core.offices AS parent_office
ON parent_office.office_id = core.offices.parent_office_id
WHERE NOT core.offices.deleted;