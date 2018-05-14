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
