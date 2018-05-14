DROP VIEW IF EXISTS website.blog_scrud_view;

CREATE VIEW website.blog_scrud_view
AS
SELECT
	website.contents.content_id AS blog_id,
	website.contents.title,
	website.categories.category_name,
	website.contents.alias,
	website.contents.is_draft,
	website.contents.publish_on
FROM website.contents
INNER JOIN website.categories
ON website.categories.category_id = website.contents.category_id
WHERE NOT website.contents.deleted
AND website.categories.is_blog;