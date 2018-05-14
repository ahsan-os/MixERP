DROP SCHEMA IF EXISTS auth CASCADE;
CREATE SCHEMA auth;

CREATE TABLE auth.access_types
(
    access_type_id                              integer PRIMARY KEY,
    access_type_name                            national character varying(48) NOT NULL,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE UNIQUE INDEX access_types_uix
ON auth.access_types(UPPER(access_type_name))
WHERE NOT deleted;


CREATE TABLE auth.group_entity_access_policy
(
    group_entity_access_policy_id           SERIAL NOT NULL PRIMARY KEY,
    entity_name                             national character varying(500) NULL,
    office_id                               integer NOT NULL REFERENCES core.offices,
    role_id                                 integer NOT NULL REFERENCES account.roles,
    access_type_id                          integer NULL REFERENCES auth.access_types,
    allow_access                            boolean NOT NULL,
    audit_user_id                           integer NULL REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE TABLE auth.entity_access_policy
(
    entity_access_policy_id                 SERIAL NOT NULL PRIMARY KEY,
    entity_name                             national character varying(500) NULL,
    office_id                               integer NOT NULL REFERENCES core.offices,
    user_id                                 integer NOT NULL REFERENCES account.users,
    access_type_id                          integer NULL REFERENCES auth.access_types,
    allow_access                            boolean NOT NULL,
    audit_user_id                           integer NULL REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE TABLE auth.group_menu_access_policy
(
    group_menu_access_policy_id             BIGSERIAL PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES core.offices,
    menu_id                                 integer NOT NULL REFERENCES core.menus,
    role_id                                 integer REFERENCES account.roles,
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE UNIQUE INDEX menu_access_uix
ON auth.group_menu_access_policy(office_id, menu_id, role_id)
WHERE NOT deleted;

CREATE TABLE auth.menu_access_policy
(
    menu_access_policy_id                   BIGSERIAL PRIMARY KEY,
    office_id                               integer NOT NULL REFERENCES core.offices,
    menu_id                                 integer NOT NULL REFERENCES core.menus,
    user_id                                 integer NULL REFERENCES account.users,
    allow_access                            boolean,
    disallow_access                         boolean
                                            CHECK(NOT(allow_access is true AND disallow_access is true)),
    audit_user_id                           integer REFERENCES account.users,
    audit_ts                                TIMESTAMP WITH TIME ZONE DEFAULT(NOW()),
	deleted									boolean DEFAULT(false)
);

CREATE UNIQUE INDEX menu_access_policy_uix
ON auth.menu_access_policy(office_id, menu_id, user_id)
WHERE NOT deleted;
