IF OBJECT_ID('social.get_next_top_feeds') IS NOT NULL
DROP FUNCTION social.get_next_top_feeds;

GO

CREATE FUNCTION social.get_next_top_feeds(@user_id integer, @last_feed_id bigint, @parent_feed_id bigint, @url national character varying(4000))
RETURNS @results TABLE
(
    row_number                      bigint,
    feed_id                         bigint,
    event_timestamp                 DATETIMEOFFSET,
    audit_ts                        DATETIMEOFFSET,
    formatted_text                  national character varying(4000),    
    created_by                      integer,
    created_by_name                 national character varying(100),
    attachments                     text,
    scope                           national character varying(100),
    is_public                       bit,
    parent_feed_id                  bigint,
    child_count                     bigint
)
AS
BEGIN
    DECLARE @role_id                integer;


    SELECT @role_id = account.users.role_id
    FROM account.users
    WHERE account.users.user_id = @user_id;

    INSERT INTO @results(feed_id, event_timestamp, audit_ts, formatted_text, created_by, created_by_name, attachments, scope, is_public, parent_feed_id)
    SELECT
	TOP 10
        social.feeds.feed_id,
        social.feeds.event_timestamp,
        social.feeds.audit_ts,
        social.feeds.formatted_text,
        social.feeds.created_by,
        account.get_name_by_user_id(social.feeds.created_by),
        social.feeds.attachments,
        social.feeds.scope,
        social.feeds.is_public,
        social.feeds.parent_feed_id        
    FROM social.feeds
    WHERE social.feeds.deleted = 0
    AND (@last_feed_id = 0 OR social.feeds.feed_id < @last_feed_id)
    AND COALESCE(social.feeds.parent_feed_id, 0) = COALESCE(@parent_feed_id, 0)
	AND COALESCE(social.feeds.url, '') = COALESCE(@url, '')
    AND 
    (
        social.feeds.is_public = 1
        OR
        social.feeds.feed_id IN
        (
            SELECT social.shared_with.feed_id
            FROM social.shared_with
            WHERE (@last_feed_id = 0 OR social.shared_with.feed_id < @last_feed_id)
            AND (social.shared_with.role_id = @role_id OR social.shared_with.user_id = @user_id)
        )
    )
    ORDER BY social.feeds.audit_ts DESC;

    WITH all_comments
    AS
    (
        SELECT
            social.feeds.feed_id,
            social.feeds.event_timestamp,
            social.feeds.audit_ts,
            social.feeds.formatted_text,
            social.feeds.created_by,
            account.get_name_by_user_id(social.feeds.created_by) AS name,
            social.feeds.attachments,
            social.feeds.scope,
            social.feeds.is_public,
            social.feeds.parent_feed_id,
            ROW_NUMBER() OVER(PARTITION BY social.feeds.parent_feed_id ORDER BY social.feeds.audit_ts DESC) AS row_number
        FROM social.feeds
        WHERE social.feeds.deleted = 0
        AND (@last_feed_id = 0 OR social.feeds.feed_id < @last_feed_id)
        AND social.feeds.parent_feed_id IN
        (
            SELECT feed_id
            FROM @results
        )        
        AND 
        (
            social.feeds.is_public = 1
            OR
            social.feeds.feed_id IN
            (
                SELECT social.shared_with.feed_id
                FROM social.shared_with
                WHERE (@last_feed_id = 0 OR social.shared_with.feed_id < @last_feed_id)
                AND (social.shared_with.role_id = @role_id OR social.shared_with.user_id = @user_id)
            )
        )
    )
    INSERT INTO @results(feed_id, event_timestamp, audit_ts, formatted_text, created_by, created_by_name, attachments, scope, is_public, parent_feed_id)
    SELECT
        all_comments.feed_id,
        all_comments.event_timestamp,
        all_comments.audit_ts,
        all_comments.formatted_text,
        all_comments.created_by,
        all_comments.name,
        all_comments.attachments,
        all_comments.scope,
        all_comments.is_public,
        all_comments.parent_feed_id
    FROM all_comments
    WHERE all_comments.row_number <= 10;

    UPDATE @results
    SET 
		child_count = social.count_comments(feed_id);
	
	WITH ordered AS
	(
		SELECT ROW_NUMBER() OVER(ORDER BY audit_ts DESC) AS row_number, feed_id 
		FROM @results
	)

	UPDATE results
	SET 
		row_number = ordered.row_number
	FROM @results AS results
	INNER JOIN ordered
	ON results.feed_id = ordered.feed_id;
	

	RETURN;
END;

GO

--SELECT * FROM social.get_next_top_feeds(1, 0, 0, '');


