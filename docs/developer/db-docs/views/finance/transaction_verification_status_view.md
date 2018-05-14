# finance.transaction_verification_status_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | transaction_verification_status_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.transaction_verification_status_view
 AS
 SELECT transaction_master.transaction_master_id,
    transaction_master.user_id,
    transaction_master.office_id,
    transaction_master.verification_status_id,
    account.get_name_by_user_id(transaction_master.verified_by_user_id) AS verifier_name,
    transaction_master.verified_by_user_id,
    transaction_master.last_verified_on,
    transaction_master.verification_reason
   FROM finance.transaction_master;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

