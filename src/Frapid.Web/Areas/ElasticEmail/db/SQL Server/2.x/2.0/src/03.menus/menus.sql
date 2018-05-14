DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'ElasticEmail'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'ElasticEmail'
);

DELETE FROM core.menus
WHERE app_name = 'ElasticEmail';


EXECUTE core.create_app 'ElasticEmail', 'ElasticEmail', 'ElasticEmail', '1.0', 'MixERP Inc.', 'December 1, 2015', 'teal mail', '/dashboard/elastic-email', NULL;

EXECUTE core.create_menu 'ElasticEmail', 'Tasks', 'Tasks', '', 'lightning', '';
EXECUTE core.create_menu 'ElasticEmail', 'ElasticEmailSetup', 'ElasticEmail Setup', '/dashboard/elastic-email', 'configure', 'Tasks';


GO
DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'ElasticEmail',
'{*}'
;
