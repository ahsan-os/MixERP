# social.update_audit_timestamp_trigger function:

```plpgsql
CREATE OR REPLACE FUNCTION social.update_audit_timestamp_trigger()
RETURNS trigger
```
* Schema : [social](../../schemas/social.md)
* Function Name : update_audit_timestamp_trigger
* Arguments : 
* Owner : frapid_db_user
* Result Type : trigger
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION social.update_audit_timestamp_trigger()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
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
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

