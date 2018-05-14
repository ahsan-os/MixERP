DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Google'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Google'
);

DELETE FROM core.menus
WHERE app_name = 'Google';


SELECT * FROM core.create_app('Google', 'Google', 'Google', '1.0', 'MixERP Inc.', 'December 1, 2015', 'google violet', '/dashboard/google', NULL);

SELECT * FROM core.create_menu('Google', 'Tasks', 'Tasks', '', 'lightning', '');
SELECT * FROM core.create_menu('Google', 'GoogleIntegrationSetup', 'Google Integration Setup', '/dashboard/google', 'configure', 'Tasks');


SELECT * FROM auth.create_app_menu_policy
(
	'Admin', 
	core.get_office_id_by_office_name('Default'), 
	'Google',
	'{*}'
);
