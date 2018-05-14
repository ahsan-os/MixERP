IF OBJECT_ID('social.update_audit_timestamp_trigger') IS NOT NULL
DROP TRIGGER social.update_audit_timestamp_trigger;

GO

CREATE TRIGGER social.update_audit_timestamp_trigger
ON social.feeds
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

    WITH ancestors
    AS
    (
        SELECT parent_feed_id from social.feeds where feed_id IN
		(
			SELECT feed_id
			FROM INSERTED
		)
        UNION ALL
        SELECT feeds.parent_feed_id 
        FROM social.feeds AS feeds
        INNER JOIN ancestors 
        ON ancestors.parent_feed_id =feeds.feed_id
        WHERE feeds.parent_feed_id IS NOT NULL
    )

    UPDATE social.feeds
    SET audit_ts = getutcdate()
    WHERE social.feeds.feed_id IN
    (
        SELECT * FROM ancestors
    );

	RETURN;
END;

GO
