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

