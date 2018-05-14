DROP VIEW IF EXISTS website.blog_category_view;

CREATE VIEW website.blog_category_view
AS
SELECT
    website.categories.category_id          AS blog_category_id,
    website.categories.category_name        AS blog_category_name
FROM website.categories
WHERE NOT website.categories.deleted
AND website.categories.is_blog;

