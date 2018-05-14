-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
DROP SCHEMA IF EXISTS website CASCADE; --WEB BUILDER
CREATE SCHEMA website;

CREATE TABLE website.configurations
(
    configuration_id                            SERIAL PRIMARY KEY,
    domain_name                                 national character varying(500) NOT NULL,
    website_name                                national character varying(500) NOT NULL,
	description									text,
	blog_title                                  national character varying(500),
	blog_description							text,	
	is_default                                  boolean NOT NULL DEFAULT(true),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE UNIQUE INDEX configuration_domain_name_uix
ON website.configurations(LOWER(domain_name))
WHERE NOT deleted;

CREATE TABLE website.email_subscriptions
(
    email_subscription_id                       uuid PRIMARY KEY DEFAULT(gen_random_uuid()),
	first_name									national character varying(100),
	last_name									national character varying(100),
    email                                       national character varying(100) NOT NULL UNIQUE,
    browser                                     text,
    ip_address                                  national character varying(50),
	confirmed									boolean DEFAULT(false),
    confirmed_on                               	TIMESTAMP WITH TIME ZONE,
    unsubscribed                                boolean DEFAULT(false),
    subscribed_on                               TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),    
    unsubscribed_on                             TIMESTAMP WITH TIME ZONE,
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE TABLE website.categories
(
    category_id                                 SERIAL NOT NULL PRIMARY KEY,
    category_name                               national character varying(250) NOT NULL,
    alias                                       national character varying(250) NOT NULL UNIQUE,
    seo_description                             national character varying(100),
	is_blog										boolean NOT NULL DEFAULT(false),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)    
);

CREATE TABLE website.contents
(
    content_id                                  SERIAL NOT NULL PRIMARY KEY,
    category_id                                 integer NOT NULL REFERENCES website.categories,
    title                                       national character varying(500) NOT NULL,
    alias                                       national character varying(500) NOT NULL UNIQUE,
    author_id                                   integer REFERENCES account.users,
    publish_on                                  TIMESTAMP WITH TIME ZONE NOT NULL,
	created_on									TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT(NOW()),
    last_editor_id                              integer REFERENCES account.users,
	last_edited_on							    TIMESTAMP WITH TIME ZONE,
    markdown                                    text,
    contents                                    text NOT NULL,
    tags                                        text,
	hits										bigint,
    is_draft                                    boolean NOT NULL DEFAULT(true),
    seo_description                             national character varying(1000) NOT NULL DEFAULT(''),
    is_homepage                                 boolean NOT NULL DEFAULT(false),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)    
);

CREATE TABLE website.menus
(
    menu_id                                     SERIAL PRIMARY KEY,
    menu_name                                   national character varying(100),
    description                                 text,
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)
);

CREATE UNIQUE INDEX menus_menu_name_uix
ON website.menus(UPPER(menu_name))
WHERE NOT deleted;

CREATE TABLE website.menu_items
(
    menu_item_id                                SERIAL PRIMARY KEY,
    menu_id                                     integer REFERENCES website.menus,
    sort                                        integer NOT NULL DEFAULT(0),
    title                                       national character varying(100) NOT NULL,
    url                                         national character varying(500),
    target                                      national character varying(20),
    content_id                                  integer REFERENCES website.contents,    
	parent_menu_item_id							integer REFERENCES website.menu_items,
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)    
);


CREATE TABLE website.contacts
(
    contact_id                                  SERIAL PRIMARY KEY,
    title                                       national character varying(500) NOT NULL,
    name                                        national character varying(500) NOT NULL,
    position                                    national character varying(500),
    address                                     national character varying(500),
    city                                        national character varying(500),
    state                                       national character varying(500),
    country                                     national character varying(100),
    postal_code                                 national character varying(500),
    telephone                                   national character varying(500),
    details                                     text,
    email                                       national character varying(500),
    recipients                                  national character varying(1000),
    display_email                               boolean NOT NULL DEFAULT(false),
    display_contact_form                        boolean NOT NULL DEFAULT(true),
    sort                                        integer NOT NULL DEFAULT(0),
    status                                      boolean NOT NULL DEFAULT(true),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted										boolean DEFAULT(false)    
);

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/website.add_email_subscription.sql --<--<--
DROP FUNCTION IF EXISTS website.add_email_subscription
(
    _email                                  text
);


