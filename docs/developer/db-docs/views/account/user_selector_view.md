# account.user_selector_view view

| Schema | [account](../../schemas/account.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | user_selector_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW account.user_selector_view
 AS
 SELECT users.user_id,
    users.name AS user_name
   FROM account.users
  WHERE NOT users.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

