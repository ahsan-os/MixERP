-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
DROP SCHEMA IF EXISTS social CASCADE;
CREATE SCHEMA social;

CREATE TABLE social.feeds
(
    feed_id                         BIGSERIAL PRIMARY KEY,
    event_timestamp                 TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
    formatted_text                  national character varying(4000) NOT NULL,    
    created_by                      integer NOT NULL REFERENCES account.users,
    attachments                     text, --CSV
    /****************************************************************************
    Scope acts like a group.
    For example: Contacts, Sales, Notes, etc.
    *****************************************************************************/
    scope                           national character varying(100),
    is_public                       boolean NOT NULL DEFAULT(true),
    parent_feed_id                  bigint REFERENCES social.feeds,
	url								text,
    audit_ts                        TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
    deleted                         boolean NOT NULL DEFAULT(false),
    deleted_on                      TIMESTAMP WITH TIME ZONE,
	deleted_by						integer REFERENCES account.users
);


CREATE TABLE social.shared_with
(
    feed_id                         bigint NOT NULL REFERENCES social.feeds,
    user_id                         integer REFERENCES account.users,
    role_id                         integer REFERENCES account.roles,
                                    CHECK
                                    (
                                        (user_id IS NOT NULL AND role_id IS NULL)
                                        OR
                                        (user_id IS NULL AND role_id IS NOT NULL)
                                    )
);

CREATE UNIQUE INDEX shared_with_user_id_uix
ON social.shared_with(feed_id, user_id)
WHERE user_id IS NOT NULL;

CREATE UNIQUE INDEX shared_with_role_id_uix
ON social.shared_with(feed_id, role_id)
WHERE role_id IS NOT NULL;

CREATE INDEX feeds_scope_inx
ON social.feeds(LOWER(scope));

CREATE TABLE social.liked_by
(
    feed_id                         bigint NOT NULL REFERENCES social.feeds,
    liked_by                     	integer NOT NULL REFERENCES account.users,
    liked_on                     	TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
    unliked                      	boolean NOT NULL DEFAULT(false),
    unliked_on                   	TIMESTAMP WITH TIME ZONE
);

CREATE UNIQUE INDEX liked_by_uix
ON social.liked_by(feed_id, liked_by)
WHERE NOT unliked;


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/02.functions-and-logic/social.count_comments.sql --<--<--
DROP FUNCTION IF EXISTS social.count_comments(_feed_id bigint);

CREATE FUNCTION social.count_comments(_feed_id bigint)
RETURNS bigint
AS
$$
    DECLARE _count                  bigint;
BEGIN
    WITH RECURSIVE all_comments
    AS
    (
        SELECT social.feeds.feed_id 
        FROM social.feeds 
        WHERE social.feeds.parent_feed_id = _feed_id

        UNION ALL

        SELECT feed_comments.feed_id 
        FROM social.feeds AS feed_comments
        INNER JOIN all_comments
        ON all_comments.feed_id = feed_comments.parent_feed_id
    )
    
    SELECT COUNT(*) INTO _count 
    FROM all_comments;

    RETURN _count;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM social.count_comments(87);


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/02.functions-and-logic/social.get_followers.sql --<--<--
DROP FUNCTION IF EXISTS social.get_followers(_feed_id bigint, _me integer);

CREATE FUNCTION social.get_followers(_feed_id bigint, _me integer)
RETURNS text
AS
$$
    DECLARE _followers              text;
    DECLARE _parent                 bigint;
BEGIN
    _parent := social.get_root_feed_id(_feed_id);
    
    WITH RECURSIVE all_feeds
    AS
    (
        SELECT social.feeds.feed_id 
        FROM social.feeds 
        WHERE social.feeds.feed_id = _parent

        UNION ALL

        SELECT feed_comments.feed_id 
        FROM social.feeds AS feed_comments
        INNER JOIN all_feeds
        ON all_feeds.feed_id = feed_comments.parent_feed_id
    ),
    feeds
    AS
    (
        SELECT 
            all_feeds.feed_id,
            social.feeds.created_by
        FROM social.feeds
        INNER JOIN all_feeds
        ON all_feeds.feed_id = social.feeds.feed_id
    )
    
    SELECT string_agg(DISTINCT feeds.created_by::text, ',')
    INTO _followers
    FROM feeds
    WHERE feeds.created_by != _me;

    RETURN _followers;
END
$$
LANGUAGE plpgsql;

--SELECT social.get_followers(97, 1);


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/02.functions-and-logic/social.get_next_top_feeds.sql --<--<--
DROP FUNCTION IF EXISTS social.get_next_top_feeds(_user_id integer, _last_feed_id bigint, _parent_feed_id bigint, _url text);

