IF OBJECT_ID('website.get_menu_id_by_menu_name') IS NOT NULL
DROP FUNCTION website.get_menu_id_by_menu_name;

GO

CREATE FUNCTION website.get_menu_id_by_menu_name(@menu_name national character varying(500))
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT menu_id
		FROM website.menus
		WHERE menu_name = @menu_name
		AND website.menus.deleted = 0
	);
END;

GO

--SELECT website.get_menu_id_by_menu_name('Default');
