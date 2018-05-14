# inventory.item_scrud_view view

| Schema | [inventory](../../schemas/inventory.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | item_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW inventory.item_scrud_view
 AS
 SELECT items.item_id,
    items.item_code,
    items.item_name,
    items.barcode,
    items.item_group_id,
    item_groups.item_group_name,
    item_types.item_type_id,
    item_types.item_type_name,
    items.brand_id,
    brands.brand_name,
    items.preferred_supplier_id,
    items.unit_id,
    units.unit_code,
    units.unit_name,
    items.hot_item,
    items.cost_price,
    items.selling_price,
    items.maintain_inventory,
    items.allow_sales,
    items.allow_purchase,
    items.photo
   FROM inventory.items
     LEFT JOIN inventory.item_groups ON item_groups.item_group_id = items.item_group_id
     LEFT JOIN inventory.item_types ON item_types.item_type_id = items.item_type_id
     LEFT JOIN inventory.brands ON brands.brand_id = items.brand_id
     LEFT JOIN inventory.units ON units.unit_id = items.unit_id
  WHERE NOT items.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

