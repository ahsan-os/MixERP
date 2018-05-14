SELECT * FROM core.create_app('Frapid.Config', 'Config', 'Config', '1.0', 'MixERP Inc.', 'December 1, 2015', 'orange configure', '/dashboard/config/offices', null);
SELECT * FROM core.create_menu('Frapid.Config', 'Offices', 'Offices', '/dashboard/config/offices', 'building outline', '');
SELECT * FROM core.create_menu('Frapid.Config', 'SMTP', 'SMTP', '/dashboard/config/smtp', 'at', '');
SELECT * FROM core.create_menu('Frapid.Config', 'FileManager', 'File Manager', '/dashboard/config/file-manager', 'file text outline', '');

SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.Config',
    '{*}'::text[]
);