CREATE FUNCTION social.get_next_top_feeds(_user_id integer, _last_feed_id bigint, _parent_feed_id bigint, _url text)
RETURNS TABLE
(
    row_number                      bigint,
    feed_id                         bigint,
    event_timestamp                 TIMESTAMP WITH TIME ZONE,
    audit_ts                        TIMESTAMP WITH TIME ZONE,
    formatted_text                  national character varying(4000),    
    created_by                      integer,
    created_by_name                 national character varying(100),
    attachments                     text,
    scope                           national character varying(100),
    is_public                       boolean,
    parent_feed_id                  bigint,
    child_count                     bigint
)
AS
$$
    DECLARE _role_id                integer;
BEGIN

    DROP TABLE IF EXISTS temp_feeds;

    CREATE TEMPORARY TABLE temp_feeds
    (
        feed_id                         bigint,
        event_timestamp                 TIMESTAMP WITH TIME ZONE,
        audit_ts                        TIMESTAMP WITH TIME ZONE,
        formatted_text                  national character varying(4000),    
        created_by                      integer,
        created_by_name                 national character varying(100),
        attachments                     text,
        scope                           national character varying(100),
        is_public                       boolean,
        parent_feed_id                  bigint,
        child_count                     bigint
    );

    SELECT account.users.role_id INTO _role_id
    FROM account.users
    WHERE account.users.user_id = _user_id;

    INSERT INTO temp_feeds
    SELECT
        social.feeds.feed_id,
        social.feeds.event_timestamp,
        social.feeds.audit_ts,
        social.feeds.formatted_text,
        social.feeds.created_by,
        account.get_name_by_user_id(social.feeds.created_by)::national character varying(100),
        social.feeds.attachments,
        social.feeds.scope,
        social.feeds.is_public,
        social.feeds.parent_feed_id        
    FROM social.feeds
    WHERE NOT social.feeds.deleted
    AND (_last_feed_id = 0 OR social.feeds.feed_id < _last_feed_id)
    AND COALESCE(social.feeds.parent_feed_id, 0) = COALESCE(_parent_feed_id, 0)
    AND COALESCE(social.feeds.url, '') = COALESCE(_url, '')
    AND 
    (
        social.feeds.is_public
        OR
        social.feeds.feed_id IN
        (
            SELECT social.shared_with.feed_id
            FROM social.shared_with
            WHERE (_last_feed_id = 0 OR social.shared_with.feed_id < _last_feed_id)
            AND (social.shared_with.role_id = _role_id OR social.shared_with.user_id = _user_id)
        )
    )
    ORDER BY social.feeds.audit_ts DESC
    LIMIT 10;

    WITH all_comments
    AS
    (
        SELECT
            social.feeds.feed_id,
            social.feeds.event_timestamp,
            social.feeds.audit_ts,
            social.feeds.formatted_text,
            social.feeds.created_by,
            account.get_name_by_user_id(social.feeds.created_by)::national character varying(100) AS name,
            social.feeds.attachments,
            social.feeds.scope,
            social.feeds.is_public,
            social.feeds.parent_feed_id,
            ROW_NUMBER() OVER(PARTITION BY social.feeds.parent_feed_id ORDER BY social.feeds.audit_ts DESC) AS row_number
        FROM social.feeds
        WHERE NOT social.feeds.deleted
        AND (_last_feed_id = 0 OR social.feeds.feed_id < _last_feed_id)
        AND social.feeds.parent_feed_id IN
        (
            SELECT temp_feeds.feed_id
            FROM temp_feeds
        )        
        AND 
        (
            social.feeds.is_public
            OR
            social.feeds.feed_id IN
            (
                SELECT social.shared_with.feed_id
                FROM social.shared_with
                WHERE (_last_feed_id = 0 OR social.shared_with.feed_id < _last_feed_id)
                AND (social.shared_with.role_id = _role_id OR social.shared_with.user_id = _user_id)
            )
        )
    )
    INSERT INTO temp_feeds
    SELECT
        all_comments.feed_id,
        all_comments.event_timestamp,
        all_comments.audit_ts,
        all_comments.formatted_text,
        all_comments.created_by,
        all_comments.name,
        all_comments.attachments,
        all_comments.scope,
        all_comments.is_public,
        all_comments.parent_feed_id
    FROM all_comments
    WHERE all_comments.row_number <= 10;

    UPDATE temp_feeds
    SET child_count = social.count_comments(temp_feeds.feed_id);

    RETURN QUERY
    SELECT ROW_NUMBER() OVER(ORDER BY temp_feeds.audit_ts DESC), * FROM temp_feeds
    ORDER BY 1;
END
$$
LANGUAGE plpgsql;

--SELECT * FROM social.get_next_top_feeds(1, 0, 0, '');




