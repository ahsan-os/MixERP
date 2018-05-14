EXECUTE dbo.drop_schema 'auth';
GO
CREATE SCHEMA auth;

GO

CREATE TABLE auth.access_types
(
    access_type_id                          integer PRIMARY KEY,
    access_type_name                        national character varying(48) NOT NULL,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0)
);

CREATE UNIQUE INDEX access_types_uix
ON auth.access_types(access_type_name)
WHERE deleted = 0;


CREATE TABLE auth.group_entity_access_policy
(
    group_entity_access_policy_id           integer IDENTITY NOT NULL PRIMARY KEY,
    entity_name                             national character varying(500) NULL,
    office_id                               integer NOT NULL REFERENCES core.offices,
    role_id                                 integer NOT NULL REFERENCES account.roles,
    access_type_id                          integer NULL REFERENCES auth.access_types,
    allow_access                            bit NOT NULL,
    audit_user_id                           integer NULL REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0)
);

CREATE TABLE auth.entity_access_policy
(
    entity_access_policy_id                 integer IDENTITY NOT NULL PRIMARY KEY,
    entity_name                             national character varying(500) NULL,
    office_id                               integer NOT NULL REFERENCES core.offices,
    user_id                                 integer NOT NULL REFERENCES account.users,
    access_type_id                          integer NULL REFERENCES auth.access_types,
    allow_access                            bit NOT NULL,
    audit_user_id                           integer NULL REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0)
);

CREATE TABLE auth.group_menu_access_policy
(
    group_menu_access_policy_id             bigint IDENTITY PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES core.offices,
    menu_id                                 integer NOT NULL REFERENCES core.menus,
    role_id                                 integer REFERENCES account.roles,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0)
);

CREATE UNIQUE INDEX menu_access_uix
ON auth.group_menu_access_policy(office_id, menu_id, role_id)
WHERE deleted = 0;

CREATE TABLE auth.menu_access_policy
(
    menu_access_policy_id                   bigint IDENTITY PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES core.offices,
    menu_id                                 integer NOT NULL REFERENCES core.menus,
    user_id                                 integer NULL REFERENCES account.users,
    allow_access                            bit,
    disallow_access                         bit,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                DATETIMEOFFSET NULL DEFAULT(GETUTCDATE()),
	deleted									bit DEFAULT(0),
											CONSTRAINT menu_access_policy_access_chk CHECK(NOT(allow_access= 1 AND disallow_access = 1))
);


CREATE UNIQUE INDEX menu_access_policy_uix
ON auth.menu_access_policy(office_id, menu_id, user_id)
WHERE deleted = 0;


GO
