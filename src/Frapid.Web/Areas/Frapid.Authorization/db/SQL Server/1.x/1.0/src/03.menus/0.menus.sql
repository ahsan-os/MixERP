EXECUTE core.create_app 'Frapid.Authorization', 'Authorization', 'Authorization', '1.0', 'MixERP Inc.', 'December 1, 2015', 'purple privacy', '/dashboard/authorization/menu-access/group-policy', '{Frapid.Account}';



EXECUTE core.create_menu 'Frapid.Authorization', 'EntityAccessPolicy', 'Entity Access Policy', '', 'lock', '';
EXECUTE core.create_menu 'Frapid.Authorization', 'GroupEntityAccessPolicy', 'Group Entity Access Policy', '/dashboard/authorization/entity-access/group-policy', 'users', 'Entity Access Policy';
EXECUTE core.create_menu 'Frapid.Authorization', 'UserEntityAccessPolicy', 'User Entity Access Policy', '/dashboard/authorization/entity-access/user-policy', 'user', 'Entity Access Policy';
EXECUTE core.create_menu 'Frapid.Authorization', 'MenuAccessPolicy', 'Menu Access Policy', '', 'toggle on', '';
EXECUTE core.create_menu 'Frapid.Authorization', 'GroupPolicy', 'Group Policy', '/dashboard/authorization/menu-access/group-policy', 'users', 'Menu Access Policy';
EXECUTE core.create_menu 'Frapid.Authorization', 'UserPolicy', 'User Policy', '/dashboard/authorization/menu-access/user-policy', 'user', 'Menu Access Policy';

GO
