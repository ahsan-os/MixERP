# sales.cashier_scrud_view view

| Schema | [sales](../../schemas/sales.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | cashier_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW sales.cashier_scrud_view
 AS
 SELECT cashiers.cashier_id,
    cashiers.cashier_code,
    users.name AS associated_user,
    ((counters.counter_code::text || ' ('::text) || counters.counter_name::text) || ')'::text AS counter,
    cashiers.valid_from,
    cashiers.valid_till
   FROM sales.cashiers
     JOIN account.users ON users.user_id = cashiers.associated_user_id
     JOIN inventory.counters ON counters.counter_id = cashiers.counter_id
  WHERE NOT cashiers.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

