-->-->-- src/Frapid.Web/Areas/Frapid.Calendar/db/PostgreSQL/2.x/2.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
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



-->-->-- src/Frapid.Web/Areas/Frapid.Calendar/db/PostgreSQL/2.x/2.0/src/03.menus/menus.sql --<--<--
DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Frapid.Calendar'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Frapid.Calendar'
);

DELETE FROM core.menus
WHERE app_name = 'Frapid.Calendar';


SELECT * FROM core.create_app('Frapid.Calendar', 'Calendar', 'Calendar', '1.0', 'MixERP Inc.', 'December 1, 2015', 'violet calendar', '/dashboard/calendar', NULL);

SELECT * FROM core.create_menu('Frapid.Calendar', 'Tasks', 'Tasks', '', 'lightning', '');
SELECT * FROM core.create_menu('Frapid.Calendar', 'Calendar', 'Calendar', '/dashboard/calendar', 'calendar', 'Tasks');


SELECT * FROM auth.create_app_menu_policy
(
	'Admin', 
	core.get_office_id_by_office_name('Default'), 
	'Frapid.Calendar',
	'{*}'
);


-->-->-- src/Frapid.Web/Areas/Frapid.Calendar/db/PostgreSQL/2.x/2.0/src/04.default-values/01.default-values.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.Calendar/db/PostgreSQL/2.x/2.0/src/05.views/calendar.event_view.sql --<--<--
DROP VIEW IF EXISTS calendar.event_view;

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
    calendar.events.time_zone,
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
WHERE NOT calendar.events.deleted
AND NOT calendar.categories.deleted;



-->-->-- src/Frapid.Web/Areas/Frapid.Calendar/db/PostgreSQL/2.x/2.0/src/99.ownership.sql --<--<--
DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT * FROM pg_tables 
    WHERE NOT schemaname = ANY(ARRAY['pg_catalog', 'information_schema'])
    AND tableowner <> 'frapid_db_user'
    LOOP
        EXECUTE 'ALTER TABLE '|| this.schemaname || '.' || this.tablename ||' OWNER TO frapid_db_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;

DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT oid::regclass::text as mat_view
    FROM   pg_class
    WHERE  relkind = 'm'
    LOOP
        EXECUTE 'ALTER TABLE '|| this.mat_view ||' OWNER TO frapid_db_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;

DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT 'ALTER '
        || CASE WHEN p.proisagg THEN 'AGGREGATE ' ELSE 'FUNCTION ' END
        || quote_ident(n.nspname) || '.' || quote_ident(p.proname) || '(' 
        || pg_catalog.pg_get_function_identity_arguments(p.oid) || ') OWNER TO frapid_db_user;' AS sql
    FROM   pg_catalog.pg_proc p
    JOIN   pg_catalog.pg_namespace n ON n.oid = p.pronamespace
    WHERE  NOT n.nspname = ANY(ARRAY['pg_catalog', 'information_schema'])
    LOOP        
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT * FROM pg_views
    WHERE NOT schemaname = ANY(ARRAY['pg_catalog', 'information_schema'])
    AND viewowner <> 'frapid_db_user'
    LOOP
        EXECUTE 'ALTER VIEW '|| this.schemaname || '.' || this.viewname ||' OWNER TO frapid_db_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT 'ALTER SCHEMA ' || nspname || ' OWNER TO frapid_db_user;' AS sql FROM pg_namespace
    WHERE nspname NOT LIKE 'pg_%'
    AND nspname <> 'information_schema'
    LOOP
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;



DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'frapid_db_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT      'ALTER TYPE ' || n.nspname || '.' || t.typname || ' OWNER TO frapid_db_user;' AS sql
    FROM        pg_type t 
    LEFT JOIN   pg_catalog.pg_namespace n ON n.oid = t.typnamespace 
    WHERE       (t.typrelid = 0 OR (SELECT c.relkind = 'c' FROM pg_catalog.pg_class c WHERE c.oid = t.typrelid)) 
    AND         NOT EXISTS(SELECT 1 FROM pg_catalog.pg_type el WHERE el.oid = t.typelem AND el.typarray = t.oid)
    AND         typtype NOT IN ('b')
    AND         n.nspname NOT IN ('pg_catalog', 'information_schema')
    LOOP
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT * FROM pg_tables 
    WHERE NOT schemaname = ANY(ARRAY['pg_catalog', 'information_schema'])
    AND tableowner <> 'report_user'
    LOOP
        EXECUTE 'GRANT SELECT ON TABLE '|| this.schemaname || '.' || this.tablename ||' TO report_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;

DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT oid::regclass::text as mat_view
    FROM   pg_class
    WHERE  relkind = 'm'
    LOOP
        EXECUTE 'GRANT SELECT ON TABLE '|| this.mat_view  ||' TO report_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;

DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT 'GRANT EXECUTE ON '
        || CASE WHEN p.proisagg THEN 'AGGREGATE ' ELSE 'FUNCTION ' END
        || quote_ident(n.nspname) || '.' || quote_ident(p.proname) || '(' 
        || pg_catalog.pg_get_function_identity_arguments(p.oid) || ') TO report_user;' AS sql
    FROM   pg_catalog.pg_proc p
    JOIN   pg_catalog.pg_namespace n ON n.oid = p.pronamespace
    WHERE  NOT n.nspname = ANY(ARRAY['pg_catalog', 'information_schema'])
    LOOP        
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT * FROM pg_views
    WHERE NOT schemaname = ANY(ARRAY['pg_catalog', 'information_schema'])
    AND viewowner <> 'report_user'
    LOOP
        EXECUTE 'GRANT SELECT ON '|| this.schemaname || '.' || this.viewname ||' TO report_user;';
    END LOOP;
END
$$
LANGUAGE plpgsql;


DO
$$
    DECLARE this record;
BEGIN
    IF(CURRENT_USER = 'report_user') THEN
        RETURN;
    END IF;

    FOR this IN 
    SELECT 'GRANT USAGE ON SCHEMA ' || nspname || ' TO report_user;' AS sql FROM pg_namespace
    WHERE nspname NOT LIKE 'pg_%'
    AND nspname <> 'information_schema'
    LOOP
        EXECUTE this.sql;
    END LOOP;
END
$$
LANGUAGE plpgsql;


