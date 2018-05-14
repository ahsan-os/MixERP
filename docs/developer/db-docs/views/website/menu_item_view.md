# website.menu_item_view view

| Schema | [website](../../schemas/website.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | menu_item_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW website.menu_item_view
 AS
 SELECT menus.menu_id,
    menus.menu_name,
    menus.description,
    menu_items.menu_item_id,
    menu_items.sort,
    menu_items.title,
    menu_items.url,
    menu_items.target,
    menu_items.content_id,
    contents.alias AS content_alias
   FROM website.menu_items
     JOIN website.menus ON menus.menu_id = menu_items.menu_id
     LEFT JOIN website.contents ON contents.content_id = menu_items.content_id
  WHERE NOT menu_items.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

