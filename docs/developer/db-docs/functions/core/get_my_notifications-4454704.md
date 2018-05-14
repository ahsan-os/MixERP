# core.get_my_notifications function:

```plpgsql
CREATE OR REPLACE FUNCTION core.get_my_notifications(_login_id bigint)
RETURNS TABLE(notification_id uuid, associated_app character varying, associated_menu_id integer, url character varying, formatted_text character varying, icon character varying, seen boolean, event_date timestamp with time zone)
```
* Schema : [core](../../schemas/core.md)
* Function Name : get_my_notifications
* Arguments : _login_id bigint
* Owner : frapid_db_user
* Result Type : TABLE(notification_id uuid, associated_app character varying, associated_menu_id integer, url character varying, formatted_text character varying, icon character varying, seen boolean, event_date timestamp with time zone)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.get_my_notifications(_login_id bigint)
 RETURNS TABLE(notification_id uuid, associated_app character varying, associated_menu_id integer, url character varying, formatted_text character varying, icon character varying, seen boolean, event_date timestamp with time zone)
 LANGUAGE plpgsql
AS $function$
	DECLARE _user_id							    integer;
	DECLARE _office_id							    integer;
	DECLARE _role_id							    integer;
BEGIN
    DROP TABLE IF EXISTS _result;
    CREATE TEMPORARY TABLE _result
    (
        notification_id								uuid,
        associated_app								national character varying(100),
        associated_menu_id							integer,
        url											national character varying(2000),
        formatted_text								national character varying(4000),
        icon										national character varying(100),
        seen										boolean,
        event_date									TIMESTAMP WITH TIME ZONE
    ) ON COMMIT DROP;

	
	SELECT 
		account.sign_in_view."user_id",
		account.sign_in_view.office_id,
		account.sign_in_view.role_id
	INTO
        _user_id,
        _office_id,
        _role_id
	FROM account.sign_in_view
	WHERE account.sign_in_view.login_id = _login_id;


	--UNSEEN NOTIFICATIONS
	INSERT INTO _result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT core.notifications.notification_id, core.notifications.associated_app, core.notifications.associated_menu_id, core.notifications.url, core.notifications.formatted_text, core.notifications.icon, false, core.notifications.event_timestamp
	FROM core.notifications
	WHERE core.notifications.to_login_id = _login_id
	AND core.notifications.notification_id NOT IN
	(
		SELECT core.notification_statuses.notification_id
		FROM core.notification_statuses
		WHERE seen_by = _user_id
	);
	
	INSERT INTO _result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT core.notifications.notification_id, core.notifications.associated_app, core.notifications.associated_menu_id, core.notifications.url, core.notifications.formatted_text, core.notifications.icon, false, core.notifications.event_timestamp
	FROM core.notifications
	WHERE core.notifications.to_user_id = _user_id
	AND core.notifications.to_login_id IS NULL
	AND core.notifications.notification_id NOT IN
	(
		SELECT core.notification_statuses.notification_id
		FROM core.notification_statuses
		WHERE seen_by = _user_id
	);
	
	INSERT INTO _result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT core.notifications.notification_id, core.notifications.associated_app, core.notifications.associated_menu_id, core.notifications.url, core.notifications.formatted_text, core.notifications.icon, false, core.notifications.event_timestamp
	FROM core.notifications
	WHERE core.notifications.to_role_id = _role_id
	AND core.notifications.to_user_id IS NULL
	AND core.notifications.to_login_id IS NULL
	AND core.notifications.notification_id NOT IN
	(
		SELECT core.notification_statuses.notification_id
		FROM core.notification_statuses
		WHERE seen_by = _user_id
	);

	INSERT INTO _result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT core.notifications.notification_id, core.notifications.associated_app, core.notifications.associated_menu_id, core.notifications.url, core.notifications.formatted_text, core.notifications.icon, false, core.notifications.event_timestamp
	FROM core.notifications
	WHERE (core.notifications.office_id IS NULL OR core.notifications.office_id = _office_id)
	AND core.notifications.to_role_id IS NULL
	AND core.notifications.to_user_id IS NULL
	AND core.notifications.to_login_id IS NULL
	AND core.notifications.notification_id NOT IN
	(
		SELECT core.notification_statuses.notification_id
		FROM core.notification_statuses
		WHERE seen_by = _user_id
	);


	--SEEN NOTIFICATIONS
	WITH seen_notifications
	AS
	(
		SELECT core.notifications.notification_id, core.notifications.associated_app, core.notifications.associated_menu_id, core.notifications.url, core.notifications.formatted_text, core.notifications.icon, core.notifications.event_timestamp
		FROM core.notifications
		INNER JOIN core.notification_statuses
		ON core.notification_statuses.notification_id = core.notifications.notification_id
		WHERE core.notifications.to_login_id = _login_id

		UNION ALL

		SELECT core.notifications.notification_id, core.notifications.associated_app, core.notifications.associated_menu_id, core.notifications.url, core.notifications.formatted_text, core.notifications.icon, core.notifications.event_timestamp
		FROM core.notifications
		INNER JOIN core.notification_statuses
		ON core.notification_statuses.notification_id = core.notifications.notification_id
		WHERE core.notifications.to_user_id = _user_id
		AND core.notifications.to_login_id IS NULL

		UNION ALL

		SELECT core.notifications.notification_id, core.notifications.associated_app, core.notifications.associated_menu_id, core.notifications.url, core.notifications.formatted_text, core.notifications.icon, core.notifications.event_timestamp
		FROM core.notifications
		INNER JOIN core.notification_statuses
		ON core.notification_statuses.notification_id = core.notifications.notification_id
		WHERE core.notifications.to_role_id = _role_id
		AND core.notifications.to_user_id IS NULL
		AND core.notifications.to_login_id IS NULL

		UNION ALL

		SELECT core.notifications.notification_id, core.notifications.associated_app, core.notifications.associated_menu_id, core.notifications.url, core.notifications.formatted_text, core.notifications.icon, core.notifications.event_timestamp
		FROM core.notifications
		INNER JOIN core.notification_statuses
		ON core.notification_statuses.notification_id = core.notifications.notification_id
		WHERE (core.notifications.office_id IS NULL OR core.notifications.office_id = _office_id)
		AND core.notifications.to_role_id IS NULL
		AND core.notifications.to_user_id IS NULL
		AND core.notifications.to_login_id IS NULL
	)

	INSERT INTO _result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT seen_notifications.notification_id, seen_notifications.associated_app, seen_notifications.associated_menu_id, seen_notifications.url, seen_notifications.formatted_text, seen_notifications.icon, true, seen_notifications.event_timestamp
	FROM seen_notifications
	ORDER BY event_timestamp DESC
	LIMIT 10;

	RETURN QUERY
	SELECT * FROM _result;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

