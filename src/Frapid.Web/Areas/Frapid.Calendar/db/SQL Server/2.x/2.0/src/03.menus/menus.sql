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


EXECUTE core.create_app 'Frapid.Calendar', 'Calendar', 'Calendar', '1.0', 'MixERP Inc.', 'December 1, 2015', 'violet calendar', '/dashboard/calendar', NULL;

EXECUTE core.create_menu 'Frapid.Calendar', 'Tasks', 'Tasks', '', 'lightning', '';
EXECUTE core.create_menu 'Frapid.Calendar', 'Calendar', 'Calendar', '/dashboard/calendar', 'calendar', 'Tasks';


GO
DECLARE @office_id integer  = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Calendar',
'{*}';

GO
