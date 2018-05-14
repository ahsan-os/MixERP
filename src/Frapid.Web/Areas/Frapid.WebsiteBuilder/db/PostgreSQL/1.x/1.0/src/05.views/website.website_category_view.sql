DROP VIEW IF EXISTS website.website_category_view;

CREATE VIEW website.website_category_view
AS
SELECT
    website.categories.category_id          AS website_category_id,
    website.categories.category_name        AS website_category_name
FROM website.categories
WHERE NOT website.categories.deleted
AND NOT website.categories.is_blog;
