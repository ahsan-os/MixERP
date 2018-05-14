EXECUTE core.create_app 'Frapid.Account', 'Account', 'Account', '1.0', 'MixERP Inc.', 'December 1, 2015', 'grey lock', '/dashboard/account/user/list', NULL;

EXECUTE core.create_menu 'Frapid.Account', 'Roles', 'Roles', '/dashboard/account/roles', 'users', '';

EXECUTE core.create_menu 'Frapid.Account', 'Users', 'Users', '', 'user', '';
EXECUTE core.create_menu 'Frapid.Account', 'AddNewUser', 'Add a New User', '/dashboard/account/user/add', 'user', 'Users';
EXECUTE core.create_menu 'Frapid.Account', 'ChangePassword', 'Change Password', '/dashboard/account/user/change-password', 'user', 'Users';
EXECUTE core.create_menu 'Frapid.Account', 'ListUsers', 'List Users', '/dashboard/account/user/list', 'user', 'Users';

EXECUTE core.create_menu 'Frapid.Account', 'ConfigurationProfile', 'Configuration Profile', '/dashboard/account/configuration-profile', 'configure', '';
EXECUTE core.create_menu 'Frapid.Account', 'EmailTemplates', 'Email Templates', '', 'mail', '';
EXECUTE core.create_menu 'Frapid.Account', 'AccountVerification', 'Account Verification', '/dashboard/account/email-templates/account-verification', 'checkmark box', 'Email Templates';
EXECUTE core.create_menu 'Frapid.Account', 'PasswordReset', 'Password Reset', '/dashboard/account/email-templates/password-reset', 'key', 'Email Templates';
EXECUTE core.create_menu 'Frapid.Account', 'WelcomeEmail', 'Welcome Email', '/dashboard/account/email-templates/welcome-email', 'star', 'Email Templates';
EXECUTE core.create_menu 'Frapid.Account', 'WelcomeEmail3rdParty)', 'Welcome Email (3rd Party)', '/dashboard/account/email-templates/welcome-email-other', 'star outline', 'Email Templates';

