INSERT INTO account.roles(role_id, role_name, is_administrator)
SELECT 1000,   'Guest',                 0 UNION ALL
SELECT 2000,   'Website User',          0 UNION ALL
SELECT 3000,   'Partner',               0 UNION ALL
SELECT 4000,   'Content Editor',        0 UNION ALL
SELECT 5000,   'Backoffice User',       0 UNION ALL
SELECT 9999,   'Admin',                 1;


INSERT INTO account.configuration_profiles(profile_name, is_active, allow_registration, registration_role_id, registration_office_id)
SELECT 'Default', 1, 1, 2000, 1;