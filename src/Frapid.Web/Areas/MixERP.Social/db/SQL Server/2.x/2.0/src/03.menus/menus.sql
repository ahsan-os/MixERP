DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
    SELECT menu_id FROM core.menus
    WHERE app_name = 'MixERP.Social'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
    SELECT menu_id FROM core.menus
    WHERE app_name = 'MixERP.Social'
);

DELETE FROM core.menus
WHERE app_name = 'MixERP.Social';


EXECUTE core.create_app 'MixERP.Social', 'Social', 'Social', '1.0', 'MixERP Inc.', 'December 1, 2015', 'orange users', '/dashboard/social', NULL;

EXECUTE core.create_menu 'MixERP.Social', 'Tasks', 'Tasks', '', 'lightning', '';
EXECUTE core.create_menu 'MixERP.Social', 'Social', 'Social', '/dashboard/social', 'users', 'Tasks';

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'MixERP.Social',
'{*}';


GO
