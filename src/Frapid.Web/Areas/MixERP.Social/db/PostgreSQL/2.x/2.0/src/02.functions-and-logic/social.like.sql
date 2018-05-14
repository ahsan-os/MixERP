DROP FUNCTION IF EXISTS social.like(_user_id integer, _feed_id bigint);

CREATE FUNCTION social.like(_user_id integer, _feed_id bigint)
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 0 
        FROM social.liked_by
        WHERE social.liked_by.feed_id = _feed_id
        AND social.liked_by.liked_by = _user_id
    ) THEN
        INSERT INTO social.liked_by(feed_id, liked_by)
        SELECT _feed_id, _user_id;
    END IF;

    IF EXISTS
    (
        SELECT 0 
        FROM social.liked_by
        WHERE social.liked_by.feed_id = _feed_id
        AND social.liked_by.liked_by = _user_id
        AND social.liked_by.unliked
    ) THEN
        UPDATE social.liked_by
        SET
            unliked     = false,
            unliked_on  = NULL
        WHERE social.liked_by.feed_id = _feed_id
        AND social.liked_by.liked_by = _user_id;
    END IF;
END
$$
LANGUAGE plpgsql;
