# finance.exchange_rates table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | exchange_rate_id | [ ] | bigint | 0 |  |
| 2 | updated_on | [ ] | timestamp with time zone | 0 |  |
| 3 | office_id | [ ] | integer | 0 |  |
| 4 | status | [ ] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 3 | [office_id](../core/offices.md) | exchange_rates_office_id_fkey | core.offices.office_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| exchange_rates_pkey | frapid_db_user | btree | exchange_rate_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | exchange_rate_id | nextval('finance.exchange_rates_exchange_rate_id_seq'::regclass) |
| 2 | updated_on | now() |
| 4 | status | true |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
