# social.count_comments function:

```plpgsql
CREATE OR REPLACE FUNCTION social.count_comments(_feed_id bigint)
RETURNS bigint
```
* Schema : [social](../../schemas/social.md)
* Function Name : count_comments
* Arguments : _feed_id bigint
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION social.count_comments(_feed_id bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
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
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

