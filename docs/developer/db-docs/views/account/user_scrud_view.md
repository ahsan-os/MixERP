# account.user_scrud_view view

| Schema | [account](../../schemas/account.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | user_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW account.user_scrud_view
 AS
 SELECT users.user_id,
    users.email,
    users.name,
    users.phone,
    ((offices.office_code::text || ' ('::text) || offices.office_name::text) || ')'::text AS office,
    roles.role_name
   FROM account.users
     JOIN account.roles ON roles.role_id = users.role_id
     JOIN core.offices ON offices.office_id = users.office_id
  WHERE NOT users.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

