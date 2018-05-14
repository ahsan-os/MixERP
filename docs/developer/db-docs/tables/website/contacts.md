# website.contacts table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | contact_id | [ ] | integer | 0 |  |
| 2 | title | [ ] | character varying | 500 |  |
| 3 | name | [ ] | character varying | 500 |  |
| 4 | position | [x] | character varying | 500 |  |
| 5 | address | [x] | character varying | 500 |  |
| 6 | city | [x] | character varying | 500 |  |
| 7 | state | [x] | character varying | 500 |  |
| 8 | country | [x] | character varying | 100 |  |
| 9 | postal_code | [x] | character varying | 500 |  |
| 10 | telephone | [x] | character varying | 500 |  |
| 11 | details | [x] | text | 0 |  |
| 12 | email | [x] | character varying | 500 |  |
| 13 | recipients | [x] | character varying | 1000 |  |
| 14 | display_email | [ ] | boolean | 0 |  |
| 15 | display_contact_form | [ ] | boolean | 0 |  |
| 16 | sort | [ ] | integer | 0 |  |
| 17 | status | [ ] | boolean | 0 |  |
| 18 | audit_user_id | [x] | integer | 0 |  |
| 19 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 20 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 18 | [audit_user_id](../account/users.md) | contacts_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| contacts_pkey | frapid_db_user | btree | contact_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | contact_id | nextval('website.contacts_contact_id_seq'::regclass) |
| 14 | display_email | false |
| 15 | display_contact_form | true |
| 16 | sort | 0 |
| 17 | status | true |
| 19 | audit_ts | now() |
| 20 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
