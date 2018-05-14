-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/PostgreSQL/2.x/2.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
DROP SCHEMA IF EXISTS addressbook CASCADE;
CREATE SCHEMA addressbook;

CREATE TABLE addressbook.contacts
(
    contact_id                  uuid PRIMARY KEY DEFAULT(gen_random_uuid()),
    associated_user_id          integer REFERENCES account.users,
    first_name                  national character varying(200),
    prefix                      national character varying(200),
    middle_name                 national character varying(200),
    last_name                   national character varying(200),
    suffix                      national character varying(200),
    formatted_name              national character varying(610) NOT NULL,
    nick_name                   national character varying(610),
    email_addresses             national character varying(1000),
    telephones                  national character varying(1000),
    fax_numbers                 national character varying(1000),
    mobile_numbers              national character varying(1000),
    url                         national character varying(1000),
    kind                        integer,
    gender                      integer,
    language                    national character varying(500),
    time_zone                   national character varying(500),
    birth_day                   date,
    address_line_1              national character varying(500),
    address_line_2              national character varying(500),
    postal_code                 national character varying(500),
    street                      national character varying(500),
    city                        national character varying(500),
    state                       national character varying(500),
    country                     national character varying(500),
    organization                national character varying(500),
    organizational_unit         national character varying(500),
    title                       national character varying(500),
    role                        national character varying(500),
    note                        text,
    is_private                  boolean NOT NULL DEFAULT(true),
    tags                        national character varying(500),
    created_by                  integer REFERENCES account.users,
    audit_user_id               integer REFERENCES account.users,
    audit_ts                    TIMESTAMP WITH TIME ZONE NULL DEFAULT(NOW()),
    deleted                     boolean DEFAULT(false)    
);


-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/PostgreSQL/2.x/2.0/src/02.functions-and-logic/addressbook.get_tags.sql --<--<--
DROP FUNCTION IF EXISTS addressbook.get_tags(_user_id integer);

CREATE FUNCTION addressbook.get_tags(_user_id integer)
RETURNS text
AS
$$
BEGIN
    RETURN string_agg(tag, ',')
    FROM 
    (
        SELECT DISTINCT unnest(string_to_array(tags, ',')) AS tag
        FROM addressbook.contacts
        WHERE NOT addressbook.contacts.deleted
        AND (addressbook.contacts.is_private = false OR addressbook.contacts.created_by = _user_id)
    ) AS tags;
END
$$
LANGUAGE plpgsql;

SELECT * FROM addressbook.get_tags(1);

-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/PostgreSQL/2.x/2.0/src/03.menus/menus.sql --<--<--
DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Frapid.AddressBook'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'Frapid.AddressBook'
);

DELETE FROM core.menus
WHERE app_name = 'Frapid.AddressBook';


SELECT * FROM core.create_app('Frapid.AddressBook', 'AddressBook', 'AddressBook', '1.0', 'MixERP Inc.', 'December 1, 2015', 'teal phone', '/dashboard/address-book', NULL);

SELECT * FROM core.create_menu('Frapid.AddressBook', 'Tasks', 'Tasks', '', 'lightning', '');
SELECT * FROM core.create_menu('Frapid.AddressBook', 'AddressBook', 'AddressBook', '/dashboard/address-book', 'phone', 'Tasks');


SELECT * FROM auth.create_app_menu_policy
(
	'Admin', 
	core.get_office_id_by_office_name('Default'), 
	'Frapid.AddressBook',
	'{*}'
);


-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/PostgreSQL/2.x/2.0/src/04.default-values/01.default-values.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/PostgreSQL/2.x/2.0/src/99.ownership.sql --<--<--
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


