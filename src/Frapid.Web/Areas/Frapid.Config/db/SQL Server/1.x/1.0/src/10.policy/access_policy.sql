DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.kanban_details', '{*}', 1;
EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.flag_types', '{*}', 1;
EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.flag_view', '{*}', 1;
EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.kanbans', '{*}', 1;
EXECUTE auth.create_api_access_policy '{*}', @office_id, 'config.filter_name_view', '{*}', 1;

EXECUTE auth.create_api_access_policy '{User}', @office_id, 'core.offices', '{*}', 1;
EXECUTE auth.create_api_access_policy '{Admin}', @office_id, '', '{*}', 1;



GO
