IF OBJECT_ID('calendar.event_view') IS NOT NULL
DROP VIEW calendar.event_view;
GO

CREATE VIEW calendar.event_view
AS
SELECT
    calendar.events.event_id,
    calendar.events.category_id,
    calendar.categories.category_name,
    calendar.categories.color_code,
    calendar.categories.is_local AS is_local_calendar,
    calendar.categories.category_order,
    calendar.events.user_id,
    calendar.events.name,
    calendar.events.location,
    calendar.events.starts_at,
    calendar.events.ends_on,
    calendar.events.all_day,
    calendar.events.recurrence,
    calendar.events.alarm,
    calendar.events.url,
    calendar.events.note,
    calendar.events.reminder_types,
    calendar.events.is_private,
    calendar.events.until
FROM calendar.events
INNER JOIN calendar.categories
ON calendar.categories.category_id = calendar.events.category_id
WHERE calendar.events.deleted = 0
AND calendar.categories.deleted = 0;

GO
