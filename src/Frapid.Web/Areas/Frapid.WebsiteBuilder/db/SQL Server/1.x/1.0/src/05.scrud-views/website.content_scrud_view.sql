IF OBJECT_ID('website.content_scrud_view') IS NOT NULL
DROP VIEW website.content_scrud_view;

GO
CREATE VIEW website.content_scrud_view
AS
SELECT
	website.contents.content_id,
	website.contents.title,
	website.categories.category_name,
	website.contents.alias,
	website.contents.is_draft,
	website.contents.publish_on
FROM website.contents
INNER JOIN website.categories
ON website.categories.category_id = website.contents.category_id
WHERE website.contents.deleted = 0
AND website.categories.is_blog = 0;

GO
