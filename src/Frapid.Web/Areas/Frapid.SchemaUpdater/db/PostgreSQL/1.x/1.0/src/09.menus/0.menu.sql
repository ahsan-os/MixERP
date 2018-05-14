SELECT * FROM core.create_app('Frapid.SchemaUpdater', 'Updates', 'Updates', '1.0', 'MixERP Inc.', 'December 1, 2015', 'teal refresh', '/dashboard/updater/home', null);
SELECT * FROM core.create_menu('Frapid.SchemaUpdater', 'Home', 'Home', '/dashboard/updater/home', 'refresh outline', '');

SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.SchemaUpdater',
    '{*}'::text[]
);