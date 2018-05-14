# finance.auto_verification_policy_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | auto_verification_policy_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.auto_verification_policy_scrud_view
 AS
 SELECT auto_verification_policy.auto_verification_policy_id,
    auto_verification_policy.user_id,
    account.get_name_by_user_id(auto_verification_policy.user_id) AS "user",
    auto_verification_policy.office_id,
    core.get_office_name_by_office_id(auto_verification_policy.office_id) AS office,
    auto_verification_policy.effective_from,
    auto_verification_policy.ends_on,
    auto_verification_policy.is_active
   FROM finance.auto_verification_policy
  WHERE NOT auto_verification_policy.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

