EXECUTE dbo.drop_schema 'calendar';
GO
CREATE SCHEMA calendar;

GO
CREATE TABLE calendar.categories
(
	category_id								integer IDENTITY PRIMARY KEY,
	user_id									integer NOT NULL REFERENCES account.users,
	category_name							national character varying(200) NOT NULL,
	color_code								national character varying(50) NOT NULL,
	is_local								bit NOT NULL DEFAULT(1),
	category_order							smallint,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
    deleted                                 bit DEFAULT(0)  
);

CREATE UNIQUE INDEX categories_user_id_category_name_uix
ON calendar.categories(user_id, category_name)
WHERE deleted = 0;

CREATE TABLE calendar.events
(
	event_id								uniqueidentifier PRIMARY KEY DEFAULT(NEWSEQUENTIALID()),
	category_id								integer NOT NULL REFERENCES calendar.categories,
	user_id									integer NOT NULL REFERENCES account.users,
	name									national character varying(500) NOT NULL,
	location								national character varying(2000),
	starts_at								DATETIMEOFFSET NOT NULL,
	ends_on									DATETIMEOFFSET NOT NULL,
	time_zone								national character varying(200) NOT NULL,
	all_day									bit NOT NULL DEFAULT(0),
	recurrence								national character varying(MAX),--JSON data
	until									DATETIMEOFFSET,
	alarm									integer,--minutes before
	reminder_types							national character varying(MAX),--JSON data
	is_private								bit,
	url										national character varying(500),
	note									national character varying(MAX),
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
    deleted                                 bit DEFAULT(0)
);

GO
