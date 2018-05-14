GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Authorization',
'{*}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Account',
'{*}';



EXECUTE auth.create_app_menu_policy
'User', 
@office_id, 
'Frapid.Account',
'{Configuration Profile, Email Templates, Account Verification, Password Reset, Welcome Email, Welcome Email (3rd Party)}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Account',
'{*}';

EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.Authorization',
'{*}';

GO
