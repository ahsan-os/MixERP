# finance.cash_repository_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | cash_repository_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.cash_repository_scrud_view
 AS
 SELECT cash_repositories.cash_repository_id,
    ((offices.office_code::text || ' ('::text) || offices.office_name::text) || ') '::text AS office,
    cash_repositories.cash_repository_code,
    cash_repositories.cash_repository_name,
    ((parent_cash_repository.cash_repository_code::text || ' ('::text) || parent_cash_repository.cash_repository_name::text) || ') '::text AS parent_cash_repository,
    cash_repositories.description
   FROM finance.cash_repositories
     JOIN core.offices ON cash_repositories.office_id = offices.office_id
     LEFT JOIN finance.cash_repositories parent_cash_repository ON cash_repositories.parent_cash_repository_id = parent_cash_repository.parent_cash_repository_id
  WHERE NOT cash_repositories.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

