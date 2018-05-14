# inventory.item_view view

| Schema | [inventory](../../schemas/inventory.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | item_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW inventory.item_view
 AS
 SELECT items.item_id,
    items.item_code,
    items.item_name,
    items.is_taxable_item,
    items.barcode,
    items.item_group_id,
    item_groups.item_group_name,
    item_types.item_type_id,
    item_types.item_type_name,
    items.brand_id,
    brands.brand_name,
    items.preferred_supplier_id,
    items.unit_id,
    array_to_string(inventory.get_associated_unit_list(items.unit_id), ','::text) AS valid_units,
    units.unit_code,
    units.unit_name,
    items.hot_item,
    items.cost_price,
    items.cost_price_includes_tax,
    items.selling_price,
    items.selling_price_includes_tax,
    items.photo,
    items.maintain_inventory
   FROM inventory.items
     JOIN inventory.item_groups ON item_groups.item_group_id = items.item_group_id
     JOIN inventory.item_types ON item_types.item_type_id = items.item_type_id
     JOIN inventory.brands ON brands.brand_id = items.brand_id
     JOIN inventory.units ON units.unit_id = items.unit_id
  WHERE NOT items.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

