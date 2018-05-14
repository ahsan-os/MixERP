# inventory.verified_checkout_details_view view

| Schema | [inventory](../../schemas/inventory.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | verified_checkout_details_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW inventory.verified_checkout_details_view
 AS
 SELECT checkout_details.checkout_detail_id,
    checkout_details.checkout_id,
    checkout_details.store_id,
    checkout_details.value_date,
    checkout_details.book_date,
    checkout_details.transaction_type,
    checkout_details.item_id,
    checkout_details.price,
    checkout_details.discount,
    checkout_details.cost_of_goods_sold,
    checkout_details.tax,
    checkout_details.shipping_charge,
    checkout_details.unit_id,
    checkout_details.quantity,
    checkout_details.base_unit_id,
    checkout_details.base_quantity,
    checkout_details.audit_ts
   FROM inventory.checkout_details
     JOIN inventory.checkouts ON checkouts.checkout_id = checkout_details.checkout_id
     JOIN finance.transaction_master ON transaction_master.transaction_master_id = checkouts.transaction_master_id AND transaction_master.verification_status_id > 0;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

