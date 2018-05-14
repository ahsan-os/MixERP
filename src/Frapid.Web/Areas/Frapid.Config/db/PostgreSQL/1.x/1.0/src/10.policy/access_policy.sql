SELECT * FROM auth.create_api_access_policy('{*}', 1, 'config.kanban_details', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{*}', 1, 'config.kanbans', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{*}', 1, 'config.filter_name_view', '{*}', true);

SELECT * FROM auth.create_api_access_policy('{User}', core.get_office_id_by_office_name('Default'), 'core.offices', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{Admin}', core.get_office_id_by_office_name('Default'), '', '{*}', true);
