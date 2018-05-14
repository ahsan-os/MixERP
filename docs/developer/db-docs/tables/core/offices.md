# core.offices table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | office_id | [ ] | integer | 0 |  |
| 2 | office_code | [ ] | character varying | 12 |  |
| 3 | office_name | [ ] | character varying | 150 |  |
| 4 | nick_name | [x] | character varying | 50 |  |
| 5 | registration_date | [x] | date | 0 |  |
| 6 | currency_code | [x] | character varying | 12 |  |
| 7 | po_box | [x] | character varying | 128 |  |
| 8 | address_line_1 | [x] | character varying | 128 |  |
| 9 | address_line_2 | [x] | character varying | 128 |  |
| 10 | street | [x] | character varying | 50 |  |
| 11 | city | [x] | character varying | 50 |  |
| 12 | state | [x] | character varying | 50 |  |
| 13 | zip_code | [x] | character varying | 24 |  |
| 14 | country | [x] | character varying | 50 |  |
| 15 | phone | [x] | character varying | 24 |  |
| 16 | fax | [x] | character varying | 24 |  |
| 17 | email | [x] | character varying | 128 |  |
| 18 | url | [x] | character varying | 50 |  |
| 19 | logo | [x] | photo | 0 |  |
| 20 | parent_office_id | [x] | integer | 0 |  |
| 21 | registration_number | [x] | character varying | 100 |  |
| 22 | pan_number | [x] | character varying | 50 |  |
| 24 | allow_transaction_posting | [ ] | boolean | 0 |  |
| 25 | audit_user_id | [x] | integer | 0 |  |
| 26 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 27 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 20 | [parent_office_id](../core/offices.md) | offices_parent_office_id_fkey | core.offices.office_id |
| 25 | [audit_user_id](../account/users.md) | offices_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| offices_pkey | frapid_db_user | btree | office_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | office_id | nextval('core.offices_office_id_seq'::regclass) |
| 24 | allow_transaction_posting | false |
| 26 | audit_ts | now() |
| 27 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
