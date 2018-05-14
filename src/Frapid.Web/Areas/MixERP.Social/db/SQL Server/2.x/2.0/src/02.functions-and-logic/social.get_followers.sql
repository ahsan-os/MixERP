IF OBJECT_ID('social.get_followers') IS NOT NULL
DROP FUNCTION social.get_followers;

GO

CREATE FUNCTION social.get_followers(@feed_id bigint, @me integer)
RETURNS character varying(MAX)
AS
BEGIN
    DECLARE @followers              character varying(MAX);
    DECLARE @parent                 bigint = social.get_root_feed_id(@feed_id);
    
    WITH all_feeds
    AS
    (
        SELECT social.feeds.feed_id 
        FROM social.feeds 
        WHERE social.feeds.feed_id = @parent

        UNION ALL

        SELECT feed_comments.feed_id 
        FROM social.feeds AS feed_comments
        INNER JOIN all_feeds
        ON all_feeds.feed_id = feed_comments.parent_feed_id
    ),
    feeds
    AS
    (
        SELECT 
            all_feeds.feed_id,
            social.feeds.created_by
        FROM social.feeds
        INNER JOIN all_feeds
        ON all_feeds.feed_id = social.feeds.feed_id
    )
    
    SELECT DISTINCT @followers = COALESCE(@followers + ', ', '') + CAST(feeds.created_by AS character varying(100))
    FROM feeds
    WHERE feeds.created_by != @me;

    RETURN @followers;
END;

GO

--SELECT social.get_followers(2, 1);
