DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Frapid.Calendar'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Frapid.Calendar'
);

DELETE FROM core.menus
WHERE app_name = 'Frapid.Calendar';


SELECT * FROM core.create_app('Frapid.Calendar', 'Calendar', 'Calendar', '1.0', 'MixERP Inc.', 'December 1, 2015', 'violet calendar', '/dashboard/calendar', NULL);

SELECT * FROM core.create_menu('Frapid.Calendar', 'Tasks', 'Tasks', '', 'lightning', '');
SELECT * FROM core.create_menu('Frapid.Calendar', 'Calendar', 'Calendar', '/dashboard/calendar', 'calendar', 'Tasks');


SELECT * FROM auth.create_app_menu_policy
(
	'Admin', 
	core.get_office_id_by_office_name('Default'), 
	'Frapid.Calendar',
	'{*}'
);
