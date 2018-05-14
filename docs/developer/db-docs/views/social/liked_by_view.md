# social.liked_by_view view

| Schema | [social](../../schemas/social.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | liked_by_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW social.liked_by_view
 AS
 SELECT liked_by.feed_id,
    liked_by.liked_by,
    account.get_name_by_user_id(liked_by.liked_by) AS liked_by_name,
    liked_by.liked_on
   FROM social.liked_by
  WHERE NOT liked_by.unliked;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

