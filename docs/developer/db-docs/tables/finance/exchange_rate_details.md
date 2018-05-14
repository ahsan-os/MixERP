# finance.exchange_rate_details table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | exchange_rate_detail_id | [ ] | bigint | 0 |  |
| 2 | exchange_rate_id | [ ] | bigint | 0 |  |
| 3 | local_currency_code | [ ] | character varying | 12 |  |
| 4 | foreign_currency_code | [ ] | character varying | 12 |  |
| 5 | unit | [ ] | integer_strict | 0 |  |
| 6 | exchange_rate | [ ] | decimal_strict | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [exchange_rate_id](../finance/exchange_rates.md) | exchange_rate_details_exchange_rate_id_fkey | finance.exchange_rates.exchange_rate_id |
| 3 | [local_currency_code](../core/currencies.md) | exchange_rate_details_local_currency_code_fkey | core.currencies.currency_code |
| 4 | [foreign_currency_code](../core/currencies.md) | exchange_rate_details_foreign_currency_code_fkey | core.currencies.currency_code |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| exchange_rate_details_pkey | frapid_db_user | btree | exchange_rate_detail_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | exchange_rate_detail_id | nextval('finance.exchange_rate_details_exchange_rate_detail_id_seq'::regclass) |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
