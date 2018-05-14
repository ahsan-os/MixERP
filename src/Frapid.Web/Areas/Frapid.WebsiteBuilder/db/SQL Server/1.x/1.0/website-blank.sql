-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--

EXECUTE dbo.drop_schema 'website';
GO
CREATE SCHEMA website;
GO

CREATE TABLE website.configurations
(
    configuration_id                                integer IDENTITY PRIMARY KEY,
    domain_name                                     national character varying(500) NOT NULL,
    website_name                                    national character varying(500) NOT NULL,
	description										national character varying(500),
	blog_title                                      national character varying(500),
	blog_description							    national character varying(500),	
	is_default                                      bit NOT NULL DEFAULT(1),
    audit_user_id                                   integer REFERENCES account.users,
    audit_ts                                		DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted											bit DEFAULT(0)
);

CREATE UNIQUE INDEX configuration_domain_name_uix
ON website.configurations(domain_name)
WHERE deleted = 0;

CREATE TABLE website.email_subscriptions
(
    email_subscription_id                       uniqueidentifier PRIMARY KEY DEFAULT(newid()),
	first_name									national character varying(100),
	last_name									national character varying(100),
    email                                       national character varying(100) NOT NULL UNIQUE,
    browser                                     national character varying(500),
    ip_address                                  national character varying(50),
	confirmed									bit DEFAULT(0),
    confirmed_on                               	datetimeoffset,
    unsubscribed                                bit DEFAULT(0),
    subscribed_on                               datetimeoffset DEFAULT(getutcdate()),    
    unsubscribed_on                             datetimeoffset,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE TABLE website.categories
(
    category_id                                 integer IDENTITY NOT NULL PRIMARY KEY,
    category_name                               national character varying(250) NOT NULL,
    alias                                       national character varying(250) NOT NULL UNIQUE,
    seo_description                             national character varying(100),
	is_blog										bit NOT NULL DEFAULT(0),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)    
);

CREATE TABLE website.contents
(
    content_id                                  integer IDENTITY NOT NULL PRIMARY KEY,
    category_id                                 integer NOT NULL REFERENCES website.categories,
    title                                       national character varying(500) NOT NULL,
    alias                                       national character varying(500) NOT NULL UNIQUE,
    author_id                                   integer REFERENCES account.users,
    publish_on                                  datetimeoffset NOT NULL,
	created_on									datetimeoffset NOT NULL DEFAULT(getutcdate()),
    last_editor_id                              integer REFERENCES account.users,
	last_edited_on							    datetimeoffset,
    markdown                                    national character varying(MAX),
    contents                                    national character varying(MAX) NOT NULL,
    tags                                        national character varying(500),
	hits										bigint,
    is_draft                                    bit NOT NULL DEFAULT(1),
    seo_description                             national character varying(1000) NOT NULL DEFAULT(''),
    is_homepage                                 bit NOT NULL DEFAULT(0),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)    
);

CREATE TABLE website.menus
(
    menu_id                                     integer IDENTITY PRIMARY KEY,
    menu_name                                   national character varying(100),
    description                                 national character varying(500),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE UNIQUE INDEX menus_menu_name_uix
ON website.menus(menu_name)
WHERE deleted = 0;

CREATE TABLE website.menu_items
(
    menu_item_id                                integer IDENTITY PRIMARY KEY,
    menu_id                                     integer REFERENCES website.menus,
    sort                                        integer NOT NULL DEFAULT(0),
    title                                       national character varying(100) NOT NULL,
    url                                         national character varying(500),
    target                                      national character varying(20),
    content_id                                  integer REFERENCES website.contents,
	parent_menu_item_id							integer REFERENCES website.menu_items,
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0));


CREATE TABLE website.contacts
(
    contact_id                                  integer IDENTITY PRIMARY KEY,
    title                                       national character varying(500) NOT NULL,
    name                                        national character varying(500) NOT NULL,
    position                                    national character varying(500),
    address                                     national character varying(500),
    city                                        national character varying(500),
    state                                       national character varying(500),
    country                                     national character varying(100),
    postal_code                                 national character varying(500),
    telephone                                   national character varying(500),
    details                                     national character varying(500),
    email                                       national character varying(500),
    recipients                                  national character varying(1000),
    display_email                               bit NOT NULL DEFAULT(0),
    display_contact_form                        bit NOT NULL DEFAULT(1),
    sort                                        integer NOT NULL DEFAULT(0),
    status                                      bit NOT NULL DEFAULT(1),
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)    
);

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/02.functions-and-logic/website.add_email_subscription.sql --<--<--
IF OBJECT_ID('website.add_email_subscription') IS NOT NULL
DROP PROCEDURE website.add_email_subscription;

