SELECT * FROM auth.create_api_access_policy('{Content Editor, User, Admin}', core.get_office_id_by_office_name('Default'), 'website.categories', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{Content Editor, User, Admin}', core.get_office_id_by_office_name('Default'), 'website.contents', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{User, Admin}', core.get_office_id_by_office_name('Default'), 'website.menus', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{User, Admin}', core.get_office_id_by_office_name('Default'), 'website.email_subscriptions', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{User, Admin}', core.get_office_id_by_office_name('Default'), 'website.contacts', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{Admin}', core.get_office_id_by_office_name('Default'), 'website.configurations', '{*}', true);