CREATE FUNCTION website.add_email_subscription
(
    _email                                  text
)
RETURNS boolean
AS
$$
BEGIN
    IF NOT EXISTS
    (
        SELECT * 
		FROM website.email_subscriptions
        WHERE website.email_subscriptions.email = _email
		AND NOT website.email_subscriptions.deleted
    ) THEN
        INSERT INTO website.email_subscriptions(email)
        SELECT _email;
        
        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/website.add_hit.sql --<--<--
DROP FUNCTION IF EXISTS website.add_hit(_category_alias national character varying(250), _alias national character varying(500));

CREATE FUNCTION website.add_hit(_category_alias national character varying(250), _alias national character varying(500))
RETURNS void
AS
$$
BEGIN
	IF(COALESCE(_alias, '') = '' AND COALESCE(_category_alias, '') = '') THEN
		UPDATE website.contents SET hits = COALESCE(website.contents.hits, 0) + 1 
		WHERE is_homepage;

		RETURN;
	END IF;

	UPDATE website.contents SET hits = COALESCE(website.contents.hits, 0) + 1 
	WHERE website.contents.content_id
	=
	(
		SELECT website.published_content_view.content_id 
		FROM website.published_content_view
		WHERE category_alias=_category_alias AND alias=_alias
	);
END
$$
LANGUAGE plpgsql;


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/website.get_category_id_by_category_name.sql --<--<--
DROP FUNCTION IF EXISTS website.get_category_id_by_category_name(_category_name text);

CREATE FUNCTION website.get_category_id_by_category_name(_category_name text)
RETURNS integer
AS
$$
BEGIN
    RETURN category_id
    FROM website.categories
    WHERE website.categories.category_name = _category_name
	AND NOT website.categories.deleted;
END
$$
LANGUAGE plpgsql;

DROP FUNCTION IF EXISTS website.get_category_id_by_category_alias(_alias text);

CREATE FUNCTION website.get_category_id_by_category_alias(_alias text)
RETURNS integer
AS
$$
BEGIN
    RETURN category_id
    FROM website.categories
    WHERE website.categories.alias = _alias
	AND NOT website.categories.deleted;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/website.get_menu_id_by_menu_name.sql --<--<--
DROP FUNCTION IF EXISTS website.get_menu_id_by_menu_name(_menu_name national character varying(500));

CREATE FUNCTION website.get_menu_id_by_menu_name(_menu_name national character varying(500))
RETURNS integer
AS
$$
BEGIN
    RETURN
    (
		SELECT menu_id
		FROM website.menus
		WHERE menu_name = _menu_name
		AND NOT website.menus.deleted
	);
END
$$
LANGUAGE plpgsql;

--SELECT * FROM website.get_menu_id_by_menu_name('Default');



-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/02.functions-and-logic/website.remove_email_subscription.sql --<--<--
DROP FUNCTION IF EXISTS website.remove_email_subscription
(
    _email                                  text
);

CREATE FUNCTION website.remove_email_subscription
(
    _email                                  text
)
RETURNS boolean
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT * FROM website.email_subscriptions
        WHERE email = _email
        AND NOT unsubscribed
    ) THEN
        UPDATE website.email_subscriptions
        SET
            unsubscribed = true,
            unsubscribed_on = NOW()
        WHERE email = _email;

        RETURN true;
    END IF;

    RETURN false;
