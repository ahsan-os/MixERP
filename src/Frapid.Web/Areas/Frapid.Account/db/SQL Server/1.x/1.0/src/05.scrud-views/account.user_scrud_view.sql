IF OBJECT_ID('account.user_scrud_view') IS NOT NULL
DROP VIEW account.user_scrud_view;

GO

CREATE VIEW account.user_scrud_view
AS
SELECT
	account.users.user_id,
	account.users.email,
	account.users.name,
	account.users.phone,
	core.offices.office_code + ' (' + core.offices.office_name + ')' AS office,
	account.roles.role_name
FROM account.users
INNER JOIN account.roles
ON account.roles.role_id = account.users.role_id
INNER JOIN core.offices
ON core.offices.office_id = account.users.office_id
WHERE account.users.deleted = 0;

GO