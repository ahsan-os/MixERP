DROP FUNCTION IF EXISTS core.mark_notification_as_seen(_notification_id uuid, _user_id integer);

CREATE FUNCTION core.mark_notification_as_seen(_notification_id uuid, _user_id integer)
RETURNS void
AS
$$
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
$$
LANGUAGE plpgsql;
