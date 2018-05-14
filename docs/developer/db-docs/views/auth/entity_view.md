# auth.entity_view view

| Schema | [auth](../../schemas/auth.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | entity_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW auth.entity_view
 AS
 SELECT tables.table_schema,
    tables.table_name,
    (tables.table_schema::text || '.'::text) || tables.table_name::text AS object_name,
    tables.table_type
   FROM information_schema.tables
  WHERE (tables.table_type::text = 'BASE TABLE'::text OR tables.table_type::text = 'VIEW'::text) AND (tables.table_schema::text <> ALL (ARRAY['pg_catalog'::character varying, 'information_schema'::character varying]::text[]));
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

