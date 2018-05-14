# finance.bank_accounts table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | bank_account_id | [ ] | integer | 0 |  |
| 2 | bank_account_name | [ ] | character varying | 1000 |  |
| 3 | account_id | [x] | integer | 0 |  |
| 4 | maintained_by_user_id | [ ] | integer | 0 |  |
| 5 | is_merchant_account | [ ] | boolean | 0 |  |
| 6 | office_id | [ ] | integer | 0 |  |
| 7 | bank_name | [ ] | character varying | 128 |  |
| 8 | bank_branch | [ ] | character varying | 128 |  |
| 9 | bank_contact_number | [x] | character varying | 128 |  |
| 10 | bank_account_number | [x] | character varying | 128 |  |
| 11 | bank_account_type | [x] | character varying | 128 |  |
| 12 | street | [x] | character varying | 50 |  |
| 13 | city | [x] | character varying | 50 |  |
| 14 | state | [x] | character varying | 50 |  |
| 15 | country | [x] | character varying | 50 |  |
| 16 | phone | [x] | character varying | 50 |  |
| 17 | fax | [x] | character varying | 50 |  |
| 18 | cell | [x] | character varying | 50 |  |
| 19 | relationship_officer_name | [x] | character varying | 128 |  |
| 20 | relationship_officer_contact_number | [x] | character varying | 128 |  |
| 21 | audit_user_id | [x] | integer | 0 |  |
| 22 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 23 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 3 | [account_id](../finance/accounts.md) | bank_accounts_account_id_fkey | finance.accounts.account_id |
| 4 | [maintained_by_user_id](../account/users.md) | bank_accounts_maintained_by_user_id_fkey | account.users.user_id |
| 6 | [office_id](../core/offices.md) | bank_accounts_office_id_fkey | core.offices.office_id |
| 21 | [audit_user_id](../account/users.md) | bank_accounts_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| bank_accounts_pkey | frapid_db_user | btree | bank_account_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| bank_accounts_account_id_chk CHECK (finance.get_account_master_id_by_account_id(account_id::bigint) = 10102) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | bank_account_id | nextval('finance.bank_accounts_bank_account_id_seq'::regclass) |
| 5 | is_merchant_account | false |
| 22 | audit_ts | now() |
| 23 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
