EXECUTE core.create_app 'Frapid.SchemaUpdater', 'Updates', 'Updates', '1.0', 'MixERP Inc.', 'December 1, 2015', 'teal refresh', '/dashboard/updater/home', null;
EXECUTE core.create_menu 'Frapid.SchemaUpdater', 'Offices', 'Offices', '/dashboard/updater/home', 'building outline', '';

GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.SchemaUpdater',
'{*}';

EXECUTE auth.create_app_menu_policy
'User', 
@office_id, 
'Frapid.SchemaUpdater',
'{Offices}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.SchemaUpdater',
'{*}';


GO