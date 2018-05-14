# config.filter_name_view view

| Schema | [config](../../schemas/config.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | filter_name_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW config.filter_name_view
 AS
 SELECT DISTINCT filters.object_name,
    filters.filter_name,
    filters.is_default
   FROM config.filters
  WHERE NOT filters.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

