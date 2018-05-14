INSERT INTO account.roles
SELECT 1000,   'Guest',                 false UNION ALL
SELECT 2000,   'Website User',          false UNION ALL
SELECT 3000,   'Partner',               false UNION ALL
SELECT 4000,   'Content Editor',        false UNION ALL
SELECT 5000,   'Backoffice User',       false UNION ALL
SELECT 9999,   'Admin',                 true;


INSERT INTO account.configuration_profiles(profile_name, is_active, allow_registration, registration_role_id, registration_office_id)
SELECT 'Default', true, true, 2000, 1;