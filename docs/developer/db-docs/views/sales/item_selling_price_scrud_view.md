# sales.item_selling_price_scrud_view view

| Schema | [sales](../../schemas/sales.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | item_selling_price_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW sales.item_selling_price_scrud_view
 AS
 SELECT item_selling_prices.item_selling_price_id,
    ((items.item_code::text || ' ('::text) || items.item_name::text) || ')'::text AS item,
    ((units.unit_code::text || ' ('::text) || units.unit_name::text) || ')'::text AS unit,
    ((customer_types.customer_type_code::text || ' ('::text) || customer_types.customer_type_name::text) || ')'::text AS customer_type,
    ((price_types.price_type_code::text || ' ('::text) || price_types.price_type_name::text) || ')'::text AS price_type,
    item_selling_prices.includes_tax,
    item_selling_prices.price
   FROM sales.item_selling_prices
     JOIN inventory.items ON items.item_id = item_selling_prices.item_id
     JOIN inventory.units ON units.unit_id = item_selling_prices.unit_id
     JOIN inventory.customer_types ON customer_types.customer_type_id = item_selling_prices.customer_type_id
     JOIN sales.price_types ON price_types.price_type_id = item_selling_prices.price_type_id
  WHERE NOT item_selling_prices.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

