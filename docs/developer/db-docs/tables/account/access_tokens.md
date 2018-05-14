# account.access_tokens table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | access_token_id | [ ] | uuid | 0 |  |
| 2 | issued_by | [ ] | text | 0 |  |
| 3 | audience | [ ] | text | 0 |  |
| 4 | ip_address | [x] | text | 0 |  |
| 5 | user_agent | [x] | text | 0 |  |
| 6 | header | [x] | text | 0 |  |
| 7 | subject | [x] | text | 0 |  |
| 8 | token_id | [x] | text | 0 |  |
| 9 | application_id | [x] | uuid | 0 |  |
| 10 | login_id | [ ] | bigint | 0 |  |
| 11 | client_token | [ ] | text | 0 |  |
| 12 | claims | [x] | text | 0 |  |
| 13 | created_on | [ ] | timestamp with time zone | 0 |  |
| 14 | expires_on | [ ] | timestamp with time zone | 0 |  |
| 15 | revoked | [ ] | boolean | 0 |  |
| 16 | revoked_by | [x] | integer | 0 |  |
| 17 | revoked_on | [x] | timestamp with time zone | 0 |  |
| 18 | audit_user_id | [x] | integer | 0 |  |
| 19 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 20 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 9 | [application_id](../account/applications.md) | access_tokens_application_id_fkey | account.applications.application_id |
| 10 | [login_id](../account/logins.md) | access_tokens_login_id_fkey | account.logins.login_id |
| 16 | [revoked_by](../account/users.md) | access_tokens_revoked_by_fkey | account.users.user_id |
| 18 | [audit_user_id](../account/users.md) | access_tokens_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| access_tokens_pkey | frapid_db_user | btree | access_token_id |  |
| access_tokens_client_token_key | frapid_db_user | btree | client_token |  |
| access_tokens_token_info_inx | frapid_db_user | btree | client_token, ip_address, user_agent |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | access_token_id | gen_random_uuid() |
| 15 | revoked | false |
| 19 | audit_ts | now() |
| 20 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |
| account.account_token_auto_expiry_trigger | [account.token_auto_expiry_trigger](../../functions/account/token_auto_expiry_trigger-4455007.md) | INSERT | BEFORE |  | 0 | ROW |  |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
