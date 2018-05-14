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


SELECT * FROM core.create_app('MixERP.Social', 'Social', 'Social', '1.0', 'MixERP Inc.', 'December 1, 2015', 'orange users', '/dashboard/social', NULL);

SELECT * FROM core.create_menu('MixERP.Social', 'Tasks', 'Tasks', '', 'lightning', '');
SELECT * FROM core.create_menu( 'MixERP.Social', 'Social', 'Social', '/dashboard/social', 'users', 'Tasks');





SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'MixERP.Social',
    '{*}'::text[]
);
