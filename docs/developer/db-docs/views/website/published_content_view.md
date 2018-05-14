# website.published_content_view view

| Schema | [website](../../schemas/website.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | published_content_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW website.published_content_view
 AS
 SELECT contents.content_id,
    contents.category_id,
    categories.category_name,
    categories.alias AS category_alias,
    contents.title,
    contents.alias,
    contents.author_id,
    users.name AS author_name,
    contents.markdown,
    contents.publish_on,
        CASE
            WHEN contents.last_edited_on IS NULL THEN contents.publish_on
            ELSE contents.last_edited_on
        END AS last_edited_on,
    contents.contents,
    contents.tags,
    contents.seo_description,
    contents.is_homepage,
    categories.is_blog
   FROM website.contents
     JOIN website.categories ON categories.category_id = contents.category_id
     LEFT JOIN account.users ON contents.author_id = users.user_id
  WHERE NOT contents.is_draft AND contents.publish_on <= now() AND NOT contents.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