END
$$
LANGUAGE plpgsql;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/03.menus/menus.sql --<--<--
SELECT * FROM core.create_app('Frapid.WebsiteBuilder', 'Website', 'Website', '1.0', 'MixERP Inc.', 'December 1, 2015', 'world blue', '/dashboard/website/contents', null);

SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Tasks', 'Tasks', '', 'tasks icon', '');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Configuration', 'Configuration', '/dashboard/website/configuration', 'configure icon', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'ManageCategories', 'Manage Categories', '/dashboard/website/categories', 'sitemap icon', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'AddNewContent', 'Add a New Content', '/dashboard/website/contents/new', 'file', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'ViewContents', 'View Contents', '/dashboard/website/contents', 'desktop', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'AddNewBlogPost', 'Add a New Blog Post', '/dashboard/website/blogs/new', 'write', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'ViewBlogPosts', 'View Blog Posts', '/dashboard/website/blogs', 'browser', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Menus', 'Menus', '/dashboard/website/menus', 'star', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Contacts', 'Contacts', '/dashboard/website/contacts', 'phone', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'Subscriptions', 'Subscriptions', '/dashboard/website/subscriptions', 'newspaper', 'Tasks');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'LayoutManager', 'Layout Manager', '/dashboard/website/layouts', 'grid layout', '');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'EmailTemplates', 'Email Templates', '', 'mail', '');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'SubscriptionAdded', 'Subscription Added', '/dashboard/website/subscription/welcome', 'plus circle', 'Email Templates');
SELECT * FROM core.create_menu('Frapid.WebsiteBuilder', 'SubscriptionRemoved', 'Subscription Removed', '/dashboard/website/subscription/removed', 'minus circle', 'Email Templates');


SELECT * FROM auth.create_app_menu_policy
(
    'Content Editor', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.WebsiteBuilder',
    '{Tasks, Manage Categories, Add New Content, View Contents}'::text[]
);

SELECT * FROM auth.create_app_menu_policy
(
    'User', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.WebsiteBuilder',
    '{
        Tasks, Manage Categories, Add New Content, View Contents, 
        Menus, Contacts, Subscriptions, 
        Layout Manager, Edit Master Layout (Homepage), Edit Master Layout, Edit Header, Edit Footer, 404 Not Found Document
    }'::text[]
);


SELECT * FROM auth.create_app_menu_policy
(
    'Admin', 
    core.get_office_id_by_office_name('Default'), 
    'Frapid.WebsiteBuilder',
    '{*}'::text[]
);


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.scrud-views/website.blog_scrud_view.sql --<--<--
DROP VIEW IF EXISTS website.blog_scrud_view;

CREATE VIEW website.blog_scrud_view
AS
SELECT
	website.contents.content_id AS blog_id,
	website.contents.title,
	website.categories.category_name,
	website.contents.alias,
	website.contents.is_draft,
	website.contents.publish_on
FROM website.contents
INNER JOIN website.categories
ON website.categories.category_id = website.contents.category_id
WHERE NOT website.contents.deleted
AND website.categories.is_blog;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.scrud-views/website.contact_scrud_view.sql --<--<--
DROP VIEW IF EXISTS website.contact_scrud_view;

CREATE VIEW website.contact_scrud_view
AS
SELECT
	website.contacts.contact_id,
	website.contacts.title,
	website.contacts.name,
	website.contacts.position,
	website.contacts.email,
	website.contacts.display_contact_form,
	website.contacts.display_email
FROM website.contacts
WHERE NOT website.contacts.deleted;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.scrud-views/website.content_scrud_view.sql --<--<--
DROP VIEW IF EXISTS website.content_scrud_view;

CREATE VIEW website.content_scrud_view
AS
SELECT
	website.contents.content_id,
	website.contents.title,
	website.categories.category_name,
	website.contents.alias,
	website.contents.is_draft,
	website.contents.publish_on
