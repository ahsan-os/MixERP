# inventory.compound_unit_scrud_view view

| Schema | [inventory](../../schemas/inventory.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | compound_unit_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW inventory.compound_unit_scrud_view
 AS
 SELECT compound_units.compound_unit_id,
    base_unit.unit_name AS base_unit_name,
    compound_units.value,
    compare_unit.unit_name AS compare_unit_name
   FROM inventory.compound_units,
    inventory.units base_unit,
    inventory.units compare_unit
  WHERE compound_units.base_unit_id = base_unit.unit_id AND compound_units.compare_unit_id = compare_unit.unit_id;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

