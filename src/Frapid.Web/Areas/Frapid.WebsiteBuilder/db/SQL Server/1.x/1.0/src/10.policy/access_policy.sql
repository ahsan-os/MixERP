DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_api_access_policy '{Content Editor, User, Admin}', @office_id, 'website.categories', '{*}', 1;
EXECUTE auth.create_api_access_policy '{Content Editor, User, Admin}', @office_id, 'website.contents', '{*}', 1;
EXECUTE auth.create_api_access_policy '{User, Admin}', @office_id, 'website.menus', '{*}', 1;
EXECUTE auth.create_api_access_policy '{User, Admin}', @office_id, 'website.email_subscriptions', '{*}', 1;
EXECUTE auth.create_api_access_policy '{User, Admin}', @office_id, 'website.contacts', '{*}', 1;
EXECUTE auth.create_api_access_policy '{Admin}', @office_id, 'website.configurations', '{*}', 1;
