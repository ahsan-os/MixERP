IF OBJECT_ID('core.mark_notification_as_seen') IS NOT NULL
DROP PROCEDURE core.mark_notification_as_seen;

GO

CREATE PROCEDURE core.mark_notification_as_seen(@notification_id uniqueidentifier, @user_id integer)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	IF EXISTS
	(
		SELECT TOP 1 0 FROM core.notification_statuses
		WHERE notification_id = @notification_id
		AND seen_by = @user_id
	)
	BEGIN
		UPDATE core.notification_statuses
		SET last_seen_on = GETUTCDATE()
		WHERE notification_id = @notification_id
		AND seen_by = @user_id;

		RETURN;
	END;

	INSERT INTO core.notification_statuses(notification_id, last_seen_on, seen_by)
	SELECT @notification_id, GETUTCDATE(), @user_id;
END;

GO
