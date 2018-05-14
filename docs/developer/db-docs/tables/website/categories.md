# website.categories table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | category_id | [ ] | integer | 0 |  |
| 1 | category_id | [ ] | integer | 0 |  |
| 2 | category_name | [ ] | character varying | 250 |  |
| 2 | category_name | [ ] | character varying | 500 |  |
| 3 | category_alias | [ ] | character varying | 500 |  |
| 3 | alias | [ ] | character varying | 250 |  |
| 4 | description | [x] | text | 0 |  |
| 4 | seo_description | [x] | character varying | 100 |  |
| 5 | locked | [ ] | boolean | 0 |  |
| 5 | is_blog | [ ] | boolean | 0 |  |
| 6 | audit_user_id | [x] | integer | 0 |  |
| 6 | icon | [x] | character varying | 100 |  |
| 7 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | published | [ ] | boolean | 0 |  |
| 8 | parent_category_id | [x] | integer | 0 |  |
| 8 | deleted | [x] | boolean | 0 |  |
| 9 | display_recent_topics | [ ] | boolean | 0 |  |
| 10 | audit_user_id | [x] | integer | 0 |  |
| 11 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 12 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 6 | [audit_user_id](../account/users.md) | categories_audit_user_id_fkey | account.users.user_id |
| 10 | [audit_user_id](../account/users.md) | categories_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| categories_pkey | frapid_db_user | btree | category_id |  |
| categories_alias_key | frapid_db_user | btree | alias |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | category_id | nextval('forums.categories_category_id_seq'::regclass) |
| 1 | category_id | nextval('website.categories_category_id_seq'::regclass) |
| 5 | locked | false |
| 5 | is_blog | false |
| 7 | audit_ts | now() |
| 7 | published | true |
| 8 | deleted | false |
| 9 | display_recent_topics | true |
| 11 | audit_ts | now() |
| 12 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