FROM website.contents
INNER JOIN website.categories
ON website.categories.category_id = website.contents.category_id
WHERE NOT website.contents.deleted
AND NOT website.categories.is_blog;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.scrud-views/website.email_subscription_scrud_view.sql --<--<--
DROP VIEW IF EXISTS website.email_subscription_scrud_view;

CREATE VIEW website.email_subscription_scrud_view
AS
SELECT
    website.email_subscriptions.email_subscription_id,
    website.email_subscriptions.first_name,
    website.email_subscriptions.last_name,
    website.email_subscriptions.email,
    website.email_subscriptions.confirmed,
    website.email_subscriptions.confirmed_on,
    website.email_subscriptions.unsubscribed,
    website.email_subscriptions.unsubscribed_on
FROM website.email_subscriptions
WHERE NOT website.email_subscriptions.deleted;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.triggers/website.email_subscription_confirmation_trigger.sql --<--<--
DROP FUNCTION IF EXISTS website.email_subscription_confirmation_trigger() CASCADE;

CREATE FUNCTION website.email_subscription_confirmation_trigger()
RETURNS TRIGGER
AS
$$
BEGIN
    IF(NEW.confirmed) THEN
        NEW.confirmed_on = NOW();
    END IF;
    
    RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER email_subscription_confirmation_trigger 
BEFORE UPDATE ON website.email_subscriptions 
FOR EACH ROW
EXECUTE PROCEDURE website.email_subscription_confirmation_trigger();

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.views/website.blog_category_view.sql --<--<--
DROP VIEW IF EXISTS website.blog_category_view;

CREATE VIEW website.blog_category_view
AS
SELECT
    website.categories.category_id          AS blog_category_id,
    website.categories.category_name        AS blog_category_name
FROM website.categories
WHERE NOT website.categories.deleted
AND website.categories.is_blog;



-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.views/website.email_subscription_insert_view.sql --<--<--
DROP VIEW IF EXISTS website.email_subscription_insert_view;

CREATE VIEW website.email_subscription_insert_view
AS
SELECT * 
FROM website.email_subscriptions
WHERE 1 = 0
AND NOT website.email_subscriptions.deleted;


SELECT * 
FROM website.email_subscription_insert_view
WHERE NOT website.email_subscription_insert_view.deleted;

CREATE RULE log_subscriptions AS 
ON INSERT TO website.email_subscription_insert_view
DO INSTEAD 
INSERT INTO website.email_subscriptions
(
    email, 
    browser, 
    ip_address, 
    unsubscribed, 
    subscribed_on, 
    unsubscribed_on, 
    first_name, 
    last_name, 
    confirmed
)
SELECT
    NEW.email, 
    NEW.browser, 
    NEW.ip_address, 
    NEW.unsubscribed, 
    COALESCE(NEW.subscribed_on, NOW()), 
    NEW.unsubscribed_on, 
    NEW.first_name, 
    NEW.last_name, 
    NEW.confirmed
WHERE NOT EXISTS
(
    SELECT 1 
    FROM website.email_subscriptions
    WHERE website.email_subscriptions.email = NEW.email
	AND NOT website.email_subscriptions.deleted
);



-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.views/website.menu_item_view.sql --<--<--
DROP VIEW IF EXISTS website.menu_item_view;

CREATE VIEW website.menu_item_view
AS
SELECT
    website.menus.menu_id,
    website.menus.menu_name,
    website.menus.description,
    website.menu_items.menu_item_id,
    website.menu_items.sort,
    website.menu_items.title,
    website.menu_items.url,
    website.menu_items.target,
    website.menu_items.content_id,
    website.contents.alias AS content_alias
FROM website.menu_items
INNER JOIN website.menus
ON website.menus.menu_id = website.menu_items.menu_id
LEFT JOIN website.contents
ON website.contents.content_id = website.menu_items.content_id
WHERE NOT website.menu_items.deleted;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.views/website.published_content_view.sql --<--<--
DROP VIEW IF EXISTS website.published_content_view;

