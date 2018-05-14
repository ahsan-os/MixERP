DROP FUNCTION IF EXISTS social.count_comments(_feed_id bigint);

CREATE FUNCTION social.count_comments(_feed_id bigint)
RETURNS bigint
AS
$$
    DECLARE _count                  bigint;
BEGIN
    WITH RECURSIVE all_comments
    AS
    (
        SELECT social.feeds.feed_id 
        FROM social.feeds 
        WHERE social.feeds.parent_feed_id = _feed_id

        UNION ALL

        SELECT feed_comments.feed_id 
        FROM social.feeds AS feed_comments
        INNER JOIN all_comments
        ON all_comments.feed_id = feed_comments.parent_feed_id
    )
    
    SELECT COUNT(*) INTO _count 
    FROM all_comments;

    RETURN _count;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM social.count_comments(87);
