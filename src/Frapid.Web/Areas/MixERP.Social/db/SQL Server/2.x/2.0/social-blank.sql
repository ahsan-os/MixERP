-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
EXECUTE dbo.drop_schema 'social';
GO
CREATE SCHEMA social;

GO


CREATE TABLE social.feeds
(
    feed_id                         bigint IDENTITY PRIMARY KEY,
    event_timestamp                 DATETIMEOFFSET NOT NULL DEFAULT(GETUTCDATE()),
    formatted_text                  national character varying(4000) NOT NULL,    
    created_by                      integer NOT NULL REFERENCES account.users,
    attachments                     national character varying(MAX), --CSV
    /****************************************************************************
    Scope acts like a group.
    For example: Contacts, Sales, Notes, etc.
    *****************************************************************************/
    scope                           national character varying(100),
    is_public                       bit NOT NULL DEFAULT(1),
    parent_feed_id                  bigint REFERENCES social.feeds,
	url								national character varying(4000),
    audit_ts                        DATETIMEOFFSET NOT NULL DEFAULT(GETUTCDATE()),
    deleted                         bit NOT NULL DEFAULT(0),
    deleted_on                      DATETIMEOFFSET,
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
ON social.feeds(scope);


CREATE TABLE social.liked_by
(
    feed_id                         bigint NOT NULL REFERENCES social.feeds,
    liked_by                     	integer NOT NULL REFERENCES account.users,
    liked_on                     	DATETIMEOFFSET NOT NULL DEFAULT(GETUTCDATE()),
    unliked                      	bit NOT NULL DEFAULT(0),
    unliked_on                   	DATETIMEOFFSET
);

CREATE UNIQUE INDEX liked_by_uix
ON social.liked_by(feed_id, liked_by)
WHERE unliked = 0;



-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/02.functions-and-logic/social.count_comments.sql --<--<--
IF OBJECT_ID('social.count_comments') IS NOT NULL
DROP FUNCTION social.count_comments;

GO

CREATE FUNCTION social.count_comments(@feed_id bigint)
RETURNS bigint
AS
BEGIN
    DECLARE @count                  bigint;

    WITH all_comments
    AS
    (
        SELECT social.feeds.feed_id 
		FROM social.feeds 
		WHERE social.feeds.parent_feed_id = @feed_id
        
		UNION ALL
        
		SELECT feed_comments.feed_id 
		FROM social.feeds AS feed_comments
        INNER JOIN all_comments
        ON all_comments.feed_id = feed_comments.parent_feed_id
    )
    
    SELECT @count = COUNT(*)
    FROM all_comments;

    RETURN @count;
END

GO

--SELECT social.count_comments(87);



-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/02.functions-and-logic/social.get_followers.sql --<--<--
IF OBJECT_ID('social.get_followers') IS NOT NULL
DROP FUNCTION social.get_followers;

GO

CREATE FUNCTION social.get_followers(@feed_id bigint, @me integer)
RETURNS character varying(MAX)
AS
BEGIN
    DECLARE @followers              character varying(MAX);
    DECLARE @parent                 bigint = social.get_root_feed_id(@feed_id);
    
    WITH all_feeds
    AS
    (
        SELECT social.feeds.feed_id 
        FROM social.feeds 
        WHERE social.feeds.feed_id = @parent

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
    
    SELECT DISTINCT @followers = COALESCE(@followers + ', ', '') + CAST(feeds.created_by AS character varying(100))
    FROM feeds
    WHERE feeds.created_by != @me;

    RETURN @followers;
END;

GO

--SELECT social.get_followers(2, 1);


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/02.functions-and-logic/social.get_next_top_feeds.sql --<--<--
IF OBJECT_ID('social.get_next_top_feeds') IS NOT NULL
DROP FUNCTION social.get_next_top_feeds;

GO

CREATE FUNCTION social.get_next_top_feeds(@user_id integer, @last_feed_id bigint, @parent_feed_id bigint, @url national character varying(4000))
RETURNS @results TABLE
(
    row_number                      bigint,
    feed_id                         bigint,
    event_timestamp                 DATETIMEOFFSET,
    audit_ts                        DATETIMEOFFSET,
    formatted_text                  national character varying(4000),    
    created_by                      integer,
    created_by_name                 national character varying(100),
    attachments                     text,
    scope                           national character varying(100),
    is_public                       bit,
    parent_feed_id                  bigint,
    child_count                     bigint
)
AS
BEGIN
    DECLARE @role_id                integer;


    SELECT @role_id = account.users.role_id
    FROM account.users
    WHERE account.users.user_id = @user_id;

    INSERT INTO @results(feed_id, event_timestamp, audit_ts, formatted_text, created_by, created_by_name, attachments, scope, is_public, parent_feed_id)
    SELECT
	TOP 10
        social.feeds.feed_id,
        social.feeds.event_timestamp,
        social.feeds.audit_ts,
        social.feeds.formatted_text,
        social.feeds.created_by,
        account.get_name_by_user_id(social.feeds.created_by),
        social.feeds.attachments,
        social.feeds.scope,
        social.feeds.is_public,
        social.feeds.parent_feed_id        
    FROM social.feeds
    WHERE social.feeds.deleted = 0
    AND (@last_feed_id = 0 OR social.feeds.feed_id < @last_feed_id)
    AND COALESCE(social.feeds.parent_feed_id, 0) = COALESCE(@parent_feed_id, 0)
	AND COALESCE(social.feeds.url, '') = COALESCE(@url, '')
    AND 
    (
        social.feeds.is_public = 1
        OR
        social.feeds.feed_id IN
        (
            SELECT social.shared_with.feed_id
            FROM social.shared_with
            WHERE (@last_feed_id = 0 OR social.shared_with.feed_id < @last_feed_id)
            AND (social.shared_with.role_id = @role_id OR social.shared_with.user_id = @user_id)
        )
    )
    ORDER BY social.feeds.audit_ts DESC;

    WITH all_comments
    AS
    (
        SELECT
            social.feeds.feed_id,
            social.feeds.event_timestamp,
            social.feeds.audit_ts,
            social.feeds.formatted_text,
            social.feeds.created_by,
            account.get_name_by_user_id(social.feeds.created_by) AS name,
            social.feeds.attachments,
            social.feeds.scope,
            social.feeds.is_public,
            social.feeds.parent_feed_id,
            ROW_NUMBER() OVER(PARTITION BY social.feeds.parent_feed_id ORDER BY social.feeds.audit_ts DESC) AS row_number
        FROM social.feeds
        WHERE social.feeds.deleted = 0
        AND (@last_feed_id = 0 OR social.feeds.feed_id < @last_feed_id)
        AND social.feeds.parent_feed_id IN
        (
            SELECT feed_id
            FROM @results
        )        
        AND 
        (
            social.feeds.is_public = 1
            OR
            social.feeds.feed_id IN
            (
                SELECT social.shared_with.feed_id
                FROM social.shared_with
                WHERE (@last_feed_id = 0 OR social.shared_with.feed_id < @last_feed_id)
                AND (social.shared_with.role_id = @role_id OR social.shared_with.user_id = @user_id)
            )
        )
    )
    INSERT INTO @results(feed_id, event_timestamp, audit_ts, formatted_text, created_by, created_by_name, attachments, scope, is_public, parent_feed_id)
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

    UPDATE @results
    SET 
		child_count = social.count_comments(feed_id);
	
	WITH ordered AS
	(
		SELECT ROW_NUMBER() OVER(ORDER BY audit_ts DESC) AS row_number, feed_id 
		FROM @results
	)

	UPDATE results
	SET 
		row_number = ordered.row_number
	FROM @results AS results
	INNER JOIN ordered
	ON results.feed_id = ordered.feed_id;
	

	RETURN;
END;

GO

--SELECT * FROM social.get_next_top_feeds(1, 0, 0, '');




-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/02.functions-and-logic/social.get_root_feed_id.sql --<--<--
IF OBJECT_ID('social.get_root_feed_id') IS NOT NULL
DROP FUNCTION social.get_root_feed_id;

GO

CREATE FUNCTION social.get_root_feed_id(@feed_id bigint)
RETURNS bigint
AS
BEGIN
    DECLARE @parent_feed_id bigint;

    SELECT 
        @parent_feed_id = parent_feed_id
    FROM social.feeds
    WHERE social.feeds.feed_id=$1
	AND social.feeds.deleted = 0;

    

    IF(@parent_feed_id IS NULL)
	BEGIN
        RETURN $1;
	END

    RETURN social.get_root_feed_id(@parent_feed_id);
END;

GO

--SELECT social.get_root_feed_id(2)


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/02.functions-and-logic/social.like.sql --<--<--
IF OBJECT_ID('social.like') IS NOT NULL
DROP PROCEDURE social."like";

GO

CREATE PROCEDURE social."like"(@user_id integer, @feed_id bigint)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

    IF NOT EXISTS
    (
        SELECT 0 
        FROM social.liked_by
        WHERE social.liked_by.feed_id = @feed_id
        AND social.liked_by.liked_by = @user_id
    )
	BEGIN
        INSERT INTO social.liked_by(feed_id, liked_by)
        SELECT @feed_id, @user_id;
    END;

    IF EXISTS
    (
        SELECT 0 
        FROM social.liked_by
        WHERE social.liked_by.feed_id = @feed_id
        AND social.liked_by.liked_by = @user_id
        AND social.liked_by.unliked = 1
    )
	BEGIN
        UPDATE social.liked_by
        SET
            unliked     = 0,
            unliked_on  = NULL
        WHERE social.liked_by.feed_id = @feed_id
        AND social.liked_by.liked_by = @user_id;
    END;
END;

GO


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/02.functions-and-logic/social.unlike.sql --<--<--
IF OBJECT_ID('social.unlike') IS NOT NULL
DROP PROCEDURE social.unlike;

GO

CREATE PROCEDURE social.unlike(@user_id integer, @feed_id bigint)
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

    IF EXISTS
    (
        SELECT 0 FROM social.liked_by
        WHERE social.liked_by.feed_id = @feed_id
        AND  social.liked_by.liked_by = @user_id
    )
	BEGIN
        UPDATE social.liked_by
        SET 
            unliked     = 1,
            unliked_on  = getutcdate()
        WHERE social.liked_by.feed_id = @feed_id
        AND social.liked_by.liked_by = @user_id;
    END;
END;

GO


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/03.menus/menus.sql --<--<--
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


EXECUTE core.create_app 'MixERP.Social', 'Social', 'Social', '1.0', 'MixERP Inc.', 'December 1, 2015', 'orange users', '/dashboard/social', NULL;

EXECUTE core.create_menu 'MixERP.Social', 'Tasks', 'Tasks', '', 'lightning', '';
EXECUTE core.create_menu 'MixERP.Social', 'Social', 'Social', '/dashboard/social', 'users', 'Tasks';

DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'MixERP.Social',
'{*}';


GO


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/04.default-values/01.default-values.sql --<--<--


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/05.triggers/social.update_audit_timestamp_trigger.sql --<--<--
IF OBJECT_ID('social.update_audit_timestamp_trigger') IS NOT NULL
DROP TRIGGER social.update_audit_timestamp_trigger;

GO

CREATE TRIGGER social.update_audit_timestamp_trigger
ON social.feeds
AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

    WITH ancestors
    AS
    (
        SELECT parent_feed_id from social.feeds where feed_id IN
		(
			SELECT feed_id
			FROM INSERTED
		)
        UNION ALL
        SELECT feeds.parent_feed_id 
        FROM social.feeds AS feeds
        INNER JOIN ancestors 
        ON ancestors.parent_feed_id =feeds.feed_id
        WHERE feeds.parent_feed_id IS NOT NULL
    )

    UPDATE social.feeds
    SET audit_ts = getutcdate()
    WHERE social.feeds.feed_id IN
    (
        SELECT * FROM ancestors
    );

	RETURN;
END;

GO


-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/05.views/social.liked_by_view.sql --<--<--
IF OBJECT_ID('social.liked_by_view') IS NOT NULL
DROP VIEW social.liked_by_view;

GO

CREATE VIEW social.liked_by_view
AS
SELECT
    social.liked_by.feed_id,
    social.liked_by.liked_by,
    account.get_name_by_user_id(social.liked_by.liked_by) AS liked_by_name,
    social.liked_by.liked_on
FROM social.liked_by
WHERE social.liked_by.unliked = 0;

GO

-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/99.ownership.sql --<--<--
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

