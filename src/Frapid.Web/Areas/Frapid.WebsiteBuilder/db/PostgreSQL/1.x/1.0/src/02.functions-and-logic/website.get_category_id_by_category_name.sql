DROP FUNCTION IF EXISTS website.get_category_id_by_category_name(_category_name text);

CREATE FUNCTION website.get_category_id_by_category_name(_category_name text)
RETURNS integer
AS
$$
BEGIN
    RETURN category_id
    FROM website.categories
    WHERE website.categories.category_name = _category_name
	AND NOT website.categories.deleted;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS website.get_category_id_by_category_alias(_alias text);

CREATE FUNCTION website.get_category_id_by_category_alias(_alias text)
RETURNS integer
AS
$$
BEGIN
    RETURN category_id
    FROM website.categories
    WHERE website.categories.alias = _alias
	AND NOT website.categories.deleted;
END
$$
LANGUAGE plpgsql;