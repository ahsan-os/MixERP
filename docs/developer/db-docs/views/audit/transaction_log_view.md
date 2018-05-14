# audit.transaction_log_view view

| Schema | [audit](../../schemas/audit.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | transaction_log_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW audit.transaction_log_view
 AS
 SELECT t.log_id,
    t.user_id,
    u.name,
    t.tran_type,
    t.action,
    t.invoice_number,
    t.tran_date,
    round(t.amount, 2) AS amount,
    t.remarks,
    t.audit_ts
   FROM audit.transaction_log t
     JOIN account.users u ON t.user_id = u.user_id;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

