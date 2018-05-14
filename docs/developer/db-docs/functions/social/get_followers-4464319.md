# social.get_followers function:

```plpgsql
CREATE OR REPLACE FUNCTION social.get_followers(_feed_id bigint, _me integer)
RETURNS text
```
* Schema : [social](../../schemas/social.md)
* Function Name : get_followers
* Arguments : _feed_id bigint, _me integer
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION social.get_followers(_feed_id bigint, _me integer)
 RETURNS text
 LANGUAGE plpgsql
AS $function$
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
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