GO

CREATE PROCEDURE website.add_email_subscription
(
    @email                                  national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NOT EXISTS
    (
        SELECT * FROM website.email_subscriptions
        WHERE email = @email
		AND website.email_subscriptions.deleted = 0
    )
    BEGIN
        INSERT INTO website.email_subscriptions(email)
        SELECT @email;
        
        RETURN 1;
    END;

    RETURN 0;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/02.functions-and-logic/website.add_hit.sql --<--<--
IF OBJECT_ID('website.add_hit') IS NOT NULL
DROP PROCEDURE website.add_hit;

GO

CREATE PROCEDURE website.add_hit(@category_alias national character varying(250), @alias national character varying(500))
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	IF(COALESCE(@alias, '') = '' AND COALESCE(@category_alias, '') = '')
	BEGIN
		UPDATE website.contents SET hits = COALESCE(website.contents.hits, 0) + 1 
		WHERE is_homepage = 1;

		RETURN;
	END;

	UPDATE website.contents SET hits = COALESCE(website.contents.hits, 0) + 1 
	WHERE website.contents.content_id =
	(
		SELECT website.published_content_view.content_id 
		FROM website.published_content_view
		WHERE category_alias=@category_alias AND alias=@alias
	)
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/02.functions-and-logic/website.get_category_id_by_category_name.sql --<--<--
IF OBJECT_ID('website.get_category_id_by_category_name') IS NOT NULL
DROP FUNCTION website.get_category_id_by_category_name;

GO

CREATE FUNCTION website.get_category_id_by_category_name(@category_name national character varying(500))
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT category_id
		FROM website.categories
		WHERE category_name = @category_name
		AND website.categories.deleted = 0
	);
END;

GO

IF OBJECT_ID('website.get_category_id_by_category_alias') IS NOT NULL
DROP FUNCTION website.get_category_id_by_category_alias;

GO

CREATE FUNCTION website.get_category_id_by_category_alias(@alias national character varying(500))
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT category_id
		FROM website.categories
		WHERE alias = @alias
		AND website.categories.deleted = 0
	);
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/02.functions-and-logic/website.get_menu_id_by_menu_name.sql --<--<--
IF OBJECT_ID('website.get_menu_id_by_menu_name') IS NOT NULL
DROP FUNCTION website.get_menu_id_by_menu_name;

GO

CREATE FUNCTION website.get_menu_id_by_menu_name(@menu_name national character varying(500))
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT menu_id
		FROM website.menus
		WHERE menu_name = @menu_name
		AND website.menus.deleted = 0
	);
END;

GO

--SELECT website.get_menu_id_by_menu_name('Default');


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/02.functions-and-logic/website.remove_email_subscription.sql --<--<--
IF OBJECT_ID('website.remove_email_subscription') IS NOT NULL
DROP PROCEDURE website.remove_email_subscription;

GO


CREATE PROCEDURE website.remove_email_subscription
(
    @email                                  national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF EXISTS
    (
        SELECT * FROM website.email_subscriptions
        WHERE email = @email
        AND unsubscribed = 0
		AND website.email_subscriptions.deleted = 0
    ) 
    BEGIN
        UPDATE website.email_subscriptions
        SET
            unsubscribed = 1,
            unsubscribed_on = getutcdate()
        WHERE email = @email;

        RETURN 1;
    END;

    RETURN 0;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/03.menus/menus.sql --<--<--
EXECUTE core.create_app 'Frapid.WebsiteBuilder', 'Website', 'Website', '1.0', 'MixERP Inc.', 'December 1, 2015', 'world blue', '/dashboard/website/contents', null;

EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Tasks', 'Tasks', '', 'tasks icon', '';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Configuration', 'Configuration', '/dashboard/website/configuration', 'configure icon', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'ManageCategories', 'Manage Categories', '/dashboard/website/categories', 'sitemap icon', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'AddNewContent', 'Add a New Content', '/dashboard/website/contents/new', 'file', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'ViewContents', 'View Contents', '/dashboard/website/contents', 'desktop', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'AddNewBlogPost', 'Add a New Blog Post', '/dashboard/website/blogs/new', 'write', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'ViewBlogPosts', 'View Blog Posts', '/dashboard/website/blogs', 'browser', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Menus', 'Menus', '/dashboard/website/menus', 'star', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Contacts', 'Contacts', '/dashboard/website/contacts', 'phone', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'Subscriptions', 'Subscriptions', '/dashboard/website/subscriptions', 'newspaper', 'Tasks';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'LayoutManager', 'Layout Manager', '/dashboard/website/layouts', 'grid layout', '';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'EmailTemplates', 'Email Templates', '', 'mail', '';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'SubscriptionAdded', 'Subscription Added', '/dashboard/website/subscription/welcome', 'plus circle', 'Email Templates';
EXECUTE core.create_menu 'Frapid.WebsiteBuilder', 'SubscriptionRemoved', 'Subscription Removed', '/dashboard/website/subscription/removed', 'minus circle', 'Email Templates';

GO

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_app_menu_policy
'Content Editor', 
@office_id, 
'Frapid.WebsiteBuilder',
'{Tasks, Manage Categories, Add New Content, View Contents}';

EXECUTE auth.create_app_menu_policy
'User', 
@office_id, 
'Frapid.WebsiteBuilder',
'{
    Tasks, Manage Categories, Add New Content, View Contents, 
    Menus, Contacts, Subscriptions, 
    Layout Manager, Edit Master Layout (Homepage), Edit Master Layout, Edit Header, Edit Footer, 404 Not Found Document
}';


EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.WebsiteBuilder',
'{*}';



GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.scrud-views/website.blog_scrud_view.sql --<--<--
IF OBJECT_ID('website.blog_scrud_view') IS NOT NULL
DROP VIEW website.blog_scrud_view;

GO
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
WHERE website.contents.deleted = 0
AND website.categories.is_blog = 1;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.scrud-views/website.contact_scrud_view.sql --<--<--
IF OBJECT_ID('website.contact_scrud_view') IS NOT NULL
DROP VIEW website.contact_scrud_view;

GO

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
WHERE website.contacts.deleted = 0;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.scrud-views/website.content_scrud_view.sql --<--<--
IF OBJECT_ID('website.content_scrud_view') IS NOT NULL
DROP VIEW website.content_scrud_view;

GO
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
WHERE website.contents.deleted = 0
AND website.categories.is_blog = 0;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.scrud-views/website.email_subscription_scrud_view.sql --<--<--
IF OBJECT_ID('website.email_subscription_scrud_view') IS NOT NULL
DROP VIEW website.email_subscription_scrud_view;

GO

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
WHERE website.email_subscriptions.deleted = 0;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.triggers/website.email_subscription_confirmation_trigger.sql --<--<--
IF OBJECT_ID('website.email_subscription_confirmation_trigger') IS NOT NULL
DROP TRIGGER website.email_subscription_confirmation_trigger;

GO

CREATE TRIGGER website.email_subscription_confirmation_trigger
ON website.email_subscriptions 
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
    SET XACT_ABORT ON;
	
	IF @@NESTLEVEL > 1
	BEGIN
		RETURN;
	END;
	
	DECLARE @inserted TABLE
	(
		email_subscription_id				uniqueidentifier,
		confirmed							bit
	);


	INSERT INTO @inserted
	SELECT email_subscription_id, confirmed
	FROM INSERTED
	WHERE confirmed = 1;
	
	UPDATE website.email_subscriptions
	SET confirmed_on = getutcdate()
	WHERE email_subscription_id IN
	(
		SELECT email_subscription_id
		FROM @inserted
	);    
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.views/website.blog_category_view.sql --<--<--
IF OBJECT_ID('website.blog_category_view') IS NOT NULL
DROP VIEW website.blog_category_view;

GO

CREATE VIEW website.blog_category_view
AS
SELECT
    website.categories.category_id          AS blog_category_id,
    website.categories.category_name        AS blog_category_name
FROM website.categories
WHERE website.categories.deleted = 0
AND website.categories.is_blog = 1;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.views/website.email_subscription_insert_view.sql --<--<--
IF OBJECT_ID('website.email_subscription_insert_view') IS NOT NULL
DROP VIEW website.email_subscription_insert_view;
GO

CREATE VIEW website.email_subscription_insert_view
AS
SELECT * FROM website.email_subscriptions
WHERE 1 = 0;

GO

