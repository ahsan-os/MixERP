# inventory.customer_scrud_view view

| Schema | [inventory](../../schemas/inventory.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | customer_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW inventory.customer_scrud_view
 AS
 SELECT customers.customer_id,
    customers.customer_code,
    customers.customer_name,
    ((customer_types.customer_type_code::text || ' ('::text) || customer_types.customer_type_name::text) || ')'::text AS customer_type,
    customers.currency_code,
    customers.company_name,
    customers.company_phone_numbers,
    customers.contact_first_name,
    customers.contact_middle_name,
    customers.contact_last_name,
    customers.contact_phone_numbers,
    customers.photo
   FROM inventory.customers
     JOIN inventory.customer_types ON customer_types.customer_type_id = customers.customer_type_id;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

