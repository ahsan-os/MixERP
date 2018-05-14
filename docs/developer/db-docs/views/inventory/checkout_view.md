# inventory.checkout_view view

| Schema | [inventory](../../schemas/inventory.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | checkout_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW inventory.checkout_view
 AS
 SELECT transaction_master.transaction_master_id,
    checkouts.checkout_id,
    checkout_details.checkout_detail_id,
    transaction_master.book,
    transaction_master.transaction_counter,
    transaction_master.transaction_code,
    transaction_master.value_date,
    transaction_master.transaction_ts,
    transaction_master.login_id,
    transaction_master.user_id,
    transaction_master.office_id,
    transaction_master.cost_center_id,
    transaction_master.reference_number,
    transaction_master.statement_reference,
    transaction_master.last_verified_on,
    transaction_master.verified_by_user_id,
    transaction_master.verification_status_id,
    transaction_master.verification_reason,
    checkout_details.transaction_type,
    checkout_details.store_id,
    checkout_details.item_id,
    checkout_details.quantity,
    checkout_details.unit_id,
    checkout_details.base_quantity,
    checkout_details.base_unit_id,
    checkout_details.price,
    checkout_details.discount,
    checkout_details.tax,
    checkout_details.shipping_charge,
    (checkout_details.price::numeric - checkout_details.discount::numeric + COALESCE(checkout_details.tax::numeric, 0::numeric) + COALESCE(checkout_details.shipping_charge::numeric, 0::numeric)) * checkout_details.quantity::numeric AS amount
   FROM inventory.checkout_details
     JOIN inventory.checkouts ON checkouts.checkout_id = checkout_details.checkout_id
     JOIN finance.transaction_master ON transaction_master.transaction_master_id = checkouts.transaction_master_id;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

