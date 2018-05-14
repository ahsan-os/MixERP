-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/Sql Server/2.x/2.0/src/01.types-domains-tables-and-constraints/tables-and-constraints.sql --<--<--
EXECUTE dbo.drop_schema 'addressbook';
GO
CREATE SCHEMA addressbook;
GO

CREATE TABLE addressbook.contacts
(
    contact_id                  uniqueidentifier PRIMARY KEY DEFAULT(NEWID()),
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
    is_private                  BIT NOT NULL DEFAULT(1),
    tags                        national character varying(500),
    created_by                  integer REFERENCES account.users,
    audit_user_id               integer REFERENCES account.users,
    audit_ts                    DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
    deleted                     BIT DEFAULT(0)    
);


-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/Sql Server/2.x/2.0/src/02.functions-and-logic/addressbook.get_tags.sql --<--<--
IF OBJECT_ID('addressbook.get_tags') IS NOT NULL
DROP FUNCTION addressbook.get_tags;

GO

CREATE FUNCTION addressbook.get_tags(@user_id integer)
RETURNS national character varying(MAX)
AS
BEGIN
	DECLARE @tags national character varying(MAX);
	SELECT @tags = COALESCE(@tags + ',', '') +  member 
	FROM
	(
			SELECT DISTINCT member
			FROM addressbook.contacts
			CROSS APPLY core.split(tags)
			WHERE addressbook.contacts.deleted = 0
			AND (addressbook.contacts.is_private = 0 OR addressbook.contacts.created_by = @user_id)
	) AS tags;

	RETURN @tags;
END;

GO

--SELECT addressbook.get_tags(1);

GO

-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/Sql Server/2.x/2.0/src/03.menus/menus.sql --<--<--
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


EXECUTE core.create_app 'Frapid.AddressBook', 'AddressBook', 'AddressBook', '1.0', 'MixERP Inc.', 'December 1, 2015', 'teal phone', '/dashboard/address-book', NULL;

EXECUTE core.create_menu 'Frapid.AddressBook', 'Tasks', 'Tasks', '', 'lightning', '';
EXECUTE core.create_menu 'Frapid.AddressBook', 'AddressBook', 'AddressBook', '/dashboard/address-book', 'phone', 'Tasks';

GO
DECLARE @office_id integer = core.get_office_id_by_office_name('Default');
EXECUTE auth.create_app_menu_policy
'Admin', 
@office_id, 
'Frapid.AddressBook',
'{*}';


-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/Sql Server/2.x/2.0/src/04.default-values/01.default-values.sql --<--<--


-->-->-- src/Frapid.Web/Areas/Frapid.AddressBook/db/Sql Server/2.x/2.0/src/99.ownership.sql --<--<--
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

