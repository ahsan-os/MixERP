DROP FUNCTION IF EXISTS social.update_audit_timestamp_trigger() CASCADE;

CREATE FUNCTION social.update_audit_timestamp_trigger()
RETURNS TRIGGER
AS
$$
BEGIN
    WITH RECURSIVE ancestors
    AS
    (
        SELECT parent_feed_id from social.feeds where feed_id  = NEW.feed_id
        UNION ALL
        SELECT feeds.parent_feed_id 
        FROM social.feeds AS feeds
        INNER JOIN ancestors 
        ON ancestors.parent_feed_id =feeds.feed_id
        WHERE feeds.parent_feed_id IS NOT NULL
    )

    UPDATE social.feeds
    SET audit_ts = NOW()
    WHERE social.feeds.feed_id IN
    (
        SELECT * FROM ancestors
    );

    RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER update_audit_timestamp_trigger
AFTER INSERT
ON social.feeds
FOR EACH ROW EXECUTE PROCEDURE social.update_audit_timestamp_trigger();
