DROP VIEW IF EXISTS account.configuration_profile_scrud_view;

CREATE VIEW account.configuration_profile_scrud_view
AS
SELECT
	account.configuration_profiles.configuration_profile_id,
	account.configuration_profiles.profile_name,
	account.configuration_profiles.is_active,
	account.configuration_profiles.allow_registration,
	account.roles.role_name AS defult_role,
	core.offices.office_code || ' (' || core.offices.office_name || ')' AS default_office
FROM account.configuration_profiles
LEFT JOIN account.roles
ON account.roles.role_id = account.configuration_profiles.registration_role_id
LEFT JOIN core.offices
ON core.offices.office_id = account.configuration_profiles.registration_office_id
WHERE NOT account.configuration_profiles.deleted;