-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/02.functions-and-logic/social.get_root_feed_id.sql --<--<--
DROP FUNCTION IF EXISTS social.get_root_feed_id(bigint);

CREATE FUNCTION social.get_root_feed_id(_feed_id bigint)
RETURNS bigint
AS
$$
    DECLARE _parent_feed_id bigint;
BEGIN
    SELECT 
        parent_feed_id
        INTO _parent_feed_id
    FROM social.feeds
    WHERE social.feeds.feed_id=$1
	AND NOT social.feeds.deleted;

    

    IF(_parent_feed_id IS NULL) THEN
        RETURN $1;
    ELSE
        RETURN social.get_root_feed_id(_parent_feed_id);
    END IF; 
END
$$
LANGUAGE plpgsql;

--SELECT social.get_root_feed_id(97)

-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/02.functions-and-logic/social.like.sql --<--<--
DROP FUNCTION IF EXISTS social.like(_user_id integer, _feed_id bigint);

CREATE FUNCTION social.like(_user_id integer, _feed_id bigint)
RETURNS void
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT 0 
        FROM social.liked_by
        WHERE social.liked_by.feed_id = _feed_id
        AND social.liked_by.liked_by = _user_id
    ) THEN
        INSERT INTO social.liked_by(feed_id, liked_by)
        SELECT _feed_id, _user_id;
    END IF;

    IF EXISTS
    (
        SELECT 0 
        FROM social.liked_by
        WHERE social.liked_by.feed_id = _feed_id
        AND social.liked_by.liked_by = _user_id
        AND social.liked_by.unliked
    ) THEN
        UPDATE social.liked_by
        SET
            unliked     = false,
            unliked_on  = NULL
        WHERE social.liked_by.feed_id = _feed_id
        AND social.liked_by.liked_by = _user_id;
    END IF;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/02.functions-and-logic/social.unlike.sql --<--<--
DROP FUNCTION IF EXISTS social.unlike(_user_id integer, _feed_id bigint);

CREATE FUNCTION social.unlike(_user_id integer, _feed_id bigint)
RETURNS void
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT 0 FROM social.liked_by
        WHERE social.liked_by.feed_id = _feed_id
        AND  social.liked_by.liked_by = _user_id
    ) THEN
        UPDATE social.liked_by
        SET 
            unliked     = true,
            unliked_on  = NOW()
        WHERE social.liked_by.feed_id = _feed_id
        AND social.liked_by.liked_by = _user_id;
    END IF;
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/03.menus/menus.sql --<--<--
DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'MixERP.Social'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'MixERP.Social'
);

DELETE FROM core.menus
WHERE app_name = 'MixERP.Social';


SELECT * FROM core.create_app('MixERP.Social', 'Social', 'Social', '1.0', 'MixERP Inc.', 'December 1, 2015', 'orange users', '/dashboard/social', NULL);

SELECT * FROM core.create_menu('MixERP.Social', 'Tasks', 'Tasks', '', 'lightning', '');
SELECT * FROM core.create_menu( 'MixERP.Social', 'Social', 'Social', '/dashboard/social', 'users', 'Tasks');





SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'MixERP.Social',
    '{*}'::text[]
);


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/04.default-values/01.default-values.sql --<--<--


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/05.triggers/social.update_audit_timestamp_trigger.sql --<--<--
DROP FUNCTION IF EXISTS social.update_audit_timestamp_trigger() CASCADE;

CREATE FUNCTION social.update_audit_timestamp_trigger()
RETURNS TRIGGER
AS
$$
BEGIN
    WITH RECURSIVE ancestors
    AS
    (
        SELECT parent_feed_id from social.feeds where feed_id  = NEW.feed_id
        UNION ALL
        SELECT feeds.parent_feed_id 
        FROM social.feeds AS feeds
        INNER JOIN ancestors 
        ON ancestors.parent_feed_id =feeds.feed_id
        WHERE feeds.parent_feed_id IS NOT NULL
    )

    UPDATE social.feeds
    SET audit_ts = NOW()
    WHERE social.feeds.feed_id IN
    (
        SELECT * FROM ancestors
    );

    RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER update_audit_timestamp_trigger
AFTER INSERT
ON social.feeds
FOR EACH ROW EXECUTE PROCEDURE social.update_audit_timestamp_trigger();


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/05.views/social.liked_by_view.sql --<--<--
DROP VIEW IF EXISTS social.liked_by_view;

CREATE VIEW social.liked_by_view
AS
SELECT
    social.liked_by.feed_id,
    social.liked_by.liked_by,
    account.get_name_by_user_id(social.liked_by.liked_by) AS liked_by_name,
    social.liked_by.liked_on
FROM social.liked_by
WHERE NOT social.liked_by.unliked;


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/PostgreSQL/2.x/2.0/src/99.ownership.sql --<--<--
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


