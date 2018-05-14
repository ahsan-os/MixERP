SELECT * FROM auth.create_app_menu_policy
(
    'User', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.Account',
    '{Configuration Profile, Email Templates, Account Verification, Password Reset, Welcome Email, Welcome Email (3rd Party)}'::text[]
);

SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.Account',
    '{*}'::text[]
);