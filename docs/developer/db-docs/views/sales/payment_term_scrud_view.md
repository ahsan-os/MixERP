# sales.payment_term_scrud_view view

| Schema | [sales](../../schemas/sales.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | payment_term_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW sales.payment_term_scrud_view
 AS
 SELECT payment_terms.payment_term_id,
    payment_terms.payment_term_code,
    payment_terms.payment_term_name,
    payment_terms.due_on_date,
    payment_terms.due_days,
    ((due_fequency.frequency_code::text || ' ('::text) || due_fequency.frequency_name::text) || ')'::text AS due_fequency,
    payment_terms.grace_period,
    ((late_fee.late_fee_code::text || ' ('::text) || late_fee.late_fee_name::text) || ')'::text AS late_fee,
    ((late_fee_frequency.frequency_code::text || ' ('::text) || late_fee_frequency.frequency_name::text) || ')'::text AS late_fee_frequency
   FROM sales.payment_terms
     JOIN finance.frequencies due_fequency ON due_fequency.frequency_id = payment_terms.due_frequency_id
     JOIN finance.frequencies late_fee_frequency ON late_fee_frequency.frequency_id = payment_terms.late_fee_posting_frequency_id
     JOIN sales.late_fee ON late_fee.late_fee_id = payment_terms.late_fee_id
  WHERE NOT payment_terms.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

