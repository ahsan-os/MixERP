# finance.cost_center_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | cost_center_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.cost_center_scrud_view
 AS
 SELECT cost_centers.cost_center_id,
    cost_centers.cost_center_code,
    cost_centers.cost_center_name
   FROM finance.cost_centers
  WHERE NOT cost_centers.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

