SELECT * FROM core.create_app('Frapid.Authorization', 'Authorization', 'Authorization', '1.0', 'MixERP Inc.', 'December 1, 2015', 'purple privacy', '/dashboard/authorization/menu-access/group-policy', '{Frapid.Account}'::text[]);

SELECT * FROM core.create_menu('Frapid.Authorization', 'EntityAccessPolicy', 'Entity Access Policy', '', 'lock', '');
SELECT * FROM core.create_menu('Frapid.Authorization', 'GroupEntityAccessPolicy', 'Group Entity Access Policy', '/dashboard/authorization/entity-access/group-policy', 'users', 'Entity Access Policy');
SELECT * FROM core.create_menu('Frapid.Authorization', 'UserEntityAccessPolicy', 'User Entity Access Policy', '/dashboard/authorization/entity-access/user-policy', 'user', 'Entity Access Policy');
SELECT * FROM core.create_menu('Frapid.Authorization', 'MenuAccessPolicy', 'Menu Access Policy', '', 'toggle on', '');
SELECT * FROM core.create_menu('Frapid.Authorization', 'GroupPolicy', 'Group Policy', '/dashboard/authorization/menu-access/group-policy', 'users', 'Menu Access Policy');
SELECT * FROM core.create_menu('Frapid.Authorization', 'UserPolicy', 'User Policy', '/dashboard/authorization/menu-access/user-policy', 'user', 'Menu Access Policy');


SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.Authorization',
    '{*}'::text[]
);

SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.Account',
    '{*}'::text[]
);
