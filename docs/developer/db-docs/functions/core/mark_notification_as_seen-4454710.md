# core.mark_notification_as_seen function:

```plpgsql
CREATE OR REPLACE FUNCTION core.mark_notification_as_seen(_notification_id uuid, _user_id integer)
RETURNS void
```
* Schema : [core](../../schemas/core.md)
* Function Name : mark_notification_as_seen
* Arguments : _notification_id uuid, _user_id integer
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.mark_notification_as_seen(_notification_id uuid, _user_id integer)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
	IF EXISTS
	(
		SELECT 0 FROM core.notification_statuses
		WHERE notification_id = _notification_id
		AND seen_by = _user_id
		LIMIT 1
	) THEN
		UPDATE core.notification_statuses
		SET last_seen_on = NOW()
		WHERE notification_id = _notification_id
		AND seen_by = _user_id;

		RETURN;
	END IF;

	INSERT INTO core.notification_statuses(notification_id, last_seen_on, seen_by)
	SELECT _notification_id, NOW(), _user_id;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

