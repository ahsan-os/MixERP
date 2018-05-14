DROP FUNCTION IF EXISTS website.add_hit(_category_alias national character varying(250), _alias national character varying(500));

CREATE FUNCTION website.add_hit(_category_alias national character varying(250), _alias national character varying(500))
RETURNS void
AS
$$
BEGIN
	IF(COALESCE(_alias, '') = '' AND COALESCE(_category_alias, '') = '') THEN
		UPDATE website.contents SET hits = COALESCE(website.contents.hits, 0) + 1 
		WHERE is_homepage;

		RETURN;
	END IF;

	UPDATE website.contents SET hits = COALESCE(website.contents.hits, 0) + 1 
	WHERE website.contents.content_id
	=
	(
		SELECT website.published_content_view.content_id 
		FROM website.published_content_view
		WHERE category_alias=_category_alias AND alias=_alias
	);
END
$$
LANGUAGE plpgsql;
