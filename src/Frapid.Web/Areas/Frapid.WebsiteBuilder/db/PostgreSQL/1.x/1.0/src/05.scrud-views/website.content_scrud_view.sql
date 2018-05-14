DROP VIEW IF EXISTS website.content_scrud_view;

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
WHERE NOT website.contents.deleted
AND NOT website.categories.is_blog;