-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
EXECUTE dbo.drop_schema 'account';
GO
CREATE SCHEMA account;

GO

CREATE TABLE account.roles
(
    role_id										integer PRIMARY KEY,
    role_name									national character varying(100) NOT NULL UNIQUE,
    is_administrator							bit NOT NULL DEFAULT(0),
    audit_user_id								integer,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE TABLE account.installed_domains
(
    domain_id									integer IDENTITY NOT NULL PRIMARY KEY,
    domain_name									national character varying(500),
    admin_email									national character varying(500),
    audit_user_id                           	integer,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE UNIQUE INDEX installed_domains_domain_name_uix
ON account.installed_domains(domain_name)
WHERE deleted = 0;


CREATE TABLE account.configuration_profiles
(
    configuration_profile_id					integer IDENTITY PRIMARY KEY,
    profile_name								national character varying(100) NOT NULL UNIQUE,
    is_active									bit NOT NULL DEFAULT(1),    
    allow_registration							bit NOT NULL DEFAULT(1),
    registration_office_id						integer NOT NULL REFERENCES core.offices,
    registration_role_id						integer NOT NULL REFERENCES account.roles,
    allow_facebook_registration					bit NOT NULL DEFAULT(1),
    allow_google_registration					bit NOT NULL DEFAULT(1),
    google_signin_client_id						national character varying(500),
    google_signin_scope							national character varying(500),
    facebook_app_id								national character varying(500),
    facebook_scope								national character varying(500),
    audit_user_id								integer,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE UNIQUE INDEX configuration_profile_uix
ON account.configuration_profiles(is_active)
WHERE is_active = 1
AND deleted = 0;

CREATE TABLE account.registrations
(
    registration_id								uniqueidentifier PRIMARY KEY DEFAULT(NEWID()),
    name										national character varying(100),
    email										national character varying(100) NOT NULL,
    phone										national character varying(100),
    password									national character varying(500),
    browser										national character varying(500),
    ip_address									national character varying(50),
    registered_on								datetimeoffset NOT NULL DEFAULT(getutcdate()),
    confirmed									bit DEFAULT(0),
    confirmed_on								datetimeoffset,
    audit_user_id                           	integer,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE UNIQUE INDEX registrations_email_uix
ON account.registrations(email)
WHERE deleted = 0;

CREATE TABLE account.users
(
    user_id										integer IDENTITY PRIMARY KEY,
    email										national character varying(100) NOT NULL,
    password									national character varying(500),
    office_id									integer NOT NULL REFERENCES core.offices,
    role_id										integer NOT NULL REFERENCES account.roles,
    name										national character varying(100),
    phone										national character varying(100),
    status										bit DEFAULT(1),
    created_on									datetimeoffset NOT NULL DEFAULT(getutcdate()),
	last_seen_on								datetimeoffset,
	last_ip										national character varying(500),
	last_browser								national character varying(500),
    audit_user_id								integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE UNIQUE INDEX users_email_uix
ON account.users(email)
WHERE deleted = 0;

ALTER TABLE account.configuration_profiles
ADD FOREIGN KEY(audit_user_id) REFERENCES account.users;

ALTER TABLE account.roles
ADD FOREIGN KEY(audit_user_id) REFERENCES account.users;

CREATE TABLE account.reset_requests
(
    request_id									uniqueidentifier PRIMARY KEY DEFAULT(NEWID()),
    user_id										integer NOT NULL REFERENCES account.users,
    email										national character varying(500),
    name										national character varying(500),
    requested_on								datetimeoffset NOT NULL DEFAULT(getutcdate()),
    expires_on									datetimeoffset NOT NULL DEFAULT(dateadd(d, 1, getutcdate())),
    browser										national character varying(500),
    ip_address									national character varying(50),
    confirmed									bit DEFAULT(0),
    confirmed_on								datetimeoffset,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);




CREATE TABLE account.fb_access_tokens
(
    user_id										integer PRIMARY KEY REFERENCES account.users,
    fb_user_id									national character varying(500),
    token										national character varying(MAX),
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE TABLE account.google_access_tokens
(
    user_id										integer PRIMARY KEY REFERENCES account.users,
    token										national character varying(MAX),
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE TABLE account.logins
(
    login_id									bigint IDENTITY PRIMARY KEY,
    user_id										integer REFERENCES account.users,
    office_id									integer REFERENCES core.offices,
    browser										national character varying(500),
    ip_address									national character varying(50),
    is_active									bit NOT NULL DEFAULT(1),
    login_timestamp								datetimeoffset NOT NULL DEFAULT(getutcdate()),
    culture										national character varying(12) NOT NULL,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);


CREATE TABLE account.applications
(
    application_id                              uniqueidentifier DEFAULT(NEWID()) PRIMARY KEY,
    application_name                            national character varying(100) NOT NULL,
    display_name                                national character varying(100),
    version_number                              national character varying(100),
    publisher                                   national character varying(100) NOT NULL,
    published_on                                date,
    application_url                             national character varying(500),
    description                                 national character varying(500),
    browser_based_app                           bit NOT NULL,
    privacy_policy_url                          national character varying(500),
    terms_of_service_url                        national character varying(500),
    support_email                               national character varying(100),
    culture                                     national character varying(12),
    redirect_url                                national character varying(500),
    app_secret                                  national character varying(500) UNIQUE,
    audit_user_id                               integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);

CREATE UNIQUE INDEX applications_app_name_uix
ON account.applications(application_name)
WHERE deleted = 0;

CREATE TABLE account.access_tokens
(
    access_token_id                             uniqueidentifier DEFAULT(NEWID()) PRIMARY KEY,
    issued_by                                   national character varying(500) NOT NULL,
    audience                                    national character varying(500) NOT NULL,
    ip_address                                  national character varying(100),
    user_agent                                  national character varying(500),
    header                                      national character varying(500),
    subject                                     national character varying(500),
    token_id                                    national character varying(500),
    application_id                              uniqueidentifier NULL REFERENCES account.applications,
    login_id                                    bigint NOT NULL REFERENCES account.logins,
    client_token                                national character varying(MAX),
    claims                                      national character varying(MAX),
    created_on                                  datetimeoffset NOT NULL,
    expires_on                                  datetimeoffset NOT NULL,
    revoked                                     bit NOT NULL DEFAULT(0),
    revoked_by                                  integer REFERENCES account.users,
    revoked_on                                  datetimeoffset,
    audit_user_id                           	integer REFERENCES account.users,
    audit_ts                                	DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted										bit DEFAULT(0)
);



-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.add_installed_domain.sql --<--<--
IF OBJECT_ID('account.add_installed_domain') IS NOT NULL
DROP PROCEDURE account.add_installed_domain;

GO

CREATE PROCEDURE account.add_installed_domain
(
    @domain_name                                national character varying(500),
    @admin_email                                national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF EXISTS
    (
        SELECT * FROM account.installed_domains
        WHERE domain_name = @domain_name  
		AND account.installed_domains.deleted = 0
    )
    BEGIN
        UPDATE account.installed_domains
        SET admin_email = @admin_email
        WHERE domain_name = @domain_name;
		RETURN;
    END;

    INSERT INTO account.installed_domains(domain_name, admin_email)
    SELECT @domain_name, @admin_email;
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.can_confirm_registration.sql --<--<--
IF OBJECT_ID('account.can_confirm_registration') IS NOT NULL
DROP FUNCTION account.can_confirm_registration;

GO

CREATE FUNCTION account.can_confirm_registration(@token uniqueidentifier)
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.registrations
        WHERE registration_id = @token
        AND confirmed = 0
		AND account.registrations.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;

    RETURN 0;
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.can_register_with_facebook.sql --<--<--
IF OBJECT_ID('account.can_register_with_facebook') IS NOT NULL
DROP FUNCTION account.can_register_with_facebook;

GO


CREATE FUNCTION account.can_register_with_facebook()
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM account.configuration_profiles
        WHERE is_active = 1
        AND allow_registration = 1
        AND allow_facebook_registration = 1
		AND account.configuration_profiles.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.can_register_with_google.sql --<--<--
IF OBJECT_ID('account.can_register_with_google') IS NOT NULL
DROP FUNCTION account.can_register_with_google;

GO


CREATE FUNCTION account.can_register_with_google()
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT 1 FROM account.configuration_profiles
        WHERE is_active = 1
        AND allow_registration = 1
        AND allow_google_registration = 1
		AND account.configuration_profiles.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.complete_reset.sql --<--<--
IF OBJECT_ID('account.complete_reset') IS NOT NULL
DROP PROCEDURE account.complete_reset;

GO

CREATE PROCEDURE account.complete_reset
(
    @request_id                     uniqueidentifier,
    @password                       national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @user_id                integer;
    DECLARE @email                  national character varying(500);

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

        SELECT
            @user_id = account.users.user_id,
            @email = account.users.email
        FROM account.reset_requests
        INNER JOIN account.users
        ON account.users.user_id = account.reset_requests.user_id
        WHERE account.reset_requests.request_id = @request_id
        AND expires_on >= getutcdate()
    	AND account.reset_requests.deleted = 0;

        
        UPDATE account.users
        SET
            password = @password
        WHERE user_id = @user_id;

        UPDATE account.reset_requests
        SET confirmed = 1, confirmed_on = getutcdate()
        WHERE user_id = @user_id;

        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.confirm_registration.sql --<--<--
IF OBJECT_ID('account.confirm_registration') IS NOT NULL
DROP PROCEDURE account.confirm_registration;

GO

CREATE PROCEDURE account.confirm_registration(@token uniqueidentifier)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @can_confirm        bit;
    DECLARE @office_id          integer;
    DECLARE @role_id            integer;

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;
        
        SET @can_confirm = account.can_confirm_registration(@token);

        IF(@can_confirm = 0)
        BEGIN
            SELECT 0;
            RETURN;
        END;

        SELECT
        TOP 1
            @office_id = registration_office_id        
        FROM account.configuration_profiles
        WHERE is_active = 1;

        INSERT INTO account.users(email, password, office_id, role_id, name, phone)
        SELECT email, password, @office_id, account.get_registration_role_id(email), name, phone
        FROM account.registrations
        WHERE registration_id = @token
    	AND confirmed = 0;

        UPDATE account.registrations
        SET 
            confirmed = 1, 
            confirmed_on = getutcdate()
        WHERE registration_id = @token;
    

        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;

        SELECT 1;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.email_exists.sql --<--<--
IF OBJECT_ID('account.email_exists') IS NOT NULL
DROP FUNCTION account.email_exists;

GO


CREATE FUNCTION account.email_exists(@email national character varying(100))
RETURNS bit
AS
BEGIN
    DECLARE @count integer;

    SELECT @count = count(*)  
    FROM account.users 
	WHERE email = @email
	AND account.users.deleted = 0;

    IF(COALESCE(@count, 0) = 0)
    BEGIN
        SELECT @count = count(*)  
        FROM account.registrations 
		WHERE email = @email
		AND account.registrations.deleted = 0;
    END;
    
    IF COALESCE(@count, 0) > 0
    BEGIN
		RETURN 1;
	END;
	
	RETURN 0;
END;


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.fb_sign_in.sql --<--<--
IF OBJECT_ID('account.fb_sign_in') IS NOT NULL
DROP PROCEDURE account.fb_sign_in;

GO

CREATE PROCEDURE account.fb_sign_in
(
    @fb_user_id                             national character varying(500),
    @email                                  national character varying(500),
    @office_id                              integer,
    @name                                   national character varying(500),
    @token                                  national character varying(500),
    @browser                                national character varying(500),
    @ip_address                             national character varying(500),
    @culture                                national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
	
    DECLARE @user_id                        integer;
    DECLARE @login_id                       bigint;
    DECLARE @auto_register                  bit = 0;

    DECLARE @result TABLE
    (
        login_id                            bigint,
        status                              bit,
        message                             national character varying(500)
    );

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;
        
        IF(COALESCE(@office_id, 0) = 0)
        BEGIN
            IF(SELECT COUNT(*) FROM core.offices) = 1
            BEGIN
                SELECT @office_id = office_id
                FROM core.offices;
            END;
        END;

        IF account.is_restricted_user(@email) = 1
        BEGIN
            --LOGIN IS RESTRICTED TO THIS USER
            INSERT INTO @result
            SELECT CAST(NULL AS bigint), 0, 'Access is denied';
    		
    		SELECT * FROM @result;
            RETURN;
        END;

        SELECT @user_id = user_id
        FROM account.users
        WHERE account.users.email = @email;

        IF account.user_exists(@email) = 0 AND account.can_register_with_facebook() = 1
        BEGIN
            INSERT INTO account.users(role_id, office_id, email, name)
            SELECT account.get_registration_role_id(@email), account.get_registration_office_id(), @email, @name;
            
            SET @user_id = SCOPE_IDENTITY();
        END;

        IF account.fb_user_exists(@user_id) = 0
        BEGIN
            INSERT INTO account.fb_access_tokens(user_id, fb_user_id, token)
            SELECT COALESCE(@user_id, account.get_user_id_by_email(@email)), @fb_user_id, @token;
        END
        ELSE
        BEGIN
            UPDATE account.fb_access_tokens
            SET token = @token
            WHERE user_id = @user_id;    
        END;

        IF(@user_id IS NULL)
        BEGIN
            SELECT @user_id = user_id
            FROM account.users
            WHERE account.users.email = @email;
        END;
        
    	UPDATE account.logins 
    	SET is_active = 0 
    	WHERE user_id=@user_id 
    	AND office_id = @office_id 
    	AND browser = @browser
    	AND ip_address = @ip_address;

        INSERT INTO account.logins(user_id, office_id, browser, ip_address, login_timestamp, culture)
        SELECT @user_id, @office_id, @browser, @ip_address, getutcdate(), COALESCE(@culture, '');

        SET @login_id = SCOPE_IDENTITY();
    	
        INSERT INTO @result
        SELECT @login_id, 1, 'Welcome';

    	SELECT * FROM @result;
                
        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.fb_user_exists.sql --<--<--
IF OBJECT_ID('account.fb_user_exists') IS NOT NULL
DROP FUNCTION account.fb_user_exists;

GO

CREATE FUNCTION account.fb_user_exists(@user_id integer)
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.fb_access_tokens
        WHERE account.fb_access_tokens.user_id = @user_id
		AND account.fb_access_tokens.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.get_email_by_user_id.sql --<--<--
IF OBJECT_ID('account.get_email_by_user_id') IS NOT NULL
DROP FUNCTION account.get_email_by_user_id;

GO

CREATE FUNCTION account.get_email_by_user_id(@user_id integer)
RETURNS national character varying(500)
AS
BEGIN
    RETURN
    (
		SELECT
			account.users.email
		FROM account.users
		WHERE account.users.user_id = @user_id
		AND account.users.deleted = 0
    );
END;


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.get_name_by_user_id.sql --<--<--
IF OBJECT_ID('account.get_name_by_user_id') IS NOT NULL
DROP FUNCTION account.get_name_by_user_id;

GO

CREATE FUNCTION account.get_name_by_user_id(@user_id integer)
RETURNS national character varying(500)
AS
BEGIN
    RETURN
    (
		SELECT
			account.users.name
		FROM account.users
		WHERE account.users.user_id = @user_id
		AND account.users.deleted = 0
	);
END;


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.get_office_id_by_login_id.sql --<--<--
IF OBJECT_ID('account.get_office_id_by_login_id') IS NOT NULL
DROP FUNCTION account.get_office_id_by_login_id;

GO

CREATE FUNCTION account.get_office_id_by_login_id(@login_id bigint)
RETURNS integer
AS
BEGIN
	RETURN
	(
		SELECT account.logins.office_id 
		FROM account.logins
		WHERE account.logins.login_id = @login_id
	);
END;

GO

--SELECT account.get_office_id_by_login_id(1);

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.get_registration_office_id.sql --<--<--
IF OBJECT_ID('account.get_registration_office_id') IS NOT NULL
DROP FUNCTION account.get_registration_office_id;

GO


CREATE FUNCTION account.get_registration_office_id()
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT
			registration_office_id
		FROM account.configuration_profiles
		WHERE is_active = 1
		AND account.configuration_profiles.deleted = 0 
	);
END;


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.get_registration_role_id.sql --<--<--
IF OBJECT_ID('account.get_registration_role_id') IS NOT NULL
DROP FUNCTION account.get_registration_role_id;

GO

CREATE FUNCTION account.get_registration_role_id(@email national character varying(500))
RETURNS integer
AS
BEGIN
    DECLARE @is_admin               bit = 0;
    DECLARE @role_id                integer;

    IF EXISTS
    (
        SELECT * FROM account.installed_domains
        WHERE admin_email = @email
		AND account.installed_domains.deleted = 0
    )
    BEGIN
        SET @is_admin = 1;
    END;
   
    IF(@is_admin = 1)
    BEGIN
        SELECT
        TOP 1
            @role_id = role_id            
        FROM account.roles
        WHERE is_administrator = 1
		AND account.roles.deleted = 0;
    END
    ELSE
    BEGIN
        SELECT 
            @role_id = registration_role_id
        FROM account.configuration_profiles
        WHERE is_active = 1
		AND account.configuration_profiles.deleted = 0;
    END;

    RETURN @role_id;
END;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.get_role_name_by_role_id.sql --<--<--
IF OBJECT_ID('account.get_role_name_by_role_id') IS NOT NULL
DROP FUNCTION account.get_role_name_by_role_id;

GO

CREATE FUNCTION account.get_role_name_by_role_id(@role_id integer)
RETURNS national character varying(100)
AS
BEGIN
    RETURN
    (
        SELECT account.roles.role_name
        FROM account.roles
        WHERE account.roles.role_id = @role_id
    );
END

GO

--SELECT account.get_role_name_by_role_id(9999);

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.get_user_id_by_email.sql --<--<--
IF OBJECT_ID('account.get_user_id_by_email') IS NOT NULL
DROP FUNCTION account.get_user_id_by_email;

GO

CREATE FUNCTION account.get_user_id_by_email(@email national character varying(100))
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT
		user_id
		FROM account.users
		WHERE account.users.email = @email
		AND account.users.deleted = 0 
	);
END;


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.get_user_id_by_login_id.sql --<--<--
IF OBJECT_ID('account.get_user_id_by_login_id') IS NOT NULL
DROP FUNCTION account.get_user_id_by_login_id;

GO

CREATE FUNCTION account.get_user_id_by_login_id(@login_id bigint)
RETURNS integer
AS
BEGIN
    RETURN
    (
		SELECT
		user_id
		FROM account.logins
		WHERE account.logins.login_id = @login_id
		AND account.logins.deleted = 0 
	);
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.google_sign_in.sql --<--<--
IF OBJECT_ID('account.google_sign_in') IS NOT NULL
DROP PROCEDURE account.google_sign_in;

GO


CREATE PROCEDURE account.google_sign_in
(
    @email                                  national character varying(500),
    @office_id                              integer,
    @name                                   national character varying(500),
    @token                                  national character varying(500),
    @browser                                national character varying(500),
    @ip_address                             national character varying(500),
    @culture                                national character varying(500)
)
AS
BEGIN    
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @user_id                        integer;
    DECLARE @login_id                       bigint;
	DECLARE @result TABLE
	(
		login_id                            bigint,
		status                              bit,
		message                             text
	);


    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

        IF(COALESCE(@office_id, 0) = 0)
        BEGIN
            IF(SELECT COUNT(*) FROM core.offices) = 1
            BEGIN
                SELECT @office_id = office_id
                FROM core.offices;
            END;
        END;

        IF account.is_restricted_user(@email) = 1
        BEGIN
            --LOGIN IS RESTRICTED TO THIS USER
            INSERT INTO @result
            SELECT CAST(NULL AS bigint), 0, 'Access is denied';
    		
    		SELECT * FROM @result;
            RETURN;
        END;

        IF account.user_exists(@email) = 0 AND account.can_register_with_google() = 1
        BEGIN
            INSERT INTO account.users(role_id, office_id, email, name)
            SELECT account.get_registration_role_id(@email), account.get_registration_office_id(), @email, @name;

            SET @user_id = SCOPE_IDENTITY();
        END;

        SELECT @user_id = user_id
        FROM account.users
        WHERE account.users.email = @email;

        IF account.google_user_exists(@user_id) = 0
        BEGIN
            INSERT INTO account.google_access_tokens(user_id, token)
            SELECT COALESCE(@user_id, account.get_user_id_by_email(@email)), @token;
    	END
        ELSE
        BEGIN
            UPDATE account.google_access_tokens
            SET token = @token
            WHERE user_id = @user_id;    
        END;

    	UPDATE account.logins 
    	SET is_active = 0 
    	WHERE user_id=@user_id 
    	AND office_id = @office_id 
    	AND browser = @browser
    	AND ip_address = @ip_address;

        INSERT INTO account.logins(user_id, office_id, browser, ip_address, login_timestamp, culture)
        SELECT @user_id, @office_id, @browser, @ip_address, getutcdate(), COALESCE(@culture, '');

        SET @login_id = SCOPE_IDENTITY();

        INSERT INTO @result
        SELECT @login_id, 1, 'Welcome';

    	SELECT * FROM @result;
                
        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.google_user_exists.sql --<--<--
IF OBJECT_ID('account.google_user_exists') IS NOT NULL
DROP FUNCTION account.google_user_exists;

GO

CREATE FUNCTION account.google_user_exists(@user_id integer)
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.google_access_tokens
        WHERE account.google_access_tokens.user_id = @user_id
		AND account.google_access_tokens.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.has_account.sql --<--<--
IF OBJECT_ID('account.has_account') IS NOT NULL
DROP FUNCTION account.has_account;

GO

CREATE FUNCTION account.has_account(@email national character varying(100))
RETURNS bit
AS
BEGIN
    DECLARE @count                          integer;

    SELECT @count = count(*) 
	FROM account.users 
	WHERE email = @email
	AND account.users.deleted = 0;
    IF COALESCE(@count, 0) = 1
    BEGIN
		RETURN 1;
    END;
    
    RETURN 0;
END;


GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.has_active_reset_request.sql --<--<--
IF OBJECT_ID('account.has_active_reset_request') IS NOT NULL
DROP FUNCTION account.has_active_reset_request;

GO

CREATE FUNCTION account.has_active_reset_request(@email national character varying(500))
RETURNS bit
AS
BEGIN
    DECLARE @expires_on                     datetimeoffset = dateadd(d, 1, getutcdate());

    IF EXISTS
    (
        SELECT * FROM account.reset_requests
        WHERE email = @email
        AND expires_on <= @expires_on
		AND account.reset_requests.deleted = 0
    )
    BEGIN        
        RETURN 1;
    END;

    RETURN 0;
END;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.is_admin.sql --<--<--
IF OBJECT_ID('account.is_admin') IS NOT NULL
DROP FUNCTION account.is_admin;

GO

CREATE FUNCTION account.is_admin(@user_id integer)
RETURNS bit
AS
BEGIN
    RETURN
    (
        SELECT account.roles.is_administrator FROM account.users
        INNER JOIN account.roles
        ON account.users.role_id = account.roles.role_id
        WHERE account.users.user_id=@user_id
    );
END;

GO

--SELECT account.is_admin(1);



-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.is_restricted_user.sql --<--<--
IF OBJECT_ID('account.is_restricted_user') IS NOT NULL
DROP FUNCTION account.is_restricted_user;

GO

CREATE FUNCTION account.is_restricted_user(@email national character varying(100))
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.users
        WHERE account.users.email = @email
        AND account.users.status = 0
		AND account.users.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.is_valid_client_token.sql --<--<--
IF OBJECT_ID('account.is_valid_client_token') IS NOT NULL
DROP FUNCTION account.is_valid_client_token;

GO

CREATE FUNCTION account.is_valid_client_token(@client_token national character varying(MAX), @ip_address national character varying(500), @user_agent national character varying(500))
RETURNS bit
AS
BEGIN
    DECLARE @created_on datetimeoffset;
    DECLARE @expires_on datetimeoffset;
    DECLARE @revoked bit;

    IF(COALESCE(@client_token, '') = '')
    BEGIN
        RETURN 0;
    END;

    SELECT
        @created_on = created_on,
        @expires_on = expires_on,
        @revoked = revoked
    FROM account.access_tokens
    WHERE client_token = @client_token
    AND ip_address = @ip_address
    AND user_agent = @user_agent
	AND account.access_tokens.deleted = 0;
    
    IF(COALESCE(@revoked, 1)) = 1
    BEGIN
        RETURN 0;
    END;

    IF(@created_on > getutcdate())
    BEGIN
        RETURN 0;
    END;

    IF(COALESCE(@expires_on, getutcdate()) <= getutcdate())
    BEGIN
        RETURN 0;
    END;
    
    RETURN 1;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.is_valid_login_id.sql --<--<--
IF OBJECT_ID('account.is_valid_login_id') IS NOT NULL
DROP FUNCTION account.is_valid_login_id;

GO

CREATE FUNCTION account.is_valid_login_id(@login_id bigint)
RETURNS bit
BEGIN
    IF EXISTS(SELECT 1 FROM account.logins WHERE login_id=@login_id)
	BEGIN
        RETURN 1;
    END;

    RETURN 0;
END;

GO

--SELECT account.is_valid_login_id(1);

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.reset_account.sql --<--<--
IF OBJECT_ID('account.reset_account') IS NOT NULL
DROP PROCEDURE account.reset_account;

GO


CREATE PROCEDURE account.reset_account
(
    @email                                  national character varying(500),
    @browser                                national character varying(500),
    @ip_address                             national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @request_table_variable			TABLE(request_id uniqueidentifier);
    DECLARE @user_id                        integer;
    DECLARE @name                           national character varying(500);
    DECLARE @expires_on                     datetimeoffset = dateadd(d, 1, getutcdate());

    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;

        IF(account.user_exists(@email) = 0 OR account.is_restricted_user(@email) = 1)
        BEGIN
            RETURN;
        END;

        SELECT
            @user_id = user_id,
            @name = name
        FROM account.users
        WHERE email = @email
    	AND account.users.deleted = 0;

        IF account.has_active_reset_request(@email) = 1
        BEGIN
            SELECT 
            TOP 1
            * FROM account.reset_requests
            WHERE email = @email
            AND expires_on <= @expires_on
    		AND account.reset_requests.deleted = 0;
            
            RETURN;
        END;

        INSERT INTO account.reset_requests(user_id, email, name, browser, ip_address, expires_on)
        OUTPUT INSERTED.request_id INTO @request_table_variable
        SELECT @user_id, @email, @name, @browser, @ip_address, @expires_on


        SELECT *
        FROM account.reset_requests
        WHERE request_id = 
        (
    		SELECT request_id 
    		FROM @request_table_variable
    	);

        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;


GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.sign_in.sql --<--<--
IF OBJECT_ID('account.sign_in') IS NOT NULL
DROP PROCEDURE account.sign_in;

GO


CREATE PROCEDURE account.sign_in
(
    @email                                  national character varying(500),
    @office_id                              integer,
    @browser                                national character varying(500),
    @ip_address                             national character varying(500),
    @culture                                national character varying(500)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @login_id                       bigint;
    DECLARE @user_id                        integer;

	DECLARE @result TABLE
	(
		login_id                            bigint,
		status                              bit,
		message                             national character varying(100)
	);


    BEGIN TRY
        DECLARE @tran_count int = @@TRANCOUNT;
        
        IF(@tran_count= 0)
        BEGIN
            BEGIN TRANSACTION
        END;
        
        IF(COALESCE(@office_id, 0) = 0)
        BEGIN
            IF(SELECT COUNT(*) FROM core.offices) = 1
            BEGIN
                SELECT @office_id = office_id
                FROM core.offices;
            END;
        END;

        IF account.is_restricted_user(@email) = 1
        BEGIN
    		INSERT INTO @result
            SELECT CAST(NULL AS bigint), 0, 'Access is denied';

    		SELECT * FROM @result;
            RETURN;
        END;

        SELECT @user_id = user_id 
        FROM account.users
        WHERE email = @email;

    	UPDATE account.logins 
    	SET is_active = 0 
    	WHERE user_id=@user_id 
    	AND office_id = @office_id 
    	AND browser = @browser
    	AND ip_address = @ip_address;

        INSERT INTO account.logins(user_id, office_id, browser, ip_address, login_timestamp, culture)
        SELECT @user_id, @office_id, @browser, @ip_address, getutcdate(), COALESCE(@culture, '');

        SET @login_id = SCOPE_IDENTITY();
        
    	INSERT INTO @result
        SELECT @login_id, 1, 'Welcome';

    	SELECT * FROM @result;

        IF(@tran_count = 0)
        BEGIN
            COMMIT TRANSACTION;
        END;
    END TRY
    BEGIN CATCH
        IF(XACT_STATE() <> 0 AND @tran_count = 0) 
        BEGIN
            ROLLBACK TRANSACTION;
        END;

        DECLARE @ErrorMessage national character varying(4000)  = ERROR_MESSAGE();
        DECLARE @ErrorSeverity int                              = ERROR_SEVERITY();
        DECLARE @ErrorState int                                 = ERROR_STATE();
        RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH;
END;

GO


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.functions-and-logic/account.user_exists.sql --<--<--
IF OBJECT_ID('account.user_exists') IS NOT NULL
DROP FUNCTION account.user_exists;

GO

CREATE FUNCTION account.user_exists(@email national character varying(100))
RETURNS bit
AS
BEGIN
    IF EXISTS
    (
        SELECT *
        FROM account.users
        WHERE account.users.email = @email
		AND account.users.deleted = 0
    )
    BEGIN
        RETURN 1;
    END;
    
    RETURN 0;
END;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.relationships/auth.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.relationships/core.sql --<--<--
ALTER TABLE core.offices
ADD FOREIGN KEY(audit_user_id) REFERENCES account.users;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/02.triggers/account.token_auto_expiry_trigger.sql --<--<--
IF OBJECT_ID('account.token_auto_expiry_trigger') IS NOT NULL
DROP TRIGGER account.token_auto_expiry_trigger;

GO

CREATE TRIGGER account.token_auto_expiry_trigger
ON account.access_tokens
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;
    SET XACT_ABORT ON;
	
	DECLARE @ip_address national character varying(100);
	DECLARE @user_agent national character varying(500);
	
	SELECT
		@ip_address = ip_address,
		@user_agent = user_agent
	FROM inserted;

	UPDATE account.access_tokens
	SET
		revoked = 1,
		revoked_on = getutcdate()
    WHERE ip_address = @ip_address
    AND user_agent = @user_agent;
    		

	INSERT INTO account.access_tokens(access_token_id, issued_by, audience, ip_address, user_agent, header, subject, token_id, application_id, login_id, client_token, claims, created_on, expires_on, revoked, revoked_by, revoked_on)
	SELECT access_token_id, issued_by, audience, ip_address, user_agent, header, subject, token_id, application_id, login_id, client_token, claims, created_on, expires_on, revoked, revoked_by, revoked_on
	FROM inserted;	
END;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/03.menus/menus.sql --<--<--
EXECUTE core.create_app 'Frapid.Account', 'Account', 'Account', '1.0', 'MixERP Inc.', 'December 1, 2015', 'grey lock', '/dashboard/account/user/list', NULL;

EXECUTE core.create_menu 'Frapid.Account', 'Roles', 'Roles', '/dashboard/account/roles', 'users', '';

EXECUTE core.create_menu 'Frapid.Account', 'Users', 'Users', '', 'user', '';
EXECUTE core.create_menu 'Frapid.Account', 'AddNewUser', 'Add a New User', '/dashboard/account/user/add', 'user', 'Users';
EXECUTE core.create_menu 'Frapid.Account', 'ChangePassword', 'Change Password', '/dashboard/account/user/change-password', 'user', 'Users';
EXECUTE core.create_menu 'Frapid.Account', 'ListUsers', 'List Users', '/dashboard/account/user/list', 'user', 'Users';

EXECUTE core.create_menu 'Frapid.Account', 'ConfigurationProfile', 'Configuration Profile', '/dashboard/account/configuration-profile', 'configure', '';
EXECUTE core.create_menu 'Frapid.Account', 'EmailTemplates', 'Email Templates', '', 'mail', '';
EXECUTE core.create_menu 'Frapid.Account', 'AccountVerification', 'Account Verification', '/dashboard/account/email-templates/account-verification', 'checkmark box', 'Email Templates';
EXECUTE core.create_menu 'Frapid.Account', 'PasswordReset', 'Password Reset', '/dashboard/account/email-templates/password-reset', 'key', 'Email Templates';
EXECUTE core.create_menu 'Frapid.Account', 'WelcomeEmail', 'Welcome Email', '/dashboard/account/email-templates/welcome-email', 'star', 'Email Templates';
EXECUTE core.create_menu 'Frapid.Account', 'WelcomeEmail3rdParty)', 'Welcome Email (3rd Party)', '/dashboard/account/email-templates/welcome-email-other', 'star outline', 'Email Templates';



-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/04.default-values/01.default-values.sql --<--<--
INSERT INTO account.roles(role_id, role_name, is_administrator)
SELECT 1000,   'Guest',                 0 UNION ALL
SELECT 2000,   'Website User',          0 UNION ALL
SELECT 3000,   'Partner',               0 UNION ALL
SELECT 4000,   'Content Editor',        0 UNION ALL
SELECT 5000,   'Backoffice User',       0 UNION ALL
SELECT 9999,   'Admin',                 1;


INSERT INTO account.configuration_profiles(profile_name, is_active, allow_registration, registration_role_id, registration_office_id)
SELECT 'Default', 1, 1, 2000, 1;

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/05.scrud-views/account.configuration_profile_scrud_view.sql --<--<--
IF OBJECT_ID('account.configuration_profile_scrud_view') IS NOT NULL
DROP VIEW account.configuration_profile_scrud_view;

GO

CREATE VIEW account.configuration_profile_scrud_view
AS
SELECT
	account.configuration_profiles.configuration_profile_id,
	account.configuration_profiles.profile_name,
	account.configuration_profiles.is_active,
	account.configuration_profiles.allow_registration,
	account.roles.role_name AS defult_role,
	core.offices.office_code + ' (' + core.offices.office_name + ')' AS default_office
FROM account.configuration_profiles
LEFT JOIN account.roles
ON account.roles.role_id = account.configuration_profiles.registration_role_id
LEFT JOIN core.offices
ON core.offices.office_id = account.configuration_profiles.registration_office_id
WHERE account.configuration_profiles.deleted = 0;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/05.scrud-views/account.user_scrud_view.sql --<--<--
IF OBJECT_ID('account.user_scrud_view') IS NOT NULL
DROP VIEW account.user_scrud_view;

GO

CREATE VIEW account.user_scrud_view
AS
SELECT
	account.users.user_id,
	account.users.email,
	account.users.name,
	account.users.phone,
	core.offices.office_code + ' (' + core.offices.office_name + ')' AS office,
	account.roles.role_name
FROM account.users
INNER JOIN account.roles
ON account.roles.role_id = account.users.role_id
INNER JOIN core.offices
ON core.offices.office_id = account.users.office_id
WHERE account.users.deleted = 0;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/05.scrud-views/account.user_selector_view.sql --<--<--
IF OBJECT_ID('account.user_selector_view') IS NOT NULL
DROP VIEW account.user_selector_view;

GO

CREATE VIEW account.user_selector_view
AS
SELECT
    account.users.user_id,
    account.users.name AS user_name
FROM account.users
WHERE account.users.deleted = 0;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/05.views/account.sign_in_view.sql --<--<--
IF OBJECT_ID('account.sign_in_view') IS NOT NULL
DROP VIEW account.sign_in_view;

GO

CREATE VIEW account.sign_in_view
AS
SELECT
    account.logins.login_id,
    account.users.name,
    account.users.email,
    account.logins.user_id,
    account.roles.role_id,
    account.roles.role_name,
    account.roles.is_administrator,
    account.logins.browser,
    account.logins.ip_address,
    account.logins.login_timestamp,
    account.logins.culture,
    account.logins.is_active,
    account.logins.office_id,
    core.offices.office_code,
    core.offices.office_name,
    core.offices.office_code + ' (' + core.offices.office_name + ')' AS office,
    core.offices.logo,
    core.offices.registration_date,
    core.offices.po_box,
    core.offices.address_line_1,
    core.offices.address_line_2,
    core.offices.street,
    core.offices.city,
    core.offices.state,
    core.offices.zip_code,
    core.offices.country,
    core.offices.email AS office_email,
    core.offices.phone,
    core.offices.fax,
    core.offices.url,
    core.offices.currency_code,
    core.currencies.currency_name,
    core.currencies.currency_symbol,
    core.currencies.hundredth_name,
    core.offices.pan_number,
    account.users.last_seen_on
FROM account.logins
INNER JOIN account.users
ON account.users.user_id = account.logins.user_id
INNER JOIN account.roles
ON account.roles.role_id = account.users.role_id
INNER JOIN core.offices
ON core.offices.office_id = account.logins.office_id
LEFT JOIN core.currencies
ON core.currencies.currency_code = core.offices.currency_code
WHERE account.logins.deleted = 0;

GO

-->-->-- src/Frapid.Web/Areas/Frapid.Account/db/SQL Server/1.x/1.0/src/99.ownership.sql --<--<--
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

