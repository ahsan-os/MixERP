IF OBJECT_ID('website.website_category_view') IS NOT NULL
DROP VIEW website.website_category_view;

GO

CREATE VIEW website.website_category_view
AS
SELECT
    website.categories.category_id          AS website_category_id,
    website.categories.category_name        AS website_category_name
FROM website.categories
WHERE website.categories.deleted = 0
AND website.categories.is_blog = 0;

GO
