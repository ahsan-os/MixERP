IF OBJECT_ID('social.count_comments') IS NOT NULL
DROP FUNCTION social.count_comments;

GO

CREATE FUNCTION social.count_comments(@feed_id bigint)
RETURNS bigint
AS
BEGIN
    DECLARE @count                  bigint;

    WITH all_comments
    AS
    (
        SELECT social.feeds.feed_id 
		FROM social.feeds 
		WHERE social.feeds.parent_feed_id = @feed_id
        
		UNION ALL
        
		SELECT feed_comments.feed_id 
		FROM social.feeds AS feed_comments
        INNER JOIN all_comments
        ON all_comments.feed_id = feed_comments.parent_feed_id
    )
    
    SELECT @count = COUNT(*)
    FROM all_comments;

    RETURN @count;
END

GO

--SELECT social.count_comments(87);

