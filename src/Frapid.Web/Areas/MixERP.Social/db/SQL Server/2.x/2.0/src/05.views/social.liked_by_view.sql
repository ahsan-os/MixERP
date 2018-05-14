IF OBJECT_ID('social.liked_by_view') IS NOT NULL
DROP VIEW social.liked_by_view;

GO

CREATE VIEW social.liked_by_view
AS
SELECT
    social.liked_by.feed_id,
    social.liked_by.liked_by,
    account.get_name_by_user_id(social.liked_by.liked_by) AS liked_by_name,
    social.liked_by.liked_on
FROM social.liked_by
WHERE social.liked_by.unliked = 0;

GO