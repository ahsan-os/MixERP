# website.tag_view view

| Schema | [website](../../schemas/website.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | tag_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW website.tag_view
 AS
 WITH tags AS (
         SELECT DISTINCT unnest(regexp_split_to_array(contents.tags, ','::text)) AS tag
           FROM website.contents
          WHERE NOT contents.deleted
        )
 SELECT row_number() OVER (ORDER BY tags.tag) AS tag_id,
    tags.tag
   FROM tags;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

