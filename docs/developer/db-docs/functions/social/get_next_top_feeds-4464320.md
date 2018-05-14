# social.get_next_top_feeds function:

```plpgsql
CREATE OR REPLACE FUNCTION social.get_next_top_feeds(_user_id integer, _last_feed_id bigint, _parent_feed_id bigint)
RETURNS TABLE(row_number bigint, feed_id bigint, event_timestamp timestamp with time zone, audit_ts timestamp with time zone, formatted_text character varying, created_by integer, created_by_name character varying, attachments text, scope character varying, is_public boolean, parent_feed_id bigint, child_count bigint)
```
* Schema : [social](../../schemas/social.md)
* Function Name : get_next_top_feeds
* Arguments : _user_id integer, _last_feed_id bigint, _parent_feed_id bigint
* Owner : frapid_db_user
* Result Type : TABLE(row_number bigint, feed_id bigint, event_timestamp timestamp with time zone, audit_ts timestamp with time zone, formatted_text character varying, created_by integer, created_by_name character varying, attachments text, scope character varying, is_public boolean, parent_feed_id bigint, child_count bigint)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION social.get_next_top_feeds(_user_id integer, _last_feed_id bigint, _parent_feed_id bigint)
 RETURNS TABLE(row_number bigint, feed_id bigint, event_timestamp timestamp with time zone, audit_ts timestamp with time zone, formatted_text character varying, created_by integer, created_by_name character varying, attachments text, scope character varying, is_public boolean, parent_feed_id bigint, child_count bigint)
 LANGUAGE plpgsql
AS $function$
    DECLARE _role_id                integer;
BEGIN

    DROP TABLE IF EXISTS temp_feeds;

    CREATE TEMPORARY TABLE temp_feeds
    (
        feed_id                         bigint,
        event_timestamp                 TIMESTAMP WITH TIME ZONE,
        audit_ts                        TIMESTAMP WITH TIME ZONE,
        formatted_text                  national character varying(4000),    
        created_by                      integer,
        created_by_name                 national character varying(100),
        attachments                     text,
        scope                           national character varying(100),
        is_public                       boolean,
        parent_feed_id                  bigint,
        child_count                     bigint
    );

    SELECT account.users.role_id INTO _role_id
    FROM account.users
    WHERE account.users.user_id = _user_id;

    INSERT INTO temp_feeds
    SELECT
        social.feeds.feed_id,
        social.feeds.event_timestamp,
        social.feeds.audit_ts,
        social.feeds.formatted_text,
        social.feeds.created_by,
        account.get_name_by_user_id(social.feeds.created_by)::national character varying(100),
        social.feeds.attachments,
        social.feeds.scope,
        social.feeds.is_public,
        social.feeds.parent_feed_id        
    FROM social.feeds
    WHERE NOT social.feeds.deleted
    AND (_last_feed_id = 0 OR social.feeds.feed_id < _last_feed_id)
    AND COALESCE(social.feeds.parent_feed_id, 0) = COALESCE(_parent_feed_id, 0)
    AND 
    (
        social.feeds.is_public
        OR
        social.feeds.feed_id IN
        (
            SELECT social.shared_with.feed_id
            FROM social.shared_with
            WHERE (_last_feed_id = 0 OR social.shared_with.feed_id < _last_feed_id)
            AND (social.shared_with.role_id = _role_id OR social.shared_with.user_id = _user_id)
        )
    )
    ORDER BY social.feeds.audit_ts DESC
    LIMIT 10;

    WITH all_comments
    AS
    (
        SELECT
            social.feeds.feed_id,
            social.feeds.event_timestamp,
            social.feeds.audit_ts,
            social.feeds.formatted_text,
            social.feeds.created_by,
            account.get_name_by_user_id(social.feeds.created_by)::national character varying(100) AS name,
            social.feeds.attachments,
            social.feeds.scope,
            social.feeds.is_public,
            social.feeds.parent_feed_id,
            ROW_NUMBER() OVER(PARTITION BY social.feeds.parent_feed_id ORDER BY social.feeds.audit_ts DESC) AS row_number
        FROM social.feeds
        WHERE NOT social.feeds.deleted
        AND (_last_feed_id = 0 OR social.feeds.feed_id < _last_feed_id)
        AND social.feeds.parent_feed_id IN
        (
            SELECT temp_feeds.feed_id
            FROM temp_feeds
        )        
        AND 
        (
            social.feeds.is_public
            OR
            social.feeds.feed_id IN
            (
                SELECT social.shared_with.feed_id
                FROM social.shared_with
                WHERE (_last_feed_id = 0 OR social.shared_with.feed_id < _last_feed_id)
                AND (social.shared_with.role_id = _role_id OR social.shared_with.user_id = _user_id)
            )
        )
    )
    INSERT INTO temp_feeds
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

    UPDATE temp_feeds
    SET child_count = social.count_comments(temp_feeds.feed_id);

    RETURN QUERY
    SELECT ROW_NUMBER() OVER(ORDER BY temp_feeds.audit_ts DESC), * FROM temp_feeds
    ORDER BY 1;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

