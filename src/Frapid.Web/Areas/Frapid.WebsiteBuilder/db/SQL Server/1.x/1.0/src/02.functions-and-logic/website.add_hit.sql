IF OBJECT_ID('website.add_hit') IS NOT NULL
DROP PROCEDURE website.add_hit;

GO

CREATE PROCEDURE website.add_hit(@category_alias national character varying(250), @alias national character varying(500))
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	IF(COALESCE(@alias, '') = '' AND COALESCE(@category_alias, '') = '')
	BEGIN
		UPDATE website.contents SET hits = COALESCE(website.contents.hits, 0) + 1 
		WHERE is_homepage = 1;

		RETURN;
	END;

	UPDATE website.contents SET hits = COALESCE(website.contents.hits, 0) + 1 
	WHERE website.contents.content_id =
	(
		SELECT website.published_content_view.content_id 
		FROM website.published_content_view
		WHERE category_alias=@category_alias AND alias=@alias
	)
END;

GO
