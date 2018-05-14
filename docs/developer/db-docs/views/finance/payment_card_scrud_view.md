# finance.payment_card_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | payment_card_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.payment_card_scrud_view
 AS
 SELECT payment_cards.payment_card_id,
    payment_cards.payment_card_code,
    payment_cards.payment_card_name,
    ((card_types.card_type_code::text || ' ('::text) || card_types.card_type_name::text) || ')'::text AS card_type
   FROM finance.payment_cards
     JOIN finance.card_types ON payment_cards.card_type_id = card_types.card_type_id
  WHERE NOT payment_cards.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

