
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