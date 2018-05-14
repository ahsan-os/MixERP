IF OBJECT_ID('website.blog_category_view') IS NOT NULL
DROP VIEW website.blog_category_view;

GO

CREATE VIEW website.blog_category_view
AS
SELECT
    website.categories.category_id          AS blog_category_id,
    website.categories.category_name        AS blog_category_name
FROM website.categories
WHERE website.categories.deleted = 0
AND website.categories.is_blog = 1;

GO