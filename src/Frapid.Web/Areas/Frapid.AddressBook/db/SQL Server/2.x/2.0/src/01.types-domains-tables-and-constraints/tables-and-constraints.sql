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
