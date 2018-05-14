IF OBJECT_ID('core.get_my_notifications') IS NOT NULL
DROP FUNCTION core.get_my_notifications;

GO

CREATE FUNCTION core.get_my_notifications(@login_id bigint)
RETURNS @result TABLE
(
	notification_id								uniqueidentifier,
	associated_app								national character varying(100),
	associated_menu_id							integer,
	url											national character varying(2000),
	formatted_text								national character varying(4000),
	icon										national character varying(100),
	seen										bit,
	event_date									DATETIMEOFFSET
)
AS
BEGIN
	DECLARE @user_id							integer;
	DECLARE @office_id							integer;
	DECLARE @role_id							integer;
	
	SELECT 
		@user_id = account.sign_in_view."user_id",
		@office_id = account.sign_in_view.office_id,
		@role_id = account.sign_in_view.role_id
	FROM account.sign_in_view
	WHERE account.sign_in_view.login_id = @login_id;


	--UNSEEN NOTIFICATIONS
	INSERT INTO @result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT notification_id, associated_app, associated_menu_id, url, formatted_text, icon, 0, event_timestamp
	FROM core.notifications
	WHERE core.notifications.to_login_id = @login_id
	AND notification_id NOT IN
	(
		SELECT notification_id
		FROM core.notification_statuses
		WHERE seen_by = @user_id
	);
	
	INSERT INTO @result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT notification_id, associated_app, associated_menu_id, url, formatted_text, icon, 0, event_timestamp
	FROM core.notifications
	WHERE core.notifications.to_user_id = @user_id
	AND core.notifications.to_login_id IS NULL
	AND notification_id NOT IN
	(
		SELECT notification_id
		FROM core.notification_statuses
		WHERE seen_by = @user_id
	);
	
	INSERT INTO @result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT notification_id, associated_app, associated_menu_id, url, formatted_text, icon, 0, event_timestamp
	FROM core.notifications
	WHERE core.notifications.to_role_id = @role_id
	AND core.notifications.to_user_id IS NULL
	AND core.notifications.to_login_id IS NULL
	AND notification_id NOT IN
	(
		SELECT notification_id
		FROM core.notification_statuses
		WHERE seen_by = @user_id
	);

	INSERT INTO @result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT notification_id, associated_app, associated_menu_id, url, formatted_text, icon, 0, event_timestamp
	FROM core.notifications
	WHERE (core.notifications.office_id IS NULL OR core.notifications.office_id = @office_id)
	AND core.notifications.to_role_id IS NULL
	AND core.notifications.to_user_id IS NULL
	AND core.notifications.to_login_id IS NULL
	AND notification_id NOT IN
	(
		SELECT notification_id
		FROM core.notification_statuses
		WHERE seen_by = @user_id
	);


	--SEEN NOTIFICATIONS
	WITH seen_notifications
	AS
	(
		SELECT core.notification_statuses.notification_id, associated_app, associated_menu_id, url, formatted_text, icon, event_timestamp
		FROM core.notifications
		INNER JOIN core.notification_statuses
		ON core.notification_statuses.notification_id = core.notifications.notification_id
		WHERE core.notifications.to_login_id = @login_id

		UNION ALL

		SELECT core.notification_statuses.notification_id, associated_app, associated_menu_id, url, formatted_text, icon, event_timestamp
		FROM core.notifications
		INNER JOIN core.notification_statuses
		ON core.notification_statuses.notification_id = core.notifications.notification_id
		WHERE core.notifications.to_user_id = @user_id
		AND core.notifications.to_login_id IS NULL

		UNION ALL

		SELECT core.notification_statuses.notification_id, associated_app, associated_menu_id, url, formatted_text, icon, event_timestamp
		FROM core.notifications
		INNER JOIN core.notification_statuses
		ON core.notification_statuses.notification_id = core.notifications.notification_id
		WHERE core.notifications.to_role_id = @role_id
		AND core.notifications.to_user_id IS NULL
		AND core.notifications.to_login_id IS NULL

		UNION ALL

		SELECT core.notification_statuses.notification_id, associated_app, associated_menu_id, url, formatted_text, icon, event_timestamp
		FROM core.notifications
		INNER JOIN core.notification_statuses
		ON core.notification_statuses.notification_id = core.notifications.notification_id
		WHERE (core.notifications.office_id IS NULL OR core.notifications.office_id = @office_id)
		AND core.notifications.to_role_id IS NULL
		AND core.notifications.to_user_id IS NULL
		AND core.notifications.to_login_id IS NULL
	)

	INSERT INTO @result(notification_id, associated_app, associated_menu_id, url, formatted_text, icon, seen, event_date)
	SELECT TOP 10 notification_id, associated_app, associated_menu_id, url, formatted_text, icon, 1, event_timestamp
	FROM seen_notifications
	ORDER BY event_timestamp DESC;

	RETURN;
END;

GO

--SELECT * FROM core.get_my_notifications(5);