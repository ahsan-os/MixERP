DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Frapid.AddressBook'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Frapid.AddressBook'
);

DELETE FROM core.menus
WHERE app_name = 'Frapid.AddressBook';


EXECUTE core.create_app 'Frapid.AddressBook', 'AddressBook', 'AddressBook', '1.0', 'MixERP Inc.', 'December 1, 2015', 'teal phone', '/dashboard/address-book', NULL;

EXECUTE core.create_menu 'Frapid.AddressBook', 'Tasks', 'Tasks', '', 'lightning', '';
EXECUTE core.create_menu 'Frapid.AddressBook', 'AddressBook', 'AddressBook', '/dashboard/address-book', 'phone', 'Tasks';

GO
DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.AddressBook',
'{*}';
