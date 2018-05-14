DROP FUNCTION IF EXISTS social.unlike(_user_id integer, _feed_id bigint);

CREATE FUNCTION social.unlike(_user_id integer, _feed_id bigint)
RETURNS void
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 0 FROM social.liked_by
        WHERE social.liked_by.feed_id = _feed_id
        AND  social.liked_by.liked_by = _user_id
    ) THEN
        UPDATE social.liked_by
        SET 
            unliked     = true,
            unliked_on  = NOW()
        WHERE social.liked_by.feed_id = _feed_id
        AND social.liked_by.liked_by = _user_id;
    END IF;
END
$$
LANGUAGE plpgsql;
