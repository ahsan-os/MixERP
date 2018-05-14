# finance.journal_verification_policy_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | journal_verification_policy_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.journal_verification_policy_scrud_view
 AS
 SELECT journal_verification_policy.journal_verification_policy_id,
    journal_verification_policy.user_id,
    account.get_name_by_user_id(journal_verification_policy.user_id) AS "user",
    journal_verification_policy.office_id,
    core.get_office_name_by_office_id(journal_verification_policy.office_id) AS office,
    journal_verification_policy.can_verify,
    journal_verification_policy.can_self_verify,
    journal_verification_policy.effective_from,
    journal_verification_policy.ends_on,
    journal_verification_policy.is_active
   FROM finance.journal_verification_policy
  WHERE NOT journal_verification_policy.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

