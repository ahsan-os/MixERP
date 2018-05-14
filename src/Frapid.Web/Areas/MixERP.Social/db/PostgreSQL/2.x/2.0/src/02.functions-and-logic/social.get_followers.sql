DROP FUNCTION IF EXISTS social.get_followers(_feed_id bigint, _me integer);

CREATE FUNCTION social.get_followers(_feed_id bigint, _me integer)
RETURNS text
AS
$$
    DECLARE _followers              text;
    DECLARE _parent                 bigint;
BEGIN
    _parent := social.get_root_feed_id(_feed_id);
    
    WITH RECURSIVE all_feeds
    AS
    (
        SELECT social.feeds.feed_id 
        FROM social.feeds 
        WHERE social.feeds.feed_id = _parent

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
    
    SELECT string_agg(DISTINCT feeds.created_by::text, ',')
    INTO _followers
    FROM feeds
    WHERE feeds.created_by != _me;

    RETURN _followers;
END
$$
LANGUAGE plpgsql;

--SELECT social.get_followers(97, 1);
