# inventory.checkout_detail_view view

| Schema | [inventory](../../schemas/inventory.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | checkout_detail_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW inventory.checkout_detail_view
 AS
 SELECT checkouts.transaction_master_id,
    checkouts.checkout_id,
    checkout_details.checkout_detail_id,
    checkout_details.transaction_type,
    checkout_details.store_id,
    stores.store_code,
    stores.store_name,
    checkout_details.item_id,
    items.is_taxable_item,
    items.item_code,
    items.item_name,
    checkout_details.quantity,
    checkout_details.unit_id,
    units.unit_code,
    units.unit_name,
    checkout_details.base_quantity,
    checkout_details.base_unit_id,
    base_unit.unit_code AS base_unit_code,
    base_unit.unit_name AS base_unit_name,
    checkout_details.price,
    checkout_details.discount,
    checkout_details.tax,
    checkout_details.shipping_charge,
    checkout_details.price::numeric * checkout_details.quantity::numeric + COALESCE(checkout_details.shipping_charge::numeric, 0::numeric) - COALESCE(checkout_details.discount::numeric, 0::numeric) AS amount,
    checkout_details.price::numeric * checkout_details.quantity::numeric + COALESCE(checkout_details.tax::numeric, 0::numeric) + COALESCE(checkout_details.shipping_charge::numeric, 0::numeric) - COALESCE(checkout_details.discount::numeric, 0::numeric) AS total
   FROM inventory.checkout_details
     JOIN inventory.checkouts ON checkouts.checkout_id = checkout_details.checkout_id
     JOIN inventory.stores ON stores.store_id = checkout_details.store_id
     JOIN inventory.items ON items.item_id = checkout_details.item_id
     JOIN inventory.units ON units.unit_id = checkout_details.unit_id
     JOIN inventory.units base_unit ON base_unit.unit_id = checkout_details.base_unit_id;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

