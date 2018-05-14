# social.get_root_feed_id function:

```plpgsql
CREATE OR REPLACE FUNCTION social.get_root_feed_id(_feed_id bigint)
RETURNS bigint
```
* Schema : [social](../../schemas/social.md)
* Function Name : get_root_feed_id
* Arguments : _feed_id bigint
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION social.get_root_feed_id(_feed_id bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
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
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

