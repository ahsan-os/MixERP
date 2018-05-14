IF OBJECT_ID('social.get_root_feed_id') IS NOT NULL
DROP FUNCTION social.get_root_feed_id;

GO

CREATE FUNCTION social.get_root_feed_id(@feed_id bigint)
RETURNS bigint
AS
BEGIN
    DECLARE @parent_feed_id bigint;

    SELECT 
        @parent_feed_id = parent_feed_id
    FROM social.feeds
    WHERE social.feeds.feed_id=$1
	AND social.feeds.deleted = 0;

    

    IF(@parent_feed_id IS NULL)
	BEGIN
        RETURN $1;
	END

    RETURN social.get_root_feed_id(@parent_feed_id);
END;

GO

--SELECT social.get_root_feed_id(2)
