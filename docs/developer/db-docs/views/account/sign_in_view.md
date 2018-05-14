# account.sign_in_view view

| Schema | [account](../../schemas/account.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | sign_in_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW account.sign_in_view
 AS
 SELECT logins.login_id,
    users.name,
    users.email,
    logins.user_id,
    roles.role_id,
    roles.role_name,
    roles.is_administrator,
    logins.browser,
    logins.ip_address,
    logins.login_timestamp,
    logins.culture,
    logins.is_active,
    logins.office_id,
    offices.office_code,
    offices.office_name,
    ((offices.office_code::text || ' ('::text) || offices.office_name::text) || ')'::text AS office,
    offices.logo,
    offices.registration_date,
    offices.po_box,
    offices.address_line_1,
    offices.address_line_2,
    offices.street,
    offices.city,
    offices.state,
    offices.zip_code,
    offices.country,
    offices.phone,
    offices.fax,
    offices.url,
    offices.currency_code,
    currencies.currency_name,
    currencies.currency_symbol,
    currencies.hundredth_name,
    offices.pan_number,
    users.last_seen_on
   FROM account.logins
     JOIN account.users ON users.user_id = logins.user_id
     JOIN account.roles ON roles.role_id = users.role_id
     JOIN core.offices ON offices.office_id = logins.office_id
     LEFT JOIN core.currencies ON currencies.currency_code::text = offices.currency_code::text
  WHERE NOT logins.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

