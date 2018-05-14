# finance.merchant_fee_setup_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | merchant_fee_setup_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.merchant_fee_setup_scrud_view
 AS
 SELECT merchant_fee_setup.merchant_fee_setup_id,
    ((bank_accounts.bank_name::text || ' ('::text) || bank_accounts.bank_account_number::text) || ')'::text AS merchant_account,
    ((payment_cards.payment_card_code::text || ' ( '::text) || payment_cards.payment_card_name::text) || ')'::text AS payment_card,
    merchant_fee_setup.rate,
    merchant_fee_setup.customer_pays_fee,
    ((accounts.account_number::text || ' ('::text) || accounts.account_name::text) || ')'::text AS account,
    merchant_fee_setup.statement_reference
   FROM finance.merchant_fee_setup
     JOIN finance.bank_accounts ON merchant_fee_setup.merchant_account_id = bank_accounts.account_id
     JOIN finance.payment_cards ON merchant_fee_setup.payment_card_id = payment_cards.payment_card_id
     JOIN finance.accounts ON merchant_fee_setup.account_id = accounts.account_id
  WHERE NOT merchant_fee_setup.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

