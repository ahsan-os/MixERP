# website.content_scrud_view view

| Schema | [website](../../schemas/website.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | content_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW website.content_scrud_view
 AS
 SELECT contents.content_id,
    contents.title,
    categories.category_name,
    categories.is_blog,
    contents.alias,
    contents.is_draft,
    contents.publish_on
   FROM website.contents
     JOIN website.categories ON categories.category_id = contents.category_id
  WHERE NOT contents.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

