IF OBJECT_ID('website.get_category_id_by_category_name') IS NOT NULL
DROP FUNCTION website.get_category_id_by_category_name;

GO

CREATE FUNCTION website.get_category_id_by_category_name(@category_name national character varying(500))
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT category_id
		FROM website.categories
		WHERE category_name = @category_name
		AND website.categories.deleted = 0
	);
END;

GO

IF OBJECT_ID('website.get_category_id_by_category_alias') IS NOT NULL
DROP FUNCTION website.get_category_id_by_category_alias;

GO

CREATE FUNCTION website.get_category_id_by_category_alias(@alias national character varying(500))
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT category_id
		FROM website.categories
		WHERE alias = @alias
		AND website.categories.deleted = 0
	);
END;

GO
