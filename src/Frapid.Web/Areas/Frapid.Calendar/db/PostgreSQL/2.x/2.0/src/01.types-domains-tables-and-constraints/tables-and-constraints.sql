DROP SCHEMA IF EXISTS calendar CASCADE;
CREATE SCHEMA calendar;

CREATE TABLE calendar.categories
(
	category_id								SERIAL PRIMARY KEY,
	user_id									integer NOT NULL REFERENCES account.users,
	category_name							national character varying(200) NOT NULL,
	color_code								national character varying(50) NOT NULL,
	is_local								boolean NOT NULL DEFAULT(true),
	category_order							smallint,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW()),
    deleted                                 boolean DEFAULT(false)  
);

CREATE UNIQUE INDEX categories_user_id_category_name_uix
ON calendar.categories(user_id, UPPER(category_name))
WHERE NOT deleted;

CREATE TABLE calendar.events
(
	event_id								uuid PRIMARY KEY DEFAULT(gen_random_uuid()),
	category_id								integer NOT NULL REFERENCES calendar.categories,
	user_id									integer NOT NULL REFERENCES account.users,
	name									national character varying(500) NOT NULL,
	location								national character varying(2000),
	starts_at								TIMESTAMP WITH TIME ZONE NOT NULL,
	ends_on									TIMESTAMP WITH TIME ZONE NOT NULL,
	time_zone								national character varying(200) NOT NULL,
	all_day									boolean NOT NULL DEFAULT(false),
	recurrence								text,--JSON data
	until									TIMESTAMP WITH TIME ZONE,
	alarm									integer,--minutes before
	reminder_types							text,--JSON data
	is_private								boolean,
	url										national character varying(500),
	note									text,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW()),
    deleted                                 boolean DEFAULT(false)
);

