DROP FUNCTION IF EXISTS website.get_menu_id_by_menu_name(_menu_name national character varying(500));

CREATE FUNCTION website.get_menu_id_by_menu_name(_menu_name national character varying(500))
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
		SELECT menu_id
		FROM website.menus
		WHERE menu_name = _menu_name
		AND NOT website.menus.deleted
	);
END
$$
LANGUAGE plpgsql;

--SELECT * FROM website.get_menu_id_by_menu_name('Default');

