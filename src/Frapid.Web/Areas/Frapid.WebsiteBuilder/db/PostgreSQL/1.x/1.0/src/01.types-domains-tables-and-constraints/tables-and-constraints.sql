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