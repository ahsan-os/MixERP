GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_api_access_policy '{Admin}', @office_id, '', '{*}', 1;

GO