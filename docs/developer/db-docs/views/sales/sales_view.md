# sales.sales_view view

| Schema | [sales](../../schemas/sales.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | sales_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW sales.sales_view
 AS
 SELECT sales.sales_id,
    sales.transaction_master_id,
    transaction_master.transaction_code,
    transaction_master.transaction_counter,
    transaction_master.value_date,
    transaction_master.book_date,
    transaction_master.transaction_ts,
    transaction_master.verification_status_id,
    verification_statuses.verification_status_name,
    transaction_master.verified_by_user_id,
    account.get_name_by_user_id(transaction_master.verified_by_user_id) AS verified_by,
    sales.checkout_id,
    checkouts.discount,
    checkouts.posted_by,
    account.get_name_by_user_id(checkouts.posted_by) AS posted_by_name,
    checkouts.office_id,
    checkouts.cancelled,
    checkouts.cancellation_reason,
    sales.cash_repository_id,
    cash_repositories.cash_repository_code,
    cash_repositories.cash_repository_name,
    sales.price_type_id,
    price_types.price_type_code,
    price_types.price_type_name,
    sales.counter_id,
    counters.counter_code,
    counters.counter_name,
    counters.store_id,
    stores.store_code,
    stores.store_name,
    sales.customer_id,
    customers.customer_name,
    sales.salesperson_id,
    account.get_name_by_user_id(sales.salesperson_id) AS salesperson_name,
    sales.gift_card_id,
    gift_cards.gift_card_number,
    (((gift_cards.first_name::text || ' '::text) || gift_cards.middle_name::text) || ' '::text) || gift_cards.last_name::text AS gift_card_owner,
    sales.coupon_id,
    coupons.coupon_code,
    coupons.coupon_name,
    sales.is_flat_discount,
    sales.total_discount_amount,
    sales.is_credit,
    sales.payment_term_id,
    payment_terms.payment_term_code,
    payment_terms.payment_term_name,
    sales.fiscal_year_code,
    sales.invoice_number,
    sales.total_amount,
    sales.tender,
    sales.change,
    sales.check_number,
    sales.check_date,
    sales.check_bank_name,
    sales.check_amount,
    sales.reward_points
   FROM sales.sales
     JOIN inventory.checkouts ON checkouts.checkout_id = sales.checkout_id
     JOIN finance.transaction_master ON transaction_master.transaction_master_id = checkouts.transaction_master_id
     JOIN finance.cash_repositories ON cash_repositories.cash_repository_id = sales.cash_repository_id
     JOIN sales.price_types ON price_types.price_type_id = sales.price_type_id
     JOIN inventory.counters ON counters.counter_id = sales.counter_id
     JOIN inventory.stores ON stores.store_id = counters.store_id
     JOIN inventory.customers ON customers.customer_id = sales.customer_id
     LEFT JOIN sales.gift_cards ON gift_cards.gift_card_id = sales.gift_card_id
     LEFT JOIN sales.payment_terms ON payment_terms.payment_term_id = sales.payment_term_id
     LEFT JOIN sales.coupons ON coupons.coupon_id = sales.coupon_id
     LEFT JOIN core.verification_statuses ON verification_statuses.verification_status_id = transaction_master.verification_status_id
  WHERE NOT transaction_master.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

