# sales.gift_card_transaction_view view

| Schema | [sales](../../schemas/sales.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | gift_card_transaction_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW sales.gift_card_transaction_view
 AS
 SELECT transaction_master.transaction_master_id,
    transaction_master.transaction_ts,
    transaction_master.transaction_code,
    transaction_master.value_date,
    transaction_master.book_date,
    users.name AS entered_by,
    (((gift_cards.first_name::text || ' '::text) || gift_cards.middle_name::text) || ' '::text) || gift_cards.last_name::text AS customer_name,
    gift_card_transactions.amount,
    verification_statuses.verification_status_name AS status,
    verified_by_user.name AS verified_by,
    transaction_master.verification_reason,
    transaction_master.last_verified_on,
    offices.office_name,
    cost_centers.cost_center_name,
    transaction_master.reference_number,
    transaction_master.statement_reference,
    account.get_name_by_user_id(transaction_master.user_id) AS posted_by,
    transaction_master.office_id
   FROM finance.transaction_master
     JOIN core.offices ON transaction_master.office_id = offices.office_id
     JOIN finance.cost_centers ON transaction_master.cost_center_id = cost_centers.cost_center_id
     JOIN sales.gift_card_transactions ON gift_card_transactions.transaction_master_id = transaction_master.transaction_master_id
     JOIN account.users ON transaction_master.user_id = users.user_id
     LEFT JOIN sales.gift_cards ON gift_card_transactions.gift_card_id = gift_cards.gift_card_id
     JOIN core.verification_statuses ON transaction_master.verification_status_id = verification_statuses.verification_status_id
     LEFT JOIN account.users verified_by_user ON transaction_master.verified_by_user_id = verified_by_user.user_id;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

