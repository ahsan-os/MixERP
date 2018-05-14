DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Config',
'{*}';

EXECUTE auth.create_app_menu_policy
'User', 
@office_id, 
'Frapid.Config',
'{Offices}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Config',
'{*}';


GO