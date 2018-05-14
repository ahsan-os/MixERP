# inventory.verified_checkout_view materialized view

| Schema | [inventory](../../schemas/inventory.md) |
| Materialized View Name | verified_checkout_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

```sql
 CREATE MATERIALIZED VIEW inventory.verified_checkout_view
 AS
 SELECT checkout_view.transaction_master_id,
    checkout_view.checkout_id,
    checkout_view.checkout_detail_id,
    checkout_view.book,
    checkout_view.transaction_counter,
    checkout_view.transaction_code,
    checkout_view.value_date,
    checkout_view.transaction_ts,
    checkout_view.login_id,
    checkout_view.user_id,
    checkout_view.office_id,
    checkout_view.cost_center_id,
    checkout_view.reference_number,
    checkout_view.statement_reference,
    checkout_view.last_verified_on,
    checkout_view.verified_by_user_id,
    checkout_view.verification_status_id,
    checkout_view.verification_reason,
    checkout_view.transaction_type,
    checkout_view.store_id,
    checkout_view.item_id,
    checkout_view.quantity,
    checkout_view.unit_id,
    checkout_view.base_quantity,
    checkout_view.base_unit_id,
    checkout_view.price,
    checkout_view.discount,
    checkout_view.tax,
    checkout_view.shipping_charge,
    checkout_view.amount
   FROM inventory.checkout_view
  WHERE checkout_view.verification_status_id > 0;
```

### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)
