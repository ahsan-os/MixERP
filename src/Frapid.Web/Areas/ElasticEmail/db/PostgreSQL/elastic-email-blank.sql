-->-->-- src/Frapid.Web/Areas/ElasticEmail/db/PostgreSQL/2.x/2.0/src/03.menus/menus.sql --<--<--
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


SELECT * FROM core.create_app('ElasticEmail', 'ElasticEmail', 'ElasticEmail', '1.0', 'MixERP Inc.', 'December 1, 2015', 'teal mail', '/dashboard/elastic-email', NULL);

SELECT * FROM core.create_menu('ElasticEmail', 'Tasks', 'Tasks', '', 'lightning', '');
SELECT * FROM core.create_menu('ElasticEmail', 'ElasticEmailSetup', 'ElasticEmail Setup', '/dashboard/elastic-email', 'configure', 'Tasks');


SELECT * FROM auth.create_app_menu_policy
(
	'Admin', 
	core.get_office_id_by_office_name('Default'), 
	'ElasticEmail',
	'{*}'
);