CREATE VIEW website.published_content_view
AS
SELECT
    website.contents.content_id,
    website.contents.category_id,
    website.categories.category_name,
    website.categories.alias AS category_alias,
    website.contents.title,
    website.contents.alias,
    website.contents.author_id,
    account.users.name AS author_name,
    website.contents.markdown,
    website.contents.publish_on,
    CASE WHEN website.contents.last_edited_on IS NULL THEN website.contents.publish_on ELSE website.contents.last_edited_on END AS last_edited_on,
    website.contents.contents,
    website.contents.tags,
    website.contents.seo_description,
    website.contents.is_homepage,
    website.categories.is_blog
FROM website.contents
INNER JOIN website.categories
ON website.categories.category_id = website.contents.category_id
LEFT JOIN account.users
ON website.contents.author_id = account.users.user_id
WHERE NOT is_draft
AND publish_on <= NOW()
AND NOT website.contents.deleted;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.views/website.tag_view.sql --<--<--
DROP VIEW IF EXISTS website.tag_view;

CREATE VIEW website.tag_view
AS
WITH tags
AS
(
	SELECT DISTINCT unnest(regexp_split_to_array(tags, ',')) AS tag 
	FROM website.contents
	WHERE NOT website.contents.deleted
)
SELECT
    ROW_NUMBER() OVER (ORDER BY tag) AS tag_id,
    tag
FROM tags;


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.views/website.website_category_view.sql --<--<--
DROP VIEW IF EXISTS website.website_category_view;

CREATE VIEW website.website_category_view
AS
SELECT
    website.categories.category_id          AS website_category_id,
    website.categories.category_name        AS website_category_name
FROM website.categories
WHERE NOT website.categories.deleted
AND NOT website.categories.is_blog;


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/05.views/website.yesterdays_email_subscriptions.sql --<--<--
DROP VIEW IF EXISTS website.yesterdays_email_subscriptions;

CREATE VIEW website.yesterdays_email_subscriptions
AS
SELECT
    website.email_subscriptions.email,
    website.email_subscriptions.first_name,
    website.email_subscriptions.last_name,
    'subscribed' AS subscription_type
FROM website.email_subscriptions
WHERE website.email_subscriptions.subscribed_on::date = 'yesterday'::date
AND NOT website.email_subscriptions.confirmed_on::date = 'yesterday'::date
AND NOT website.email_subscriptions.deleted
UNION ALL
SELECT
    website.email_subscriptions.email,
    website.email_subscriptions.first_name,
    website.email_subscriptions.last_name,
    'unsubscribed'
FROM website.email_subscriptions
WHERE website.email_subscriptions.unsubscribed_on::date = 'yesterday'::date
AND NOT website.email_subscriptions.deleted
UNION ALL
SELECT
    website.email_subscriptions.email,
    website.email_subscriptions.first_name,
    website.email_subscriptions.last_name,
    'confirmed'
FROM website.email_subscriptions
WHERE website.email_subscriptions.confirmed_on::date = 'yesterday'::date
AND NOT website.email_subscriptions.deleted;

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/10.policy/access_policy.sql --<--<--
SELECT * FROM auth.create_api_access_policy('{Content Editor, User, Admin}', core.get_office_id_by_office_name('Default'), 'website.categories', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{Content Editor, User, Admin}', core.get_office_id_by_office_name('Default'), 'website.contents', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{User, Admin}', core.get_office_id_by_office_name('Default'), 'website.menus', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{User, Admin}', core.get_office_id_by_office_name('Default'), 'website.email_subscriptions', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{User, Admin}', core.get_office_id_by_office_name('Default'), 'website.contacts', '{*}', true);
SELECT * FROM auth.create_api_access_policy('{Admin}', core.get_office_id_by_office_name('Default'), 'website.configurations', '{*}', true);


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src/99.ownership.sql --<--<--
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


