DROP FUNCTION IF EXISTS social.get_root_feed_id(bigint);

CREATE FUNCTION social.get_root_feed_id(_feed_id bigint)
RETURNS bigint
AS
$$
    DECLARE _parent_feed_id bigint;
BEGIN
    SELECT 
        parent_feed_id
        INTO _parent_feed_id
    FROM social.feeds
    WHERE social.feeds.feed_id=$1
	AND NOT social.feeds.deleted;

    

    IF(_parent_feed_id IS NULL) THEN
        RETURN $1;
    ELSE
        RETURN social.get_root_feed_id(_parent_feed_id);
    END IF; 
END
$$
LANGUAGE plpgsql;

--SELECT social.get_root_feed_id(97)