CREATE TRIGGER log_subscriptions 
ON website.email_subscription_insert_view
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;
    SET XACT_ABORT ON;
	
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
		email, 
		browser, 
		ip_address, 
		unsubscribed, 
		COALESCE(subscribed_on, getutcdate()), 
		unsubscribed_on, 
		first_name, 
		last_name, 
		confirmed
	FROM INSERTED
	WHERE NOT EXISTS
	(
		SELECT 1 
		FROM website.email_subscriptions
		WHERE email IN 
		(		
			SELECT email
			FROM INSERTED
		)
	);
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.views/website.menu_item_view.sql --<--<--
IF OBJECT_ID('website.menu_item_view') IS NOT NULL
DROP VIEW website.menu_item_view;
GO
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
WHERE website.menu_items.deleted = 0;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.views/website.published_content_view.sql --<--<--
IF OBJECT_ID('website.published_content_view') IS NOT NULL
DROP VIEW website.published_content_view;
GO
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
WHERE is_draft = 0
AND publish_on <= getutcdate()
AND website.contents.deleted = 0;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.views/website.tag_view.sql --<--<--
IF OBJECT_ID('website.tag_view') IS NOT NULL
DROP VIEW website.tag_view;
GO
CREATE VIEW website.tag_view
AS
WITH tags
AS
(
	SELECT DISTINCT split.member AS tag
	FROM website.contents
	CROSS APPLY core.split(website.contents.tags)
	WHERE website.contents.deleted = 0
)
SELECT
    ROW_NUMBER() OVER (ORDER BY tag) AS tag_id,
    tag
FROM tags;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.views/website.website_category_view.sql --<--<--
IF OBJECT_ID('website.website_category_view') IS NOT NULL
DROP VIEW website.website_category_view;

GO

CREATE VIEW website.website_category_view
AS
SELECT
    website.categories.category_id          AS website_category_id,
    website.categories.category_name        AS website_category_name
FROM website.categories
WHERE website.categories.deleted = 0
AND website.categories.is_blog = 0;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/05.views/website.yesterdays_email_subscriptions.sql --<--<--
IF OBJECT_ID('website.yesterdays_email_subscriptions') IS NOT NULL
DROP VIEW website.yesterdays_email_subscriptions;

GO


CREATE VIEW website.yesterdays_email_subscriptions
AS
SELECT
    email,
    first_name,
    last_name,
    'subscribed' AS subscription_type
FROM website.email_subscriptions
WHERE CONVERT(date, subscribed_on) = CONVERT(date, DATEADD(d, -1, getutcdate()))
AND NOT CONVERT(date, confirmed_on) = CONVERT(date, DATEADD(d, -1, getutcdate()))
AND website.email_subscriptions.deleted = 0
UNION ALL
SELECT
    email,
    first_name,
    last_name,
    'unsubscribed'
FROM website.email_subscriptions
WHERE CONVERT(date, unsubscribed_on) = CONVERT(date, DATEADD(d, -1, getutcdate()))
AND website.email_subscriptions.deleted = 0
UNION ALL
SELECT
    email,
    first_name,
    last_name,
    'confirmed'
FROM website.email_subscriptions
WHERE CONVERT(date, confirmed_on) = CONVERT(date, DATEADD(d, -1, getutcdate()))
AND website.email_subscriptions.deleted = 0;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/10.policy/access_policy.sql --<--<--
DECLARE @office_id integer = core.get_office_id_by_office_name('Default');

EXECUTE auth.create_api_access_policy '{Content Editor, User, Admin}', @office_id, 'website.categories', '{*}', 1;
EXECUTE auth.create_api_access_policy '{Content Editor, User, Admin}', @office_id, 'website.contents', '{*}', 1;
EXECUTE auth.create_api_access_policy '{User, Admin}', @office_id, 'website.menus', '{*}', 1;
EXECUTE auth.create_api_access_policy '{User, Admin}', @office_id, 'website.email_subscriptions', '{*}', 1;
EXECUTE auth.create_api_access_policy '{User, Admin}', @office_id, 'website.contacts', '{*}', 1;
EXECUTE auth.create_api_access_policy '{Admin}', @office_id, 'website.configurations', '{*}', 1;


-->-->-- src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/SQL Server/1.x/1.0/src/99.ownership.sql --<--<--
EXEC sp_addrolemember  @rolename = 'db_owner', @membername  = 'frapid_db_user'
GO

EXEC sp_addrolemember  @rolename = 'db_datareader', @membername  = 'report_user'
GO

DECLARE @proc sysname
DECLARE @cmd varchar(8000)

DECLARE cur CURSOR FOR 
SELECT '[' + schema_name(schema_id) + '].[' + name + ']' FROM sys.objects
WHERE type IN('FN')
AND is_ms_shipped = 0
ORDER BY 1
OPEN cur
FETCH next from cur into @proc
WHILE @@FETCH_STATUS = 0
BEGIN
     SET @cmd = 'GRANT EXEC ON ' + @proc + ' TO report_user';
     EXEC (@cmd)

     FETCH next from cur into @proc
END
CLOSE cur
DEALLOCATE cur

GO

