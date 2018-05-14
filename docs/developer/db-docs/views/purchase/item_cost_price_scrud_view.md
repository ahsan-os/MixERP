# purchase.item_cost_price_scrud_view view

| Schema | [purchase](../../schemas/purchase.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | item_cost_price_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW purchase.item_cost_price_scrud_view
 AS
 SELECT item_cost_prices.item_cost_price_id,
    item_cost_prices.item_id,
    ((items.item_code::text || ' ('::text) || items.item_name::text) || ')'::text AS item,
    item_cost_prices.unit_id,
    ((units.unit_code::text || ' ('::text) || units.unit_name::text) || ')'::text AS unit,
    item_cost_prices.supplier_id,
    ((suppliers.supplier_code::text || ' ('::text) || suppliers.supplier_name::text) || ')'::text AS supplier,
    item_cost_prices.lead_time_in_days,
    item_cost_prices.includes_tax,
    item_cost_prices.price
   FROM purchase.item_cost_prices
     JOIN inventory.items ON items.item_id = item_cost_prices.item_id
     JOIN inventory.units ON units.unit_id = item_cost_prices.unit_id
     JOIN inventory.suppliers ON suppliers.supplier_id = item_cost_prices.supplier_id
  WHERE NOT item_cost_prices.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

