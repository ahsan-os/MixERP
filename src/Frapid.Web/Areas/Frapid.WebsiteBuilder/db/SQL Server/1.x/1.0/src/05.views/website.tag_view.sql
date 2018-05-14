IF OBJECT_ID('website.tag_view') IS NOT NULL
DROP VIEW website.tag_view;
GO
CREATE VIEW website.tag_view
AS
WITH tags
AS
(
	SELECT DISTINCT split.member AS tag
	FROM website.contents
	CROSS APPLY core.split(website.contents.tags)
	WHERE website.contents.deleted = 0
)
SELECT
    ROW_NUMBER() OVER (ORDER BY tag) AS tag_id,
    tag
FROM tags;

GO