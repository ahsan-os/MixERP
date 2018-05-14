# website.contents table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | content_id | [ ] | integer | 0 |  |
| 2 | category_id | [ ] | integer | 0 |  |
| 3 | title | [ ] | character varying | 500 |  |
| 4 | alias | [ ] | character varying | 500 |  |
| 5 | author_id | [x] | integer | 0 |  |
| 6 | publish_on | [ ] | timestamp with time zone | 0 |  |
| 7 | created_on | [ ] | timestamp with time zone | 0 |  |
| 8 | last_editor_id | [x] | integer | 0 |  |
| 9 | last_edited_on | [x] | timestamp with time zone | 0 |  |
| 10 | markdown | [x] | text | 0 |  |
| 11 | contents | [ ] | text | 0 |  |
| 12 | tags | [x] | text | 0 |  |
| 13 | hits | [x] | bigint | 0 |  |
| 14 | is_draft | [ ] | boolean | 0 |  |
| 15 | seo_description | [ ] | character varying | 1000 |  |
| 16 | is_homepage | [ ] | boolean | 0 |  |
| 17 | audit_user_id | [x] | integer | 0 |  |
| 18 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 19 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [category_id](../website/categories.md) | contents_category_id_fkey | website.categories.category_id |
| 5 | [author_id](../account/users.md) | contents_author_id_fkey | account.users.user_id |
| 8 | [last_editor_id](../account/users.md) | contents_last_editor_id_fkey | account.users.user_id |
| 17 | [audit_user_id](../account/users.md) | contents_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| contents_pkey | frapid_db_user | btree | content_id |  |
| contents_alias_key | frapid_db_user | btree | alias |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | content_id | nextval('website.contents_content_id_seq'::regclass) |
| 7 | created_on | now() |
| 14 | is_draft | true |
| 15 | seo_description | ''::character varying |
| 16 | is_homepage | false |
| 18 | audit_ts | now() |
| 19 